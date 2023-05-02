namespace CodeBase.Logic.Pause
{
    public interface IFreeze : IPause
    {
        void Freeze();

        void Unfreeze();
    }
}