using CodeBase.UI.Services.Window;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Elements
{
    public class CloseWindowButton : MonoBehaviour
    {
        [SerializeField] private WindowId _id;
        [SerializeField] private Button _closeButton;

        private IWindowService _service;

        public void Construct(IWindowService service)
        {
            _service = service;
            _closeButton.onClick.AddListener(() => _service.Close(_id));
        }
    }
}