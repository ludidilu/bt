using System.Collections.Generic;

namespace bt
{
    public class CompositeNode<T, U> : INode<T, U>
    {
        protected List<INode<T, U>> children;

        public void Init(List<INode<T, U>> _children)
        {
            children = _children;
        }

        public virtual bool Enter(T _t, U _u)
        {
            return false;
        }

        public virtual bool TryEnter(T _t, U _u, ref ActionNode<T, U> _actionNode)
        {
            return false;
        }
    }
}
