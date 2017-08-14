using System;

namespace bt
{
    internal class SelectNode<T, U, V> : CompositeNode<T, U, V>, INode<T, U, V>
    {
        protected override bool EnterReal(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].Enter(_getRandomValueCallBack, _t, _u, _v))
                {
                    return true;
                }
            }

            return false;
        }

        bool INode<T, U, V>.TryEnter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (children[i].TryEnter(_getRandomValueCallBack, _t, _u, _v, ref _actionNode))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
