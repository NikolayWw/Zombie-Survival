using UnityEditor;
using UnityEngine;

namespace CodeBase.Editor
{
    [CustomEditor(typeof(Transform))]
    public class TestEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            var myTarget = (Transform)target;
            if (GUILayout.Button("Null parent"))
            {
                myTarget.parent = null;
                EditorUtility.SetDirty(myTarget);
            }

            if (GUILayout.Button("RemoveLod"))
            {
                foreach (var w in myTarget.GetComponentsInChildren<Transform>())
                {
                    if (w.name.Contains("LOD"))
                    {
                        //Debug.Log("Contains");
                        DestroyImmediate(w.gameObject);
                    }
                }
                EditorUtility.SetDirty(myTarget);
            }
            base.OnInspectorGUI();
        }
    }
}