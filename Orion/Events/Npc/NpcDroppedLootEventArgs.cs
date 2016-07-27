﻿using System;
using Orion.Core;
using Orion.Services;

namespace Orion.Events.Npc
{
	/// <summary>
	/// Provides data for the <see cref="INpcService.NpcDroppedLoot"/> event.
	/// </summary>
	public class NpcDroppedLootEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the loot <see cref="IItem"/> that the <see cref="INpc"/> dropped.
		/// </summary>
		public IItem Item { get; }

		/// <summary>
		/// Gets the <see cref="INpc"/> that dropped the loot.
		/// </summary>
		public INpc Npc { get; }

		/// <summary>
		/// Initializes a new instance of the <see cref="NpcDroppedLootEventArgs"/> class.
		/// </summary>
		/// <param name="npc">The <see cref="INpc"/> that dropped the loot.</param>
		/// <param name="item">The loot <see cref="IItem"/> that the <see cref="INpc"/> dropped.</param>
		/// <exception cref="ArgumentNullException">
		/// <paramref name="npc"/> or <paramref name="item"/> were null.
		/// </exception>
		public NpcDroppedLootEventArgs(INpc npc, IItem item)
		{
			if (npc == null)
			{
				throw new ArgumentNullException(nameof(npc));
			}
			if (item == null)
			{
				throw new ArgumentNullException(nameof(item));
			}

			Npc = npc;
			Item = item;
		}
	}
}
