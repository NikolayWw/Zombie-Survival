using UnityEngine;

namespace CodeBase.Player
{
    public class PlayerAudioPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource _audioSource;

        public void Play(AudioClip audioClip) =>
            _audioSource.PlayOneShot(audioClip);
    }
}