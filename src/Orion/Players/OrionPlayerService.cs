﻿// Copyright (c) 2019 Pryaxis & Orion Contributors
// 
// This file is part of Orion.
// 
// Orion is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Orion is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Orion.  If not, see <https://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Orion.Events;
using Orion.Events.Packets;
using Orion.Events.Players;
using Orion.Packets;
using Orion.Packets.Modules;
using Orion.Packets.Players;
using Orion.Utils;
using OTAPI;
using Main = Terraria.Main;
using TerrariaPlayer = Terraria.Player;

namespace Orion.Players {
    internal sealed class OrionPlayerService : OrionService, IPlayerService {
        private readonly ThreadLocal<bool> _shouldIgnoreNextReceiveData = new ThreadLocal<bool>();

        private readonly IDictionary<PacketType, Action<PacketReceiveEventArgs>> _packetReceiveHandlers =
            new Dictionary<PacketType, Action<PacketReceiveEventArgs>>();

        public IReadOnlyArray<IPlayer> Players { get; }

        public EventHandlerCollection<PacketReceiveEventArgs> PacketReceive { get; }
            = new EventHandlerCollection<PacketReceiveEventArgs>();

        public EventHandlerCollection<PacketSendEventArgs> PacketSend { get; }
            = new EventHandlerCollection<PacketSendEventArgs>();

        public EventHandlerCollection<PlayerConnectEventArgs> PlayerConnect { get; }
            = new EventHandlerCollection<PlayerConnectEventArgs>();

        public EventHandlerCollection<PlayerDataEventArgs> PlayerData { get; }
            = new EventHandlerCollection<PlayerDataEventArgs>();

        public EventHandlerCollection<PlayerInventorySlotEventArgs> PlayerInventorySlot { get; }
            = new EventHandlerCollection<PlayerInventorySlotEventArgs>();

        public EventHandlerCollection<PlayerJoinEventArgs> PlayerJoin { get; }
            = new EventHandlerCollection<PlayerJoinEventArgs>();

        public EventHandlerCollection<PlayerChatEventArgs> PlayerChat { get; }
            = new EventHandlerCollection<PlayerChatEventArgs>();

        public EventHandlerCollection<PlayerDisconnectedEventArgs> PlayerDisconnected { get; }
            = new EventHandlerCollection<PlayerDisconnectedEventArgs>();

        public OrionPlayerService() {
            // Ignore the last player since it is used as a failure slot.
            Players = new WrappedReadOnlyArray<OrionPlayer, TerrariaPlayer>(
                Main.player.AsMemory(..^1),
                (playerIndex, terrariaPlayer) => new OrionPlayer(this, playerIndex, terrariaPlayer));

            _packetReceiveHandlers[PacketType.PlayerConnect] = PlayerConnectHandler;
            _packetReceiveHandlers[PacketType.PlayerData] = PlayerDataHandler;
            _packetReceiveHandlers[PacketType.PlayerInventorySlot] = PlayerInventorySlotHandler;
            _packetReceiveHandlers[PacketType.PlayerJoin] = PlayerJoinHandler;
            _packetReceiveHandlers[PacketType.Module] = ModuleHandler;

            Hooks.Net.ReceiveData = ReceiveDataHandler;
            Hooks.Net.SendBytes = SendBytesHandler;
            Hooks.Net.RemoteClient.PreReset = PreResetHandler;
        }

        protected override void Dispose(bool disposeManaged) {
            if (!disposeManaged) {
                return;
            }

            _shouldIgnoreNextReceiveData.Dispose();

            Hooks.Net.ReceiveData = null;
            Hooks.Net.SendBytes = null;
            Hooks.Net.RemoteClient.PreReset = null;
        }

        private HookResult ReceiveDataHandler(Terraria.MessageBuffer buffer, ref byte packetId, ref int readOffset,
                ref int start, ref int length) {
            Debug.Assert(buffer != null, "buffer should not be null");
            Debug.Assert(buffer.whoAmI >= 0 && buffer.whoAmI < Players.Count, "buffer should have a valid index");

            if (_shouldIgnoreNextReceiveData.Value) {
                _shouldIgnoreNextReceiveData.Value = false;
                return HookResult.Continue;
            }

            // Offset start and length by two since the packet length field is not included.
            var stream = new MemoryStream(buffer.readBuffer, start - 2, length + 2);
            var sender = Players[buffer.whoAmI];
            var packet = Packet.ReadFromStream(stream, PacketContext.Server);
            var args = new PacketReceiveEventArgs(sender, packet);
            PacketReceive.Invoke(this, args);

            if (args.IsCanceled()) {
                return HookResult.Cancel;
            }

            packet = args.Packet;

            if (_packetReceiveHandlers.TryGetValue(packet.Type, out var handler)) {
                handler(args);
                if (args.IsCanceled()) {
                    return HookResult.Cancel;
                }

                packet = args.Packet;
            }

            if (!args.IsDirty) {
                return HookResult.Continue;
            }

            var oldBuffer = buffer.readBuffer;
            // TODO: consider reusing buffers here
            var newStream = new MemoryStream();
            packet.WriteToStream(newStream, PacketContext.Client);
            buffer.readBuffer = newStream.ToArray();
            buffer.ResetReader();

            // Ignore the next ReceiveDataHandler call.
            _shouldIgnoreNextReceiveData.Value = true;
            buffer.GetData(2, (int)(newStream.Length - 2), out _);
            buffer.readBuffer = oldBuffer;
            buffer.ResetReader();
            return HookResult.Cancel;
        }

        private HookResult SendBytesHandler(ref int remoteClient, ref byte[] data, ref int offset, ref int size,
                ref Terraria.Net.Sockets.SocketSendCallback callback, ref object state) {
            Debug.Assert(remoteClient >= 0 && remoteClient < Players.Count, "remote client should be a valid index");

            var stream = new MemoryStream(data, offset, size);
            var receiver = Players[remoteClient];
            var packet = Packet.ReadFromStream(stream, PacketContext.Client);
            var args = new PacketSendEventArgs(receiver, packet);
            PacketSend.Invoke(this, args);
            if (args.IsCanceled()) {
                return HookResult.Cancel;
            }

            if (!args.IsDirty) {
                return HookResult.Continue;
            }

            // TODO: consider reusing buffers here
            var newStream = new MemoryStream();
            args.Packet.WriteToStream(newStream, PacketContext.Server);
            data = newStream.ToArray();
            offset = 0;
            size = data.Length;
            return HookResult.Continue;
        }

        private HookResult PreResetHandler(Terraria.RemoteClient remoteClient) {
            Debug.Assert(remoteClient != null, "remote client should not be null");
            Debug.Assert(remoteClient.Id >= 0 && remoteClient.Id < Players.Count,
                "remote client should have a valid index");

            var player = Players[remoteClient.Id];
            var args = new PlayerDisconnectedEventArgs(player);
            PlayerDisconnected.Invoke(this, args);
            return HookResult.Continue;
        }

        private void PlayerConnectHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerConnectPacket)args_.Packet;
            var args = new PlayerConnectEventArgs(args_.Sender, packet);
            PlayerConnect.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;
        }

        private void PlayerDataHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerDataPacket)args_.Packet;
            var args = new PlayerDataEventArgs(args_.Sender, packet);
            PlayerData.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;
        }

        private void PlayerInventorySlotHandler(PacketReceiveEventArgs args_) {
            var packet = (PlayerInventorySlotPacket)args_.Packet;
            var args = new PlayerInventorySlotEventArgs(args_.Sender, packet);
            PlayerInventorySlot.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;
        }

        private void PlayerJoinHandler(PacketReceiveEventArgs args_) {
            var args = new PlayerJoinEventArgs(args_.Sender);
            PlayerJoin.Invoke(this, args);
            args_.CancellationReason = args.CancellationReason;
        }

        private void ModuleHandler(PacketReceiveEventArgs args_) {
            var module = ((ModulePacket)args_.Packet).Module;
            if (module is ChatModule chatModule) {
                var args = new PlayerChatEventArgs(args_.Sender, chatModule);
                PlayerChat.Invoke(this, args);
                args_.CancellationReason = args.CancellationReason;
            }
        }
    }
}
