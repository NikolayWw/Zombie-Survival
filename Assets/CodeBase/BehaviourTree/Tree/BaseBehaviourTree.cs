using UnityEngine;

namespace CodeBase.BehaviourTree.Tree
{
    public abstract class BaseBehaviourTree : MonoBehaviour
    {
        private Node _root;

        private void Start()
        {
            _root = SetupTree();
        }

        private void Update()
        {
            _root.Evaluated();
        }

        public abstract Node SetupTree();
    }
}