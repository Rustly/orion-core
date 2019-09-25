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

using System.IO;
using FluentAssertions;
using Xunit;

namespace Orion.Packets.World.Tiles {
    public class SectionRequestPacketTests {
        [Fact]
        public void SetSimpleProperties_MarkAsDirty() {
            var packet = new SectionRequestPacket();

            packet.SetSimplePropertiesShouldMarkAsDirty();
        }

        private static readonly byte[] Bytes = {11, 0, 8, 255, 255, 255, 255, 255, 255, 255, 255};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using var stream = new MemoryStream(Bytes);
            var packet = (SectionRequestPacket)Packet.ReadFromStream(stream, PacketContext.Server);

            packet.SectionX.Should().Be(-1);
            packet.SectionY.Should().Be(-1);
        }

        [Fact]
        public void DeserializeAndSerialize_SamePacket() {
            Bytes.ShouldDeserializeAndSerializeSamePacket();
        }
    }
}
