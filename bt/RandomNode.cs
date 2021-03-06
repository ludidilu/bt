﻿using System.Collections.Generic;
using System;

namespace bt
{
    internal class RandomNode<T, U, V> : CompositeNode<T, U, V>, INode<T, U, V>
    {
        private List<int> randomValue;

        private List<ActionNode<T, U, V>> actionList = new List<ActionNode<T, U, V>>();

        private List<int> randomValueList = new List<int>();

        internal void InitRandomValue(List<int> _randomValue)
        {
            randomValue = _randomValue;
        }

        protected override bool EnterReal(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v)
        {
            for (int i = 0; i < children.Count; i++)
            {
                ActionNode<T, U, V> actionNode = null;

                if (children[i].TryEnter(_getRandomValueCallBack, _t, _u, _v, ref actionNode))
                {
                    if (actionNode != null)
                    {
                        actionList.Add(actionNode);

                        randomValueList.Add(randomValue[i]);
                    }
                }
            }

            if (actionList.Count > 0)
            {
                int d = 0;

                for (int i = 0; i < randomValueList.Count; i++)
                {
                    d += randomValueList[i];
                }

                int value = _getRandomValueCallBack(d);

                for (int i = 0; i < randomValueList.Count; i++)
                {
                    int v = randomValueList[i];

                    if (value < v)
                    {
                        actionList[i].Enter(_getRandomValueCallBack, _t, _u, _v);

                        break;
                    }
                    else
                    {
                        value -= v;
                    }
                }

                actionList.Clear();

                randomValueList.Clear();

                return true;
            }
            else
            {
                return false;
            }
        }

        bool INode<T, U, V>.TryEnter(Func<int, int> _getRandomValueCallBack, T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            for (int i = 0; i < children.Count; i++)
            {
                ActionNode<T, U, V> actionNode = null;

                if (children[i].TryEnter(_getRandomValueCallBack, _t, _u, _v, ref actionNode))
                {
                    if (actionNode != null)
                    {
                        actionList.Add(actionNode);

                        randomValueList.Add(randomValue[i]);
                    }
                }
            }

            if (actionList.Count > 0)
            {
                int d = 0;

                for (int i = 0; i < randomValueList.Count; i++)
                {
                    d += randomValueList[i];
                }

                int value = _getRandomValueCallBack(d);

                for (int i = 0; i < randomValueList.Count; i++)
                {
                    int v = randomValueList[i];

                    if (value < v)
                    {
                        _actionNode = actionList[i];

                        break;
                    }
                    else
                    {
                        value -= v;
                    }
                }

                actionList.Clear();

                randomValueList.Clear();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
