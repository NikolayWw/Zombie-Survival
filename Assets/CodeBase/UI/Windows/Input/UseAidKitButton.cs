namespace CodeBase.UI.Windows.Input
{
    public class UseAidKitButton : BaseInputButton
    {
        private void Start() => OnDown += () => InputService.OnUseAidKit?.Invoke();
    }
}