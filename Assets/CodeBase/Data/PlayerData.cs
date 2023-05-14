using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerData
    {
        [SerializeField] private float _maxHealth;

        [field: SerializeField] public int Money { get; private set; }
        [field: SerializeField] public int AidKit { get; private set; }
        [field: SerializeField] public float Health { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }

        public Action OnChangeMoney;
        public Action OnHealthChange;
        public Action OnFirstAidKitChange;

        public PlayerData(Vector3 playerPosition, int money, float maxHealth, int aidKit)
        {
            _maxHealth = maxHealth;
            Money = money;
            Health = maxHealth;
            AidKit = aidKit;
            Position = playerPosition;
        }

        public void SetPosition(Vector3 position) =>
            Position = position;

        public void IncrementAidKit(int value)
        {
            AidKit += value;
            OnFirstAidKitChange?.Invoke();
        }

        public void IncrementMoney(int value)
        {
            Money += value;
            OnChangeMoney?.Invoke();
        }

        public void IncrementHealth(float value)
        {
            Health += value;
            if (Health > _maxHealth)
                Health = _maxHealth;

            OnHealthChange?.Invoke();
        }

        public void DecrementAidKit(int value)
        {
            AidKit -= value;
            if (AidKit < 0)
                AidKit = 0;

            OnFirstAidKitChange?.Invoke();
        }

        public void DecrementMoney(int value)
        {
            Money -= value;
            OnChangeMoney?.Invoke();
        }

        public void DecrementHealth(float value)
        {
            Health -= value;
            if (Health < 0)
                Health = 0;

            OnHealthChange?.Invoke();
        }
    }
}