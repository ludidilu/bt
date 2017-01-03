namespace bt
{
    public class BtRoot<T, U>
    {
        private INode<T, U> rootNode;

        public void Init(INode<T, U> _rootNode)
        {
            rootNode = _rootNode;
        }

        public void Enter(T _t, U _u)
        {
            rootNode.Enter(_t, _u);
        }
    }
}
