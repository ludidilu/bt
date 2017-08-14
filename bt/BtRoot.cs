using System;

namespace bt
{
    public class BtRoot<T, U, V>
    {
        private INode<T, U, V> rootNode;

        internal void Init(INode<T, U, V> _rootNode)
        {
            rootNode = _rootNode;
        }

        public void Enter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v)
        {
            rootNode.Enter(_getRandomValueCallBack, _t, _u, _v);
        }
    }
}
