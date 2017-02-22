using System.Collections.Generic;

namespace bt
{
    public class CompositeNode<T, U, V> : INode<T, U, V>
    {
        protected List<INode<T, U, V>> children;

        public void Init(List<INode<T, U, V>> _children)
        {
            children = _children;
        }

        public virtual bool Enter(T _t, U _u, V _v)
        {
            return false;
        }

        public virtual bool TryEnter(T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            return false;
        }
    }
}
