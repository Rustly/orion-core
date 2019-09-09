﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FluentAssertions;
using Orion.Networking.Packets;
using Xunit;

namespace Orion.Tests.Networking.Packets.Npcs {
    public class NpcHomePacketTests {
        public static readonly byte[] NpcHomeBytes = {10, 0, 60, 0, 0, 0, 1, 100, 0, 0,};

        [Fact]
        public void ReadFromStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcHomeBytes)) {
                var packet = (NpcHomePacket)Packet.ReadFromStream(stream);

                packet.NpcIndex.Should().Be(0);
                packet.NpcHomeX.Should().Be(256);
                packet.NpcHomeY.Should().Be(100);
                packet.IsNpcHomeless.Should().BeFalse();
            }
        }

        [Fact]
        public void WriteToStream_IsCorrect() {
            using (var stream = new MemoryStream(NpcHomeBytes))
            using (var stream2 = new MemoryStream()) {
                var packet = Packet.ReadFromStream(stream);

                packet.WriteToStream(stream2);

                stream2.ToArray().Should().BeEquivalentTo(NpcHomeBytes);
            }
        }
    }
}
