using System.Collections.Generic;

namespace DATA.Scripts.EnemiesAI.Behaviour_Tree
{
    
    
    public class Selector : Node
    {
        public Selector() : base()
        {
        }
        
        public Selector(List<Node> children) : base(children)
        {
        }

        public override NodeState Evaluate()
        {
            foreach (var node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        continue;
                    case NodeState.Success:
                        return NodeState.Success;
                    case NodeState.Running:
                        return NodeState.Running;
                    default:
                        continue;
                }
            }

            return NodeState.Failure;
        }
    }
}