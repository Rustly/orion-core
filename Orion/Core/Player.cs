﻿using System;
using Microsoft.Xna.Framework;

namespace Orion.Core
{
	/// <summary>
	/// Wraps a Terraria player.
	/// </summary>
	public class Player : IPlayer
	{
		private readonly IItem[] _inventory;

		/// <inheritdoc/>
		public int Defense => WrappedPlayer.statDefense;

		/// <inheritdoc/>
		public int Health
		{
			get { return WrappedPlayer.statLife; }
			set { WrappedPlayer.statLife = value; }
		}

		/// <inheritdoc/>
		public int MaxHealth
		{
			get { return WrappedPlayer.statLifeMax; }
			set { WrappedPlayer.statLifeMax = value; }
		}

		/// <inheritdoc/>
		public int Mana
		{
			get { return WrappedPlayer.statMana; }
			set { WrappedPlayer.statMana = value; }
		}

		/// <inheritdoc/>
		public int MaxMana
		{
			get { return WrappedPlayer.statManaMax; }
			set { WrappedPlayer.statManaMax = value; }
		}

		/// <inheritdoc/>
		public string Name => WrappedPlayer.name;

		/// <inheritdoc/>
		public Vector2 Position
		{
			get { return WrappedPlayer.position; }
			set { WrappedPlayer.position = value; }
		}

		/// <inheritdoc/>
		public Vector2 Velocity
		{
			get { return WrappedPlayer.velocity; }
			set { WrappedPlayer.velocity = value; }
		}

		/// <inheritdoc/>
		public Terraria.Player WrappedPlayer { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="Player"/> class wrapping the specified Terraria player.
		/// </summary>
		/// <param name="terrariaPlayer">The Terraria player to wrap.</param>
		/// <exception cref="ArgumentNullException"><paramref name="terrariaPlayer"/> was null.</exception>
		public Player(Terraria.Player terrariaPlayer)
		{
			if (terrariaPlayer == null)
			{
				throw new ArgumentNullException(nameof(terrariaPlayer));
			}

			_inventory = new IItem[terrariaPlayer.inventory.Length];
			WrappedPlayer = terrariaPlayer;
		}

		/// <inheritdoc/>
		public IItem GetInventory(int index)
		{
			if (_inventory[index]?.WrappedItem != WrappedPlayer.inventory[index])
			{
				_inventory[index] = new Item(WrappedPlayer.inventory[index]);
			}
			return _inventory[index];
		}

		/// <inheritdoc/>
		public IItem GetSelectedItem() => GetInventory(WrappedPlayer.selectedItem);

		/// <inheritdoc/>
		public void SetInventory(int index, IItem item)
		{
			_inventory[index] = item;
			WrappedPlayer.inventory[index] = item.WrappedItem;
		}
	}
}
