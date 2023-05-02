using CodeBase.Infrastructure.Logic;
using CodeBase.Services.Factory;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Dialogs;
using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows.Dialogs;
using System.Collections;

namespace CodeBase.Logic.Quest
{
    public class QuestPlayer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly IGameFactory _gameFactory;
        private readonly IWindowService _windowService;
        private readonly IStaticDataService _dataService;

        private QuestAudioPlayer _audioPlayer;
        private DialogWindow _dialogWindow;
        public bool IsPlayed { get; private set; }

        public QuestPlayer(ICoroutineRunner coroutineRunner, IStaticDataService dataService, IGameFactory gameFactory, IWindowService windowService)
        {
            _coroutineRunner = coroutineRunner;
            _dataService = dataService;
            _gameFactory = gameFactory;
            _windowService = windowService;
        }

        public void Play(params DialogId[] ids)
        {
            IsPlayed = true;
            InitAudioAndWindow();
            _coroutineRunner.StartCoroutine(PlayProcess(ids));
        }

        private IEnumerator PlayProcess(DialogId[] dialogIds)
        {
            int contextCount = 0;
            while (true)
            {
                if (_audioPlayer.IsPlaying == false && contextCount < dialogIds.Length)
                {
                    var context = _dataService.ForDialog(dialogIds[contextCount]).Context;
                    _audioPlayer.PlayAudio(context.AudioClip);
                    _dialogWindow.Refresh(context.Context);
                    contextCount++;
                }

                if (_audioPlayer.IsPlaying == false && contextCount >= dialogIds.Length)
                    break;
                yield return null;
            }

            CloseAudioAndWindow();
            IsPlayed = false;
        }

        private void InitAudioAndWindow()
        {
            if (_dialogWindow == null)
            {
                _windowService.Open(WindowId.DialogWindow);
                _windowService.GetWindow(WindowId.DialogWindow, out _dialogWindow);
            }

            if (_audioPlayer == null)
                _audioPlayer = _gameFactory.CreateQuestAudioPlayer();
        }

        private void CloseAudioAndWindow()
        {
            _windowService.Close(WindowId.DialogWindow);
            _audioPlayer.Close();

            _dialogWindow = null;
            _audioPlayer = null;
        }
    }
}