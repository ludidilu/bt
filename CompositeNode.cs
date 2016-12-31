using System.Collections.Generic;

namespace bt
{
    public class CompositeNode : INode
    {
        protected LinkedList<INode> children;

        public void Init(LinkedList<INode> _children)
        {
            children = _children;
        }

        public virtual bool Enter()
        {
            return false;
        }
    }
}
