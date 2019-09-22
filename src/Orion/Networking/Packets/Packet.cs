﻿// Copyright (c) 2015-2019 Pryaxis & Orion Contributors
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
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using Orion.Networking.Packets.Entities;
using Orion.Networking.Packets.Misc;
using Orion.Networking.Packets.Modules;
using Orion.Networking.Packets.World;
using Orion.Networking.Packets.World.TileEntities;
using Orion.Networking.Packets.World.Tiles;
using Orion.Utils;

namespace Orion.Networking.Packets {
    /// <summary>
    /// Represents a packet.
    /// </summary>
    public abstract class Packet : IDirtiable {
        private static readonly Func<Packet>[] PacketConstructors = {
            /* 000 */ null,
            /* 001 */ () => new PlayerConnectPacket(),
            /* 002 */ () => new PlayerDisconnectPacket(),
            /* 003 */ () => new PlayerContinueConnectingPacket(),
            /* 004 */ () => new PlayerDataPacket(),
            /* 005 */ () => new PlayerInventorySlotPacket(),
            /* 006 */ () => new PlayerJoinPacket(),
            /* 007 */ () => new WorldInfoPacket(),
            /* 008 */ () => new RequestSectionPacket(),
            /* 009 */ () => new PlayerStatusPacket(),
            /* 010 */ () => new SectionPacket(),
            /* 011 */ () => new SectionFramesPacket(),
            /* 012 */ () => new SpawnPlayerPacket(),
            /* 013 */ () => new PlayerInfoPacket(),
            /* 014 */ () => new PlayerActivityPacket(),
            /* 015 */ null,
            /* 016 */ () => new PlayerHealthPacket(),
            /* 017 */ () => new TileModificationPacket(),
            /* 018 */ () => new TimePacket(),
            /* 019 */ () => new ToggleDoorPacket(),
            /* 020 */ () => new SquareTilesPacket(),
            /* 021 */ () => new ItemInfoPacket(),
            /* 022 */ () => new ItemOwnerPacket(),
            /* 023 */ () => new NpcInfoPacket(),
            /* 024 */ () => new NpcDamageHeldItemPacket(),
            /* 025 */ null,
            /* 026 */ null,
            /* 027 */ () => new ProjectileInfoPacket(),
            /* 028 */ () => new NpcDamagePacket(),
            /* 029 */ () => new RemoveProjectilePacket(),
            /* 030 */ () => new PlayerPvpPacket(),
            /* 031 */ () => new RequestChestPacket(),
            /* 032 */ () => new ChestContentsSlotPacket(),
            /* 033 */ () => new ChestInfoPacket(),
            /* 034 */ () => new ChestModificationPacket(),
            /* 035 */ () => new PlayerHealEffectPacket(),
            /* 036 */ () => new PlayerZonesPacket(),
            /* 037 */ () => new PlayerPasswordChallengePacket(),
            /* 038 */ () => new PlayerPasswordResponsePacket(),
            /* 039 */ () => new RemoveItemOwnerPacket(),
            /* 040 */ () => new PlayerTalkingToNpcPacket(),
            /* 041 */ () => new PlayerItemAnimationPacket(),
            /* 042 */ () => new PlayerManaPacket(),
            /* 043 */ () => new PlayerManaEffectPacket(),
            /* 044 */ null,
            /* 045 */ () => new PlayerTeamPacket(),
            /* 046 */ () => new RequestSignPacket(),
            /* 047 */ () => new SignInfoPacket(),
            /* 048 */ () => new TileLiquidPacket(),
            /* 049 */ () => new PlayerEnterWorldPacket(),
            /* 050 */ () => new PlayerBuffsPacket(),
            /* 051 */ () => new MiscActionPacket(),
            /* 052 */ () => new UnlockObjectPacket(),
            /* 053 */ () => new NpcBuffPacket(),
            /* 054 */ () => new NpcBuffsPacket(),
            /* 055 */ () => new PlayerBuffPacket(),
            /* 056 */ () => new NpcNamePacket(),
            /* 057 */ () => new BiomeStatsPacket(),
            /* 058 */ () => new PlayerHarpNotePacket(),
            /* 059 */ () => new ActivateWirePacket(),
            /* 060 */ () => new NpcHomePacket(),
            /* 061 */ () => new BossOrInvasionPacket(),
            /* 062 */ () => new PlayerDodgePacket(),
            /* 063 */ () => new PaintBlockPacket(),
            /* 064 */ () => new PaintWallPacket(),
            /* 065 */ () => new EntityTeleportationPacket(),
            /* 066 */ () => new PlayerHealPacket(),
            /* 067 */ null,
            /* 068 */ () => new PlayerUuidPacket(),
            /* 069 */ () => new ChestNamePacket(),
            /* 070 */ () => new NpcCatchPacket(),
            /* 071 */ () => new ReleaseNpcPacket(),
            /* 072 */ () => new TravelingMerchantShopPacket(),
            /* 073 */ () => new PlayerTeleportationPotionPacket(),
            /* 074 */ () => new AnglerQuestPacket(),
            /* 075 */ () => new PlayerFinishAnglerQuestPacket(),
            /* 076 */ () => new PlayerAnglerQuestsPacket(),
            /* 077 */ () => new TileAnimationPacket(),
            /* 078 */ () => new InvasionInfoPacket(),
            /* 079 */ () => new PlaceObjectPacket(),
            /* 080 */ () => new PlayerChestPacket(),
            /* 081 */ () => new CombatNumberPacket(),
            /* 082 */ () => new ModulePacket(),
            /* 083 */ () => new NpcKillCountPacket(),
            /* 084 */ () => new PlayerStealthPacket(),
            /* 085 */ () => new QuickStackChestPacket(),
            /* 086 */ () => new TileEntityInfoPacket(),
            /* 087 */ () => new PlaceTileEntityPacket(),
            /* 088 */ () => new ItemAlterationPacket(),
            /* 089 */ () => new ItemFramePacket(),
            /* 090 */ () => new ItemInstanceInfoPacket(),
            /* 091 */ () => new EmoteBubblePacket(),
            /* 092 */ () => new NpcStealCoinsPacket(),
            /* 093 */ null,
            /* 094 */ null,
            /* 095 */ () => new RemovePortalPacket(),
            /* 096 */ () => new TeleportPlayerPortalPacket(),
            /* 097 */ () => new NpcTypeKilledPacket(),
            /* 098 */ () => new ProgressionEventPacket(),
            /* 099 */ () => new PlayerMinionPositionPacket(),
            /* 100 */ () => new TeleportNpcPortalPacket(),
            /* 101 */ () => new PillarShieldStrengthsPacket(),
            /* 102 */ () => new PlayerNebulaBuffPacket(),
            /* 103 */ () => new MoonLordCountdownPacket(),
            /* 104 */ () => new NpcShopSlotPacket(),
            /* 105 */ () => new ToggleGemLockPacket(),
            /* 106 */ () => new PoofOfSmokePacket(),
            /* 107 */ () => new ChatPacket(),
            /* 108 */ () => new CannonShotPacket(),
            /* 109 */ () => new MassWireOperationPacket(),
            /* 110 */ () => new PlayerConsumeItemsPacket(),
            /* 111 */ () => new ToggleBirthdayPartyPacket(),
            /* 112 */ () => new TreeGrowingEffectPacket(),
            /* 113 */ () => new StartOldOnesArmyPacket(),
            /* 114 */ () => new EndOldOnesArmyPacket(),
            /* 115 */ () => new PlayerMinionNpcPacket(),
            /* 116 */ () => new OldOnesArmyInfoPacket(),
            /* 117 */ () => new PlayerDamagePacket(),
            /* 118 */ () => new PlayerKillPacket(),
            /* 119 */ () => new CombatTextPacket()
        };

        private protected bool _isDirty;

        /// <inheritdoc />
        public virtual bool IsDirty => _isDirty;

        /// <summary>
        /// Gets the packet's type.
        /// </summary>
        public abstract PacketType Type { get; }

        // Prevent outside inheritance.
        private protected Packet() { }

        /// <summary>
        /// Reads and returns a packet from the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the packet.</param>
        /// <returns>The resulting packet.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="PacketException">The packet could not be parsed correctly.</exception>
        public static Packet ReadFromStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            try {
                var reader = new BinaryReader(stream, Encoding.UTF8, true);
#if DEBUG
                var oldPosition = stream.Position;
                var packetLength = reader.ReadUInt16();
#else
                reader.ReadUInt16();
#endif
                Func<Packet> GetPacketConstructor(byte packetTypeId) =>
                    packetTypeId < PacketConstructors.Length ? PacketConstructors[packetTypeId] : null;

                var packetConstructor = GetPacketConstructor(reader.ReadByte()) ??
                                        throw new PacketException("Packet type is invalid.");
                var packet = packetConstructor();
                packet.ReadFromReader(reader, context);
                packet.Clean();
#if DEBUG
                Debug.Assert(stream.Position - oldPosition == packetLength, "Packet should have been consumed.");
                Debug.Assert(!packet.IsDirty, "Packet should not be dirty.");
#endif
                return packet;
            } catch (Exception ex) when (!(ex is PacketException)) {
                throw new PacketException("An exception occurred when parsing the packet.", ex);
            }
        }

        /// <inheritdoc />
        [ExcludeFromCodeCoverage]
        public override string ToString() => $"{Type}";

        /// <inheritdoc />
        public virtual void Clean() {
            _isDirty = false;
        }

        /// <summary>
        /// Writes the packet to the given stream with the specified context.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="context">The context with which to read the packet.</param>
        /// <exception cref="ArgumentNullException"><paramref name="stream"/> is <c>null</c>.</exception>
        /// <exception cref="PacketException">The packet could not be written correctly.</exception>
        public void WriteToStream(Stream stream, PacketContext context) {
            if (stream == null) throw new ArgumentNullException(nameof(stream));

            try {
                var writer = new BinaryWriter(stream, Encoding.UTF8, true);
                var oldPosition = stream.Position;
                stream.Position += 2;
                writer.Write((byte)Type);
                WriteToWriter(writer, context);

                var position = stream.Position;
                var packetLength = position - oldPosition;
                if (packetLength > ushort.MaxValue) {
                    throw new PacketException("Packet is too long.");
                }

                stream.Position = oldPosition;
                writer.Write((ushort)packetLength);
                stream.Position = position;
            } catch (Exception ex) when (!(ex is PacketException)) {
                throw new PacketException("An exception occurred when writing the packet.", ex);
            }
        }

        private protected abstract void ReadFromReader(BinaryReader reader, PacketContext context);
        private protected abstract void WriteToWriter(BinaryWriter writer, PacketContext context);
    }
}
