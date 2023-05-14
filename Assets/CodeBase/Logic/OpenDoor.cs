using UnityEngine;

namespace CodeBase.Logic
{
    public class OpenDoor : MonoBehaviour
    {
        private readonly int OpenHash = Animator.StringToHash("Open");
        private readonly int CloseHash = Animator.StringToHash("Close");

        [SerializeField] private Animator _animator;
        [SerializeField] private InteractionReporter _reporter;

        private bool _processOfOpening;
        private bool _isClosed = true;

        private void Awake() =>
            _reporter.OnUse += Open;

        private void OpenProcessStop() =>//use animation invent
            _processOfOpening = false;

        private void Open()
        {
            if (_processOfOpening)
                return;

            _animator.Play(_isClosed ? OpenHash : CloseHash, 0, 0);
            _isClosed = !_isClosed;
            _processOfOpening = true;
        }
    }
}