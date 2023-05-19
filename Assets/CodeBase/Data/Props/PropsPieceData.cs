using CodeBase.StaticData.Props;
using System;
using UnityEngine;

namespace CodeBase.Data.Props
{
    [Serializable]
    public class PropsPieceData
    {
        [field: SerializeField] public PropsId PropsId { get; private set; }
        [field: SerializeField] public Vector3 Position { get; private set; }
        [field: SerializeField] public float CurrentHealth { get; private set; }

        public PropsPieceData(PropsId id, Vector3 position, float health)
        {
            PropsId = id;
            Position = position;
            CurrentHealth = health;
        }

        public void SetPosition(Vector3 at) =>
            Position = at;

        public void Decrement(float value)
        {
            CurrentHealth -= value;
            if (CurrentHealth < 0)
                CurrentHealth = 0;
        }
    }
}