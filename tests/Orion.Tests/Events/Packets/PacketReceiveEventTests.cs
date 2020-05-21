﻿// Copyright (c) 2020 Pryaxis & Orion Contributors
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
using Moq;
using Orion.Packets;
using Orion.Players;
using Xunit;

namespace Orion.Events.Packets {
    public class PacketReceiveEventTests {
        [Fact]
        public void Ctor_NullSender_ThrowsArgumentNullException() {
            var packet = new TestPacket();

            Assert.Throws<ArgumentNullException>(() => new PacketReceiveEvent<TestPacket>(ref packet, null!));
        }

        [Fact]
        public void Sender_Get() {
            var packet = new TestPacket();
            var sender = new Mock<IPlayer>().Object;
            var evt = new PacketReceiveEvent<TestPacket>(ref packet, sender);

            Assert.Same(sender, evt.Sender);
        }

        private struct TestPacket : IPacket {
            public PacketType Type => throw new NotImplementedException();
            public void Read(ReadOnlySpan<byte> span, PacketContext context) => throw new NotImplementedException();
            public void Write(ref Span<byte> span, PacketContext context) => throw new NotImplementedException();
        }
    }
}
