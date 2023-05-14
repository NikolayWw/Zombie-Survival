using UnityEngine;

namespace CodeBase.Props
{
    public class PropsFX : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public void Construct(float lifeTime, Vector3 directionAndForce)
        {
            _rigidbody.velocity = directionAndForce;
            Destroy(gameObject, lifeTime);
        }
    }
}