namespace CodeBase.UI.Windows.Input
{
    public class FireButton : BaseInputButton
    {
        private void Start()
        {
            OnUp += () => InputService.UpdateFire(false);
            OnDown += () => InputService.UpdateFire(true);
        }
    }
}