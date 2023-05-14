namespace CodeBase.UI.Windows.Input
{
    public class JumpButton : BaseInputButton
    {
        private void Start() => OnDown += () => InputService.OnJump?.Invoke();
    }
}