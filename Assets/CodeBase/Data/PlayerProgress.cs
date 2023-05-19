using CodeBase.Data.QuestItemInventory;
using CodeBase.Data.WeaponInventory;
using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        [field: SerializeField] public PlayerData PlayerData { get; private set; }
        [field: SerializeField] public WorldData.WorldData WorldData { get; private set; }
        [field: SerializeField] public WeaponSlotsContainer WeaponSlotsContainer { get; private set; }
        [field: SerializeField] public QuestSlotsContainer QuestSlotsContainer { get; private set; }
        [field: SerializeField] public Quests.Quests Quests { get; private set; }

        public PlayerProgress(PlayerData playerData, WorldData.WorldData worldData, WeaponSlotsContainer weaponSlotsContainer)
        {
            PlayerData = playerData;
            WorldData = worldData;
            WeaponSlotsContainer = weaponSlotsContainer;

            QuestSlotsContainer = new QuestSlotsContainer();
            Quests = new Quests.Quests();
        }
    }
}