using CodeBase.Infrastructure.Logic;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.UI.Windows.Inventory.WeaponInventory.Logic
{
    public class UIWeaponTimerUpdate
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private Action<float> _onUpdate;
        private Action _onElapsed;
        public bool Elapsed { get; private set; }

        public UIWeaponTimerUpdate(ICoroutineRunner coroutineRunner, Action<float> onUpdate, Action onElapsed)
        {
            _coroutineRunner = coroutineRunner;
            _onUpdate = onUpdate;
            _onElapsed = onElapsed;
        }

        public void Start()
        {
            Elapsed = false;
            _coroutineRunner.StartCoroutine(TimerProcess());
        }

        public void Unsubscribe()
        {
            _onUpdate = null;
            _onElapsed = null;
        }

        private IEnumerator TimerProcess()
        {
            const float elapsedTime = 0.5f;
            float startTimerValue = Time.timeSinceLevelLoad;
            float current = 0.0f;

            while (current <= 1.0f)//image fill amount
            {
                current = Time.timeSinceLevelLoad - startTimerValue;
                current /= elapsedTime;
                _onUpdate?.Invoke(current);

                yield return null;
            }
            Elapsed = true;
            _onElapsed?.Invoke();
        }
    }
}