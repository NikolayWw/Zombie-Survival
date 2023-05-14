using CodeBase.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Windows.MainMenu
{
    public class MainMenuWindow : BaseWindow
    {
        [SerializeField] private Button _resumeButton;

        public void Construct(ISaveLoadService saveLoad)
        {
            _resumeButton.interactable = saveLoad.LoadProgress() != null;
        }
    }
}