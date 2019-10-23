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

using Microsoft.Xna.Framework;
using Orion.Utils;

namespace Orion.Items {
    /// <summary>
    /// Represents an item service. Provides access to item-related properties and methods in a thread-safe manner
    /// unless specified otherwise.
    /// </summary>
    public interface IItemService {
        /// <summary>
        /// Gets the items. All items are returned, regardless of whether or not they are actually active.
        /// </summary>
        /// <value>The items.</value>
        IReadOnlyArray<IItem> Items { get; }

        /// <summary>
        /// Spawns and returns an item with the given <paramref name="type"/> at the specified
        /// <paramref name="position"/> with an optional <paramref name="stackSize"/> and <paramref name="prefix"/>.
        /// </summary>
        /// <param name="type">The item type.</param>
        /// <param name="position">The position.</param>
        /// <param name="stackSize">The stack size.</param>
        /// <param name="prefix">The item prefix.</param>
        /// <returns>The resulting item, or <see langword="null"/> if none was spawned.</returns>
        /// <remarks>Implementations of this method are not required to be thread-safe.</remarks>
        IItem? SpawnItem(ItemType type, Vector2 position, int stackSize = 1, ItemPrefix prefix = ItemPrefix.None);
    }
}
