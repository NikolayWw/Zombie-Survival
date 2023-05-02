using UnityEditor;
using UnityEngine;

public class Test : MonoBehaviour
{
    [ContextMenu("Run")]
    private void Run()
    {
        foreach (var automaticLOD in FindObjectsOfType<GameObject>())
            foreach (var child in automaticLOD.GetComponentsInChildren<LODGroup>())
            {
                DestroyImmediate(child);
                EditorUtility.SetDirty(automaticLOD);
            }
    }
}