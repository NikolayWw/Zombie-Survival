using CodeBase.Data;
using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    public class CleanSaveEditor : UnityEditor.Editor
    {
        [MenuItem("Tools/Clean save")]
        private static void CleanSave()
        {
            if (Application.isPlaying)
                return;

            PlayerPrefs.DeleteKey(GameConstants.SaveProgressKey);
        }
    }
}