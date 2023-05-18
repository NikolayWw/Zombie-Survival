using CodeBase.Services.Input;
using CodeBase.UI.Services.Window;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class OpenGameMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _openButton;

        private IWindowService _windowService;
        private IInputService _inputService;

        public void Construct(IWindowService windowService, IInputService inputService)
        {
            _windowService = windowService;
            _inputService = inputService;

            _inputService.OnOpenGameMenu += OpenOrClose;
            _openButton.onClick.AddListener(OpenOrClose);
        }

        private void OnDestroy() =>
            _inputService.OnOpenGameMenu -= OpenOrClose;

        private void OpenOrClose()
        {
            if (_windowService.IsWindowOpened(WindowId.GameMenuWindow))
                _windowService.Close(WindowId.GameMenuWindow);
            else
                _windowService.Open(WindowId.GameMenuWindow);
        }
    }
}