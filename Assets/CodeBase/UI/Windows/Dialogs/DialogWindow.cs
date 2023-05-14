using TMPro;
using UnityEngine;

namespace CodeBase.UI.Windows.Dialogs
{
    public class DialogWindow : BaseWindow
    {
        [SerializeField] private TMP_Text _contextText;

        public void Refresh(in string context) =>
            _contextText.text = context;
    }
}