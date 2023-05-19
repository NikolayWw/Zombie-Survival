using CodeBase.Logic.Pause;
using CodeBase.Services.Factory;
using System.Collections;
using UnityEngine;

namespace CodeBase.Logic.Quest
{
    public class QuestAudioPlayer : MonoBehaviour, IPause
    {
        [SerializeField] private AudioSource _audioSource;
        public bool IsPlaying { get; private set; }
        private bool _isPause;
        private IGameFactory _gameFactory;

        public void Construct(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        #region Pause

        public void Play()
        {
            _isPause = false;
            _audioSource.Play();
        }

        public void Pause()
        {
            _isPause = true;
            _audioSource.Pause();
        }

        #endregion Pause

        public void PlayAudio(AudioClip audioClip)
        {
            StartCoroutine(PlayProcess(audioClip));
        }

        public void Close()
        {
            _gameFactory.Pauses.Remove(this);
            Destroy(gameObject);
        }

        private IEnumerator PlayProcess(AudioClip audioClip)
        {
            IsPlaying = true;
            _audioSource.clip = audioClip;
            _audioSource.Play();

            while (_audioSource.isPlaying || _isPause)
            {
                yield return null;
            }

            IsPlaying = false;
        }
    }
}