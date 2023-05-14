using UnityEngine;

namespace CodeBase.Logic.ObjectPool
{
    public class FXShotObject : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particleSystem;
        public bool IsReady => _particleSystem.gameObject.activeInHierarchy == false;

        public void Enable()
        {
            _particleSystem.gameObject.SetActive(true);
            _particleSystem.Play();
        }

        public void ParentDestroy()
        {
            transform.parent = null;
            _particleSystem.gameObject.SetActive(false);
        }
    }
}