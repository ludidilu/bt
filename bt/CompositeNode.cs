using System.Collections.Generic;

namespace bt
{
    internal class CompositeNode<T, U, V>
    {
        protected List<INode<T, U, V>> children;

        internal void Init(List<INode<T, U, V>> _children)
        {
            children = _children;
        }
    }
}
