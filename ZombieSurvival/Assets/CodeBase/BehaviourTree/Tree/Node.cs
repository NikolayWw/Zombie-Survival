using CodeBase.Logic.ApplyDamage;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.BehaviourTree.Tree
{
    public class Node
    {
        protected IApplyDamage Target;
        protected Transform TargetTransform;
        protected List<Node> Children = new List<Node>();
        public Node Parent;

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (var child in children)
                SetParents(child);
        }

        private void SetParents(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        public void SetTarget(IApplyDamage target, Transform targetTransform)
        {
            Target = target;
            TargetTransform = targetTransform;
        }

        public IApplyDamage GetTarget()
        {
            return Target ?? Parent?.GetTarget();
        }

        public Transform GetTargetTransform()
        {
            return TargetTransform ?? Parent?.GetTargetTransform();
        }

        public void ClearData()
        {
            if (Target != null)
            {
                Target = null;
                TargetTransform = null;
            }
            else
                Parent?.ClearData();
        }

        public virtual bool Evaluated() => false;
    }
}