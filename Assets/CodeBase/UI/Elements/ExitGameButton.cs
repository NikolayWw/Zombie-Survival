using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class ExitGameButton : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;

        private void Start() =>
            _exitButton.onClick.AddListener(Application.Quit);
    }
}