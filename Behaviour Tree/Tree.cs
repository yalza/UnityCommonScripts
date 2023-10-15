namespace DATA.Scripts.EnemiesAI.Behaviour_Tree
{
    public abstract class Tree
    {
        protected Node root;
        
        protected abstract Node SetupTree();
    }
}