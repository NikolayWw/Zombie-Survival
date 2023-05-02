using CodeBase.Logic.Pause;

namespace CodeBase.Player.Move
{
    public interface IMoveState : IFreeze
    {
        void Enter();

        void Exit();

        void FixedUpdate();

        void DrawGizmos();
    }
}