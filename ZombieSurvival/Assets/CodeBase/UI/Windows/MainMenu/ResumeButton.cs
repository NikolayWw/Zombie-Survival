using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.MainMenu
{
    public class ResumeButton : MonoBehaviour
    {
        [SerializeField] private Button _resumeButton;

        private IGameStateMachine _gameStateMachine;
        private IPersistentProgressService _persistentProgress;
        private ISaveLoadService _saveLoad;

        public void Construct(IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoad)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgress = persistentProgressService;
            _saveLoad = saveLoad;

            _resumeButton.onClick.AddListener(ResumeGame);
        }

        private void ResumeGame()
        {
            _persistentProgress.PlayerProgress = _saveLoad.LoadProgress();
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}