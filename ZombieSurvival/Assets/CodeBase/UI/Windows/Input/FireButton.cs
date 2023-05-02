namespace CodeBase.UI.Windows.Input
{
    public class FireButton : BaseInputButton
    {
        private void Start()
        {
            OnUp += () => InputService.SetFire(false);
            OnDown += () => InputService.SetFire(true);
        }
    }
}