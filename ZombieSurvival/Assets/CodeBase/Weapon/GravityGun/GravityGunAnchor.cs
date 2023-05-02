using UnityEngine;

namespace CodeBase.Weapon.GravityGun
{
    public class GravityGunAnchor : MonoBehaviour
    {
        [field: SerializeField] public Rigidbody Rigidbody { get; private set; }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}