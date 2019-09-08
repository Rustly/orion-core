﻿using System.IO;

namespace Orion.Networking.Packets.Connections {
    /// <summary>
    /// Packet sent from the server to the client to allow it to continue connecting.
    /// </summary>
    public sealed class ContinueConnectingPacket : Packet {
        /// <summary>
        /// Gets or sets the player index that the client will use.
        /// </summary>
        public byte PlayerIndex { get; set; }

        private protected override void ReadFromReader(BinaryReader reader, ushort packetLength) {
            PlayerIndex = reader.ReadByte();
        }

        private protected override void WriteToWriter(BinaryWriter writer) {
            writer.Write(PlayerIndex);
        }
    }
}
