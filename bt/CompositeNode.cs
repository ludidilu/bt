﻿using System.Collections.Generic;
using System;

namespace bt
{
    internal class CompositeNode<T, U, V> : INode<T, U, V>
    {
        protected List<INode<T, U, V>> children;

        private bool loop;

        internal void Init(List<INode<T, U, V>> _children, bool _loop)
        {
            children = _children;

            loop = _loop;
        }

        bool INode<T, U, V>.Enter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v)
        {
            if (loop)
            {
                bool result = false;

                while (EnterReal(_getRandomValueCallBack, _t, _u, _v))
                {
                    result = true;
                }

                return result;
            }
            else
            {
                return EnterReal(_getRandomValueCallBack, _t, _u, _v);
            }
        }

        protected virtual bool EnterReal(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v)
        {
            throw new NotImplementedException();
        }

        bool INode<T, U, V>.TryEnter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            throw new NotImplementedException();
        }
    }
}
