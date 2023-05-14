namespace CodeBase.UI.Windows.Input
{
    public class WeaponReloadButton : BaseInputButton
    {
        private void Start() => OnDown += () => InputService.OnWeaponReload?.Invoke();
    }
}