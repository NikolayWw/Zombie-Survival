using UnityEngine;

namespace CodeBase.Logic
{
    public class FollowTarget : MonoBehaviour
    {
        private Transform _target;

        public void Construct(Transform target) =>
            _target = target;

        private void LateUpdate()
        {
            if (_target)
                transform.position = _target.position;
        }
    }
}