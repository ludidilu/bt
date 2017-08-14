using System;

namespace bt
{
    internal interface INode<T, U, V>
    {
        bool Enter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v);

        bool TryEnter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode);
    }
}
