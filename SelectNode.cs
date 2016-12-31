using System.Collections.Generic;

namespace bt
{
    public class SelectNode : CompositeNode
    {
        public override bool Enter()
        {
            LinkedList<INode>.Enumerator enumerator = children.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Enter())
                {
                    return true;
                }
            }

            return false;
        }
    }
}
