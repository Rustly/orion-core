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
using JetBrains.Annotations;
using Serilog.Events;

namespace Orion.Events {
    /// <summary>
    /// Specifies information about event arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    [BaseTypeRequired(typeof(EventArgs))]
    public sealed class EventArgsAttribute : Attribute {
        /// <summary>
        /// Gets the event's name, which is used for logs.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets or sets the log level for the event.
        /// 
        /// <para/>
        /// 
        /// By default this is <c>Debug</c>. Noisier events may want to use <c>Verbose</c>.
        /// </summary>
        public LogEventLevel LogLevel { get; set; } = LogEventLevel.Debug;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventArgsAttribute"/> class with the specified name.
        /// </summary>
        /// <param name="name">
        /// The name, which is used for logs.
        /// 
        /// <para/>
        /// 
        /// This should be short while still disambiguating the event among all events. The convention is to use
        /// <c>kebab-case</c>.
        /// </param>
        /// <exception cref="ArgumentNullException"><paramref name="name"/> is <see langword="null"/>.</exception>
        public EventArgsAttribute(string name) {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }
}
