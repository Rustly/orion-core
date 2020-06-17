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
using Orion.Core.Players;
using Orion.Core.World;
using Orion.Core.World.Tiles;
using Xunit;

namespace Orion.Core.Events.World.Tiles
{
    public class WallPlaceEventTests
    {
        [Fact]
        public void Ctor_NullWorld_ThrowsArgumentNullException()
        {
            var player = Mock.Of<IPlayer>();

            Assert.Throws<ArgumentNullException>(
                () => new WallPlaceEvent(null!, player, 256, 100, WallId.Stone, false));
        }

        [Fact]
        public void Id_Get()
        {
            var world = Mock.Of<IWorld>();
            var player = Mock.Of<IPlayer>();
            var evt = new WallPlaceEvent(world, player, 256, 100, WallId.Stone, false);

            Assert.Equal(WallId.Stone, evt.Id);
        }

        [Fact]
        public void IsReplacement_Get()
        {
            var world = Mock.Of<IWorld>();
            var player = Mock.Of<IPlayer>();
            var evt = new WallPlaceEvent(world, player, 256, 100, WallId.Stone, false);

            Assert.False(evt.IsReplacement);
        }
    }
}
