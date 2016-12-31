using System.Collections.Generic;

namespace bt
{
    public class SequenceNode : CompositeNode
    {
        public override bool Enter()
        {
            LinkedList<INode>.Enumerator enumerator = children.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (!enumerator.Current.Enter())
                {
                    return false;
                }
            }

            return true;
        }
    }
}
