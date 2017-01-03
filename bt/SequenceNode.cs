﻿using System.Collections.Generic;

namespace bt
{
    public class SequenceNode<T, U> : CompositeNode<T, U>
    {
        public override bool Enter(T _t, U _u)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (!children[i].Enter(_t, _u))
                {
                    return false;
                }
            }

            return true;
        }

        public override bool TryEnter(T _t, U _u, ref ActionNode<T, U> _actionNode)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (!children[i].TryEnter(_t, _u, ref _actionNode))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
