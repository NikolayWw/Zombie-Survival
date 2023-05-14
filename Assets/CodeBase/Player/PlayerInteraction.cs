using CodeBase.Logic;
using CodeBase.Logic.Pause;
using CodeBase.Services.Input;
using CodeBase.Services.StaticData;
using CodeBase.StaticData.Player;
using System.Collections;
using UnityEngine;
using static UnityEngine.Screen;

namespace CodeBase.Player
{
    public class PlayerInteraction : MonoBehaviour, IFreeze
    {
        private InteractionReporter _previous;
        private IInputService _inputService;
        private UnityEngine.Camera _mainCamera;
        private PlayerConfig _config;

        public void Construct(IStaticDataService dataService, IInputService inputService, UnityEngine.Camera mainCamera)
        {
            _config = dataService.PlayerConfig;
            _inputService = inputService;
            _mainCamera = mainCamera;

            Subscribe(true);
        }

        private void Start()
        {
            StartCoroutine(Select());
        }

        private void OnDestroy()
        {
            _inputService.OnInteract -= Interact;
        }

        #region Pause

        public void Pause()
        { }

        public void Play()
        { }

        public void Freeze()
        {
            Subscribe(false);
            StopAllCoroutines();
        }

        public void Unfreeze()
        {
            Subscribe(true);
            StartCoroutine(Select());
        }

        #endregion Pause

        private void Subscribe(bool isSubscribe)
        {
            if (isSubscribe)
            {
                _inputService.OnInteract += Interact;
            }
            else
            {
                _inputService.OnInteract -= Interact;
            }
        }

        private void Interact()
        {
            if (_previous != null)
                _previous.UseSend();
        }

        private IEnumerator Select()
        {
            var wait = new WaitForSeconds(_config.InteractionDelaySelect);
            while (true)
            {
                Ray ray = _mainCamera.ScreenPointToRay(new Vector2(width / 2, height / 2));
                if (Physics.Raycast(ray, out RaycastHit hit, _config.InteractionSelectRayDistance, _config.InteractionLayerMask, QueryTriggerInteraction.Ignore) && hit.transform.TryGetComponent(out InteractionReporter interaction))
                {
                    if (_previous != interaction)
                    {
                        if (_previous != null)
                            _previous.DeselectSend();
                        interaction.SelectSend();
                        _previous = interaction;
                    }
                }
                else
                {
                    if (_previous != null)
                        _previous.DeselectSend();
                    _previous = null;
                }
                yield return wait;
            }
        }
    }
}