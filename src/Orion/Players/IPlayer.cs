﻿using Orion.Entities;

namespace Orion.Players {
    /// <summary>
    /// Provides a wrapper around a Terraria.Player instance.
    /// </summary>
    public interface IPlayer : IEntity {
        /// <summary>
        /// Gets or sets the player's health.
        /// </summary>
        int Health { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum health.
        /// </summary>
        int MaxHealth { get; set; }

        /// <summary>
        /// Gets or sets the player's mana.
        /// </summary>
        int Mana { get; set; }

        /// <summary>
        /// Gets or sets the player's maximum mana.
        /// </summary>
        int MaxMana { get; set; }
    }
}
