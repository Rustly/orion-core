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

using System.Collections.Generic;

namespace Orion.Npcs {
    /// <summary>
    /// Represents an item service. Provides access to NPC-related properties and methods.
    /// </summary>
    public interface INpcService {
        /// <summary>
        /// Gets the NPCs.
        /// </summary>
        /// <value>The NPCs.</value>
        IReadOnlyList<INpc> Npcs { get; }
    }
}
