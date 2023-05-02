using CodeBase.Infrastructure.Logic;
using CodeBase.UI.Windows.Inventory.WeaponInventory.Logic;
using System;

namespace CodeBase.Enemy
{
    public class EnemyAnimationTimer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly float _delay;
        private readonly Action _onElapsed;
        private UIWeaponTimer _timer;

        public EnemyAnimationTimer(ICoroutineRunner coroutineRunner, float delay, Action onElapsed)
        {
            _coroutineRunner = coroutineRunner;
            _delay = delay;
            _onElapsed = onElapsed;
        }

        public void Start()
        {
            _timer?.Unsubscribe();
            _timer = new UIWeaponTimer(_coroutineRunner, _delay);
            _timer.Start();
            _timer.OnElapsed += () => _onElapsed?.Invoke();
        }
    }
}