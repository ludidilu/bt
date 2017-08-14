using System;

namespace bt
{
    public class ActionNode<T, U, V> : INode<T, U, V>
    {
        public virtual bool Enter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v)
        {
            return true;
        }

        bool INode<T, U, V>.TryEnter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            _actionNode = this;

            return true;
        }
    }
}
