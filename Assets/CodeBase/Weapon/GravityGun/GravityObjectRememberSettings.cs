using UnityEngine;

namespace CodeBase.Weapon.GravityGun
{
    public struct GravityObjectRememberSettings
    {
        [field: SerializeField] public float RigidbodyAngularDrag { get; private set; }
        [field: SerializeField] public float RigidbodyDrag { get; private set; }

        public GravityObjectRememberSettings(float rigidbodyAngularDrag, float rigidbodyDrag)
        {
            RigidbodyAngularDrag = rigidbodyAngularDrag;
            RigidbodyDrag = rigidbodyDrag;
        }
    }
}