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

using System;
using FluentAssertions;
using Xunit;

namespace Orion {
    public class ServiceAttributeTests {
        [Fact]
        public void Ctor_NullName_ThrowsArgumentNullException() {
            Func<ServiceAttribute> func = () => new ServiceAttribute(null);

            func.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Name_Get() {
            var attribute = new ServiceAttribute("TEST");

            attribute.Name.Should().Be("TEST");
        }

        [Fact]
        public void Author_SetNullValue_ThrowsArgumentNullException() {
            var attribute = new ServiceAttribute("");
            Action action = () => attribute.Author = null;

            action.Should().Throw<ArgumentNullException>();
        }
    }
}
