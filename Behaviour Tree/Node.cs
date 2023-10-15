using System.Collections.Generic;

namespace DATA.Scripts.EnemiesAI.Behaviour_Tree
{
    
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }
    public class Node
    {
        protected NodeState state;
        public Node parent;
        
        protected readonly List<Node> children = new List<Node>();
        
        private readonly Dictionary<string,object> _data = new Dictionary<string, object>();

        protected Node()
        {
            parent = null;
        }

        protected Node(List<Node> children)
        {
            foreach (var node in children)
            {
                node.parent = this;
                this.children.Add(node);
            }
        }
        
        protected virtual NodeState Evaluate()
        {
            return NodeState.Failure;
        }

        protected void SetData(string key, object value)
        {
            _data[key] = value;
        } 
        
        protected object GetData(string key)
        {
            if(_data.TryGetValue(key, out var value))
                return value;
            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.parent;
            }

            return null;
        }

        protected bool RemoveData(string key)
        {
            if (_data.ContainsKey(key))
            {
                _data.Remove(key);
                return true;
            }
            
            Node node = parent;

            while (node != null)
            {
                bool removed = node.RemoveData(key);
                if (removed) return true;
                node = node.parent;
            }

            return false;
        }
        
    }
}
