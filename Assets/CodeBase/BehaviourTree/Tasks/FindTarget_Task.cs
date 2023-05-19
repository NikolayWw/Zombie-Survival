using CodeBase.BehaviourTree.Tree;
using CodeBase.Logic.ApplyDamage;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tasks
{
    public class FindTarget_Task : Node
    {
        private readonly float _delayCheck;
        private readonly float _checkRadius;
        private readonly LayerMask _targetLayer;
        private readonly Transform _transform;
        private readonly Collider[] _targetColliders = new Collider[5];
        private float _currentTime;
        public FindTarget_Task(Transform transform, LayerMask targetLayer, float checkRadius,float delayCheck)
        {
            _checkRadius = checkRadius;
            _delayCheck = delayCheck;
            _transform = transform;
            _targetLayer = targetLayer;
        }

        public override bool Evaluated()
        {
            if (_currentTime < _delayCheck)
            {
                _currentTime += Time.deltaTime;
                return false;
            }

            _currentTime = 0;

            IApplyDamage target = GetTarget();
            if (target != null)
                return true;

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
    }
}