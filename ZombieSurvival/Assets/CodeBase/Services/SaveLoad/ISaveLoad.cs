namespace CodeBase.Services.SaveLoad
{
    public interface ISaveLoad
    {
        void Save();

        void Load();

        bool CanSave { get; }
    }
}