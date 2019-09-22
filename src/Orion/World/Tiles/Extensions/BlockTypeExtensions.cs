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

using System.Collections.Generic;
using JetBrains.Annotations;

namespace Orion.World.Tiles.Extensions {
    /// <summary>
    /// Provides extensions for the <see cref="BlockType"/> enumeration.
    /// </summary>
    [PublicAPI]
    public static class BlockTypeExtensions {
        [NotNull] private static readonly ISet<BlockType> FramesImportantBlockTypes = new HashSet<BlockType> {
            (BlockType)3,
            (BlockType)4,
            (BlockType)5,
            (BlockType)10,
            (BlockType)11,
            (BlockType)12,
            (BlockType)13,
            (BlockType)14,
            (BlockType)15,
            (BlockType)16,
            (BlockType)17,
            (BlockType)18,
            (BlockType)19,
            (BlockType)20,
            (BlockType)21,
            (BlockType)24,
            (BlockType)26,
            (BlockType)27,
            (BlockType)28,
            (BlockType)29,
            (BlockType)31,
            (BlockType)33,
            (BlockType)34,
            (BlockType)35,
            (BlockType)36,
            (BlockType)42,
            (BlockType)50,
            (BlockType)55,
            (BlockType)61,
            (BlockType)71,
            (BlockType)72,
            (BlockType)73,
            (BlockType)74,
            (BlockType)77,
            (BlockType)78,
            (BlockType)79,
            (BlockType)81,
            (BlockType)82,
            (BlockType)83,
            (BlockType)84,
            (BlockType)85,
            (BlockType)86,
            (BlockType)87,
            (BlockType)88,
            (BlockType)89,
            (BlockType)90,
            (BlockType)91,
            (BlockType)92,
            (BlockType)93,
            (BlockType)94,
            (BlockType)95,
            (BlockType)96,
            (BlockType)97,
            (BlockType)98,
            (BlockType)99,
            (BlockType)100,
            (BlockType)101,
            (BlockType)102,
            (BlockType)103,
            (BlockType)104,
            (BlockType)105,
            (BlockType)106,
            (BlockType)110,
            (BlockType)113,
            (BlockType)114,
            (BlockType)125,
            (BlockType)126,
            (BlockType)128,
            (BlockType)129,
            (BlockType)132,
            (BlockType)133,
            (BlockType)134,
            (BlockType)135,
            (BlockType)136,
            (BlockType)137,
            (BlockType)138,
            (BlockType)139,
            (BlockType)141,
            (BlockType)142,
            (BlockType)143,
            (BlockType)144,
            (BlockType)149,
            (BlockType)165,
            (BlockType)171,
            (BlockType)172,
            (BlockType)173,
            (BlockType)174,
            (BlockType)178,
            (BlockType)184,
            (BlockType)185,
            (BlockType)186,
            (BlockType)187,
            (BlockType)201,
            (BlockType)207,
            (BlockType)209,
            (BlockType)210,
            (BlockType)212,
            (BlockType)215,
            (BlockType)216,
            (BlockType)217,
            (BlockType)218,
            (BlockType)219,
            (BlockType)220,
            (BlockType)227,
            (BlockType)228,
            (BlockType)231,
            (BlockType)233,
            (BlockType)235,
            (BlockType)236,
            (BlockType)237,
            (BlockType)238,
            (BlockType)239,
            (BlockType)240,
            (BlockType)241,
            (BlockType)242,
            (BlockType)243,
            (BlockType)244,
            (BlockType)245,
            (BlockType)246,
            (BlockType)247,
            (BlockType)254,
            (BlockType)269,
            (BlockType)270,
            (BlockType)271,
            (BlockType)275,
            (BlockType)276,
            (BlockType)277,
            (BlockType)278,
            (BlockType)279,
            (BlockType)280,
            (BlockType)281,
            (BlockType)282,
            (BlockType)283,
            (BlockType)285,
            (BlockType)286,
            (BlockType)287,
            (BlockType)288,
            (BlockType)289,
            (BlockType)290,
            (BlockType)291,
            (BlockType)292,
            (BlockType)293,
            (BlockType)294,
            (BlockType)295,
            (BlockType)296,
            (BlockType)297,
            (BlockType)298,
            (BlockType)299,
            (BlockType)300,
            (BlockType)301,
            (BlockType)302,
            (BlockType)303,
            (BlockType)304,
            (BlockType)305,
            (BlockType)306,
            (BlockType)307,
            (BlockType)308,
            (BlockType)309,
            (BlockType)310,
            (BlockType)314,
            (BlockType)316,
            (BlockType)317,
            (BlockType)318,
            (BlockType)319,
            (BlockType)320,
            (BlockType)323,
            (BlockType)324,
            (BlockType)334,
            (BlockType)335,
            (BlockType)337,
            (BlockType)338,
            (BlockType)339,
            (BlockType)349,
            (BlockType)354,
            (BlockType)355,
            (BlockType)356,
            (BlockType)358,
            (BlockType)359,
            (BlockType)360,
            (BlockType)361,
            (BlockType)362,
            (BlockType)363,
            (BlockType)364,
            (BlockType)372,
            (BlockType)373,
            (BlockType)374,
            (BlockType)375,
            (BlockType)376,
            (BlockType)377,
            (BlockType)378,
            (BlockType)380,
            (BlockType)386,
            (BlockType)387,
            (BlockType)388,
            (BlockType)389,
            (BlockType)390,
            (BlockType)391,
            (BlockType)392,
            (BlockType)393,
            (BlockType)394,
            (BlockType)395,
            (BlockType)405,
            (BlockType)406,
            (BlockType)410,
            (BlockType)411,
            (BlockType)412,
            (BlockType)413,
            (BlockType)414,
            (BlockType)419,
            (BlockType)420,
            (BlockType)423,
            (BlockType)424,
            (BlockType)425,
            (BlockType)427,
            (BlockType)428,
            (BlockType)429,
            (BlockType)435,
            (BlockType)436,
            (BlockType)437,
            (BlockType)438,
            (BlockType)439,
            (BlockType)440,
            (BlockType)441,
            (BlockType)442,
            (BlockType)443,
            (BlockType)444,
            (BlockType)445,
            (BlockType)452,
            (BlockType)453,
            (BlockType)454,
            (BlockType)455,
            (BlockType)456,
            (BlockType)457,
            (BlockType)461,
            (BlockType)462,
            (BlockType)463,
            (BlockType)464,
            (BlockType)465,
            (BlockType)466,
            (BlockType)467,
            (BlockType)468,
            (BlockType)469
        };

        /// <summary>
        /// Returns a value indicating whether frames are important for the block type.
        /// </summary>
        /// <param name="blockType">The block type.</param>
        /// <returns>A value indicating whether frames are important.</returns>
        public static bool AreFramesImportant(this BlockType blockType) =>
            FramesImportantBlockTypes.Contains(blockType);
    }
}
