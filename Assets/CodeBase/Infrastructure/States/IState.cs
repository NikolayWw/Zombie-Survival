namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitable
    {
        void Enter();
    }

    public interface IExitable
    {
        void Exit();
    }
}