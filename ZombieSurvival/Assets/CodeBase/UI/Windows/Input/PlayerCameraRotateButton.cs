namespace CodeBase.UI.Windows.Input
{
    public class PlayerCameraRotateButton : BaseInputButton
    {
        private void Start()
        {
            OnUp += () => InputService.OnTouchScreenCameraUp?.Invoke();
            OnDown += () => InputService.OnTouchScreenCameraDown?.Invoke();
        }
    }
}