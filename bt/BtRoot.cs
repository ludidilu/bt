namespace bt
{
    public class BtRoot<T, U, V> where V : class, new()
    {
        private INode<T, U, V> rootNode;

        public void Init(INode<T, U, V> _rootNode)
        {
            rootNode = _rootNode;
        }

        public void Enter(T _t, U _u)
        {
            V v = new V();

            rootNode.Enter(_t, _u, v);
        }
    }
}
