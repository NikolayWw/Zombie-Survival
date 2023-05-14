using CodeBase.UI.Services.Window;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class OpenWindowButton : MonoBehaviour
    {
        [SerializeField] private Button _openButton;
        [SerializeField] private WindowId _windowId;

        public void Construct(IWindowService windowService) =>
            _openButton.onClick.AddListener(() => windowService.Open(_windowId));
    }
}