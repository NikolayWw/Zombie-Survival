using CodeBase.UI.Services.Window;
using CodeBase.UI.Windows;
using System;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [Serializable]
    public class WindowConfig
    {
        [SerializeField] private string _inspectorName = string.Empty;
        [field: SerializeField] public WindowId WindowId { get; private set; }
        [field: SerializeField] public BaseWindow Template { get; private set; }

        public void Validate() =>
            _inspectorName = WindowId.ToString();
    }
}