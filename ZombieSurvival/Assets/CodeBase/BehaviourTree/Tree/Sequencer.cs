using System.Collections.Generic;

namespace CodeBase.BehaviourTree.Tree
{
    public class Sequencer : Node
    {
        public Sequencer(List<Node> children) : base(children)
        { }

        public override bool Evaluated()
        {
            foreach (var child in Children)
            {
                if (child.Evaluated() == false)
                    return false;
            }
            return true;
        }
    }
}