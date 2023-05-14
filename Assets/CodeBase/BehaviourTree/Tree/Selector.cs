using System.Collections.Generic;

namespace CodeBase.BehaviourTree.Tree
{
    public class Selector : Node
    {
        public Selector(List<Node> children) : base(children)
        { }

        public override bool Evaluated()
        {
            foreach (var child in Children)
            {
                if (child.Evaluated())
                    return true;
            }
            return false;
        }
    }
}