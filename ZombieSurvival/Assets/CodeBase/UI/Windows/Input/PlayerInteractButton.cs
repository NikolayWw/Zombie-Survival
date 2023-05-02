namespace CodeBase.UI.Windows.Input
{
    public class PlayerInteractButton : BaseInputButton
    {
        private void Start() => OnDown += () => InputService.OnInteract?.Invoke();
    }
}