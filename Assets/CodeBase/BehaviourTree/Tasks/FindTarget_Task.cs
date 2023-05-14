using CodeBase.BehaviourTree.Tree;
using CodeBase.Logic.ApplyDamage;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class FindTarget_Task : Node
    {
        private readonly float _checkRadius;
        private readonly LayerMask _targetLayer;
        private readonly Transform _transform;
        private readonly Collider[] _targetColliders = new Collider[5];

        public FindTarget_Task(Transform transform, LayerMask targetLayer, float checkRadius)
        {
            _transform = transform;
            _checkRadius = checkRadius;
            _targetLayer = targetLayer;
        }

        public override bool Evaluated()
        {
            IApplyDamage target = GetTarget();
            if (target == null)
            {
                int colliderCount = Physics.OverlapSphereNonAlloc(_transform.position, _checkRadius, _targetColliders, _targetLayer);
                for (int i = 0; i < colliderCount; i++)
                {
                    if (_targetColliders[i].TryGetComponent(out IApplyDamage applyDamage))
                    {
                        Parent.Parent.SetTarget(applyDamage, _targetColliders[i].transform);
                        return true;
                    }
                }
                return false;
            }
            return true;
        }
    }
}