using System.Collections.Generic;

namespace DATA.Scripts.EnemiesAI.Behaviour_Tree
{
    public class Sequence : Node
    {
        public Sequence() : base()
        {
        }
        
        public Sequence(List<Node> children) : base(children)
        {
        }

        public override NodeState Evaluate()
        {
            bool isAnyChildRunning = false;
            foreach (var node  in children)
            {
                switch (node.Evaluate())
                {
                    case NodeState.Failure:
                        return NodeState.Failure;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        isAnyChildRunning = true;
                        continue;   
                    default:
                        return NodeState.Success;
                }
            }
            return isAnyChildRunning ? NodeState.Running : NodeState.Success;
        }
    }
}