using CodeBase.Data;

namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        PlayerProgress LoadProgress();

        void SaveProgress();

        PlayerProgress NewProgress();
    }
}