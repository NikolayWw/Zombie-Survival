using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.StaticData.Windows
{
    [CreateAssetMenu(menuName = "Static Data/Window Config Container", fileName = "WindowConfigsContainer")]
    public class WindowConfigsContainer : ScriptableObject
    {
        [field: SerializeField] public List<WindowConfig> Configs { get; private set; }

        private void OnValidate()
        {
            Configs.ForEach(x => x.Validate());
        }
    }
}