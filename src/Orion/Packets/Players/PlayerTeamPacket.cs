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
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Players;

namespace Orion.Packets.Players {
    /// <summary>
    /// Packet sent to set a player's team.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct PlayerTeamPacket : IPacket {
        [FieldOffset(0)] private byte _playerIndex;
        [FieldOffset(1)] private PlayerTeam _team;

        /// <summary>
        /// Gets or sets the player index.
        /// </summary>
        /// <value>The player index.</value>
        public byte PlayerIndex {
            get => _playerIndex;
            set => _playerIndex = value;
        }

        /// <summary>
        /// Gets or sets the player's team.
        /// </summary>
        /// <value>The player's team.</value>
        public PlayerTeam Team {
            get => _team;
            set => _team = value;
        }

        PacketId IPacket.Id => PacketId.PlayerTeam;

        /// <inheritdoc/>
        public void Read(ReadOnlySpan<byte> span, PacketContext context) {
            Unsafe.CopyBlockUnaligned(ref this.AsRefByte(0), ref Unsafe.AsRef(in span[0]), 2);
        }

        /// <inheritdoc/>
        public void Write(ref Span<byte> span, PacketContext context) {
            Unsafe.CopyBlockUnaligned(ref span[0], ref this.AsRefByte(0), 2);
            span = span[2..];
        }
    }
}
