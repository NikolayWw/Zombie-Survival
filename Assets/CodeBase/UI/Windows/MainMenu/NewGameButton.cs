using CodeBase.Infrastructure.States;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.MainMenu
{
    public class NewGameButton : MonoBehaviour
    {
        [SerializeField] private Button _newGameButton;

        private IPersistentProgressService _persistentProgress;
        private ISaveLoadService _saveLoad;
        private IGameStateMachine _gameStateMachine;

        public void Construct(IGameStateMachine gameStateMachine, IPersistentProgressService persistentProgressService, ISaveLoadService saveLoad)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgress = persistentProgressService;
            _saveLoad = saveLoad;

            _newGameButton.onClick.AddListener(LoadNewGame);
        }

        private void LoadNewGame()
        {
            _persistentProgress.PlayerProgress = _saveLoad.NewProgress();
            _gameStateMachine.Enter<LoadLevelState>();
        }
    }
}