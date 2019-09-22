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

namespace Orion.Networking.Packets {
    /// <summary>
    /// Specifies a packet type.
    /// </summary>
    public enum PacketType : byte {
#pragma warning disable 1591
        PlayerConnect = 1,
        PlayerDisconnect = 2,
        PlayerContinueConnecting = 3,
        PlayerData = 4,
        PlayerInventorySlot = 5,
        PlayerJoin = 6,
        WorldInfo = 7,
        RequestSection = 8,
        PlayerStatus = 9,
        Section = 10,
        SectionFrames = 11,
        SpawnPlayer = 12,
        PlayerInfo = 13,
        PlayerActivity = 14,
        PlayerHealth = 16,
        TileModification = 17,
        Time = 18,
        ToggleDoor = 19,
        SquareTiles = 20,
        ItemInfo = 21,
        ItemOwner = 22,
        NpcInfo = 23,
        NpcDamageHeldItem = 24,
        ProjectileInfo = 27,
        NpcDamage = 28,
        RemoveProjectile = 29,
        PlayerPvp = 30,
        RequestChest = 31,
        ChestContentsSlot = 32,
        ChestInfo = 33,
        ChestModification = 34,
        PlayerHealEffect = 35,
        PlayerZones = 36,
        PlayerPasswordChallenge = 37,
        PlayerPasswordResponse = 38,
        RemoveItemOwner = 39,
        PlayerTalkingToNpc = 40,
        PlayerItemAnimation = 41,
        PlayerMana = 42,
        PlayerManaEffect = 43,
        PlayerTeam = 45,
        RequestSign = 46,
        SignInfo = 47,
        TileLiquid = 48,
        PlayerEnterWorld = 49,
        PlayerBuffs = 50,
        MiscAction = 51,
        UnlockObject = 52,
        NpcBuff = 53,
        NpcBuffs = 54,
        PlayerBuff = 55,
        NpcName = 56,
        BiomeStats = 57,
        PlayerHarpNote = 58,
        ActivateWire = 59,
        NpcHome = 60,
        BossOrInvasion = 61,
        PlayerDodge = 62,
        PaintBlock = 63,
        PaintWall = 64,
        EntityTeleportation = 65,
        PlayerHeal = 66,
        PlayerUuid = 68,
        ChestName = 69,
        NpcCatch = 70,
        ReleaseNpc = 71,
        TravelingMerchantShop = 72,
        PlayerTeleportationPotion = 73,
        AnglerQuest = 74,
        PlayerFinishAnglerQuest = 75,
        PlayerAnglerQuests = 76,
        TileAnimation = 77,
        InvasionInfo = 78,
        PlaceObject = 79,
        PlayerChest = 80,
        CombatNumber = 81,
        Module = 82,
        NpcKillCount = 83,
        PlayerStealth = 84,
        QuickStackChest = 85,
        TileEntityInfo = 86,
        PlaceTileEntity = 87,
        ItemAlteration = 88,
        ItemFrame = 89,
        ItemInstanceInfo = 90,
        EmoteBubble = 91,
        NpcStealCoins = 92,
        RemovePortal = 95,
        TeleportPlayerPortal = 96,
        NpcTypeKilled = 97,
        ProgressionEvent = 98,
        PlayerMinionPosition = 99,
        TeleportNpcPortal = 100,
        PillarShieldStrengths = 101,
        PlayerNebulaBuff = 102,
        MoonLordCountdown = 103,
        NpcShopSlot = 104,
        ToggleGemLock = 105,
        PoofOfSmoke = 106,
        Chat = 107,
        CannonShot = 108,
        MassWireOperation = 109,
        PlayerConsumeItems = 110,
        ToggleBirthdayParty = 111,
        TreeGrowingEffect = 112,
        StartOldOnesArmy = 113,
        EndOldOnesArmy = 114,
        PlayerMinionNpc = 115,
        OldOnesArmyInfo = 116,
        PlayerDamage = 117,
        PlayerKill = 118,
        CombatText = 119
#pragma warning disable 1591
    }
}
