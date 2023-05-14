using UnityEngine;

namespace CodeBase.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path) =>
            Object.Instantiate(Resources.Load<GameObject>(path));

        public GameObject Instantiate(string path, Transform parent) =>
            Object.Instantiate(Resources.Load<GameObject>(path), parent);

        public GameObject Instantiate(string path, Vector3 at, Transform parent)
        {
            GameObject instantiate = Object.Instantiate(Resources.Load<GameObject>(path), parent);
            instantiate.transform.position = at;
            return instantiate;
        }

        public GameObject Instantiate(string path, Vector3 at, Quaternion rotate) =>
             Object.Instantiate(Resources.Load<GameObject>(path), at, rotate);
    }
}