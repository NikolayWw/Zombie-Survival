using CodeBase.Infrastructure.Logic;
using System;
using System.Collections;
using UnityEngine;

namespace CodeBase.UI.Windows.Inventory.WeaponInventory.Logic
{
    public class UIWeaponTimer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _delay;
        public Action OnElapsed;
        public bool Elapsed { get; private set; }

        public UIWeaponTimer(ICoroutineRunner coroutineRunner, float delay)
        {
            _coroutineRunner = coroutineRunner;
            _delay = delay;
            OnElapsed = null;
            Elapsed = false;
        }

        public void Start()
        {
            Elapsed = false;
            _coroutineRunner.StartCoroutine(TimerProcess());
        }

        public void Unsubscribe() =>
            OnElapsed = null;

        private IEnumerator TimerProcess()
        {
            yield return new WaitForSeconds(_delay);
            Elapsed = true;
            OnElapsed?.Invoke();
        }
    }
}