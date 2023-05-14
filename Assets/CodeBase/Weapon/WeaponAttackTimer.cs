using UnityEngine;

namespace CodeBase.Weapon
{
    public class WeaponAttackTimer
    {
        private readonly float _delay;
        private float _elapsedTime;
        public bool Elapsed => Time.timeSinceLevelLoad >= _elapsedTime;

        public WeaponAttackTimer(float delay) =>
            _delay = delay;

        public void Start() =>
            _elapsedTime = Time.timeSinceLevelLoad + _delay;
    }
}