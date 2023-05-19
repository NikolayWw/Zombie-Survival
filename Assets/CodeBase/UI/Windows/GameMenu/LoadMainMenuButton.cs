using CodeBase.Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.GameMenu
{
    public class LoadMainMenuButton : MonoBehaviour
    {
        [SerializeField] private Button _loadMainMenuButton;

        public void Construct(IGameStateMachine gameStateMachine)
        {
            _loadMainMenuButton.onClick.AddListener(gameStateMachine.Enter<LoadMainMenuState>);
        }
    }
}