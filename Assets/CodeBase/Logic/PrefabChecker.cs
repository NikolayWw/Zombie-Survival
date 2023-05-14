using UnityEngine;

namespace CodeBase.Logic
{
    public static class PrefabChecker
    {
        public static bool IsPrefab(GameObject gameObject) =>
            gameObject.scene.rootCount == 0;
    }
}