namespace CodeBase.Services.Pause
{
    public interface IPauseService : IService
    {
        void Pause();

        void Play();

        void FreezePlayer();

        void UnfreezePlayer();
    }
}