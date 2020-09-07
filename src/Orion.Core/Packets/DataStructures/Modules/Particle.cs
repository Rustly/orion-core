﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Orion.Core.Utils;

namespace Orion.Core.Packets.DataStructures.Modules
{
    /// <summary>
    /// A module sent to display particle effects.
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 22)]
    public struct Particle : IModule
    {
        [FieldOffset(0)] private byte _bytes; // Used to obtain an interior reference

        /// <summary>
        /// Gets or sets the type of particle
        /// </summary>
        [field: FieldOffset(0)] public ParticleType Type { get; set; }

        /// <summary>
        /// Gets or sets the world position.
        /// </summary>
        [field: FieldOffset(1)] public Vector2 Position { get; set; }

        /// <summary>
        /// Gets or sets the movement vector.
        /// </summary>
        [field: FieldOffset(9)] public Vector2 MovementVector { get; set; }

        /// <summary>
        /// Gets or sets the shader index.
        /// </summary>
        [field: FieldOffset(17)] public int ShaderIndex { get; set; }

        /// <summary>
        /// Gets or sets the index of the player who triggered the module.
        /// </summary>
        [field: FieldOffset(21)] public byte PlayerIndex { get; set; }

        ModuleId IModule.Id => ModuleId.Particle;

        int IModule.ReadBody(Span<byte> span, PacketContext context) => span.Read(ref _bytes, 22);

        int IModule.WriteBody(Span<byte> span, PacketContext context) => span.Write(ref _bytes, 22);
    }

    /// <summary>
    /// Specifies a type of <see cref="Particle"/>.
    /// </summary>
    public enum ParticleType : byte
    {
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        Keybrand,
        FlameWaders,
        StellarTune,
        WallOfFleshGoatMountFlames,
        BlackLightningHit,
        RainbowRodHit,
        BlackLightningSmall,
        StardustPunch
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
    }
}
