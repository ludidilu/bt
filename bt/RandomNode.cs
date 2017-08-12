using System.Collections.Generic;
using System;

namespace bt
{
    internal class RandomNode<T, U, V> : CompositeNode<T, U, V>, INode<T, U, V>
    {
        private Func<int, int> getRandomValueCallBack;

        private List<int> randomValue;

        internal void InitRandomValue(Func<int, int> _getRandomValueCallBack, List<int> _randomValue)
        {
            getRandomValueCallBack = _getRandomValueCallBack;

            randomValue = _randomValue;
        }

        bool INode<T, U, V>.Enter(T _t, U _u, V _v)
        {
            List<ActionNode<T, U, V>> actionList = null;

            List<int> valueList = null;

            for (int i = 0; i < children.Count; i++)
            {
                ActionNode<T, U, V> actionNode = null;

                if (children[i].TryEnter(_t, _u, _v, ref actionNode))
                {
                    if (actionNode != null)
                    {
                        if (actionList == null)
                        {
                            actionList = new List<ActionNode<T, U, V>>();

                            valueList = new List<int>();
                        }

                        actionList.Add(actionNode);

                        valueList.Add(randomValue[i]);
                    }
                }
            }

            if (actionList != null)
            {
                int d = 0;

                for (int i = 0; i < valueList.Count; i++)
                {
                    d += valueList[i];
                }

                int value = getRandomValueCallBack(d);

                for (int i = 0; i < valueList.Count; i++)
                {
                    int v = valueList[i];

                    if (value < v)
                    {
                        actionList[i].Enter(_t, _u, _v);

                        break;
                    }
                    else
                    {
                        value -= v;
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        bool INode<T, U, V>.TryEnter(T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            List<ActionNode<T, U, V>> actionList = null;

            List<int> valueList = null;

            for (int i = 0; i < children.Count; i++)
            {
                ActionNode<T, U, V> actionNode = null;

                if (children[i].TryEnter(_t, _u, _v, ref actionNode))
                {
                    if (actionNode != null)
                    {
                        if (actionList == null)
                        {
                            actionList = new List<ActionNode<T, U, V>>();

                            valueList = new List<int>();
                        }

                        actionList.Add(actionNode);

                        valueList.Add(randomValue[i]);
                    }
                }
            }

            if (actionList != null)
            {
                int d = 0;

                for (int i = 0; i < valueList.Count; i++)
                {
                    d += valueList[i];
                }

                int value = getRandomValueCallBack(d);

                for (int i = 0; i < valueList.Count; i++)
                {
                    int v = valueList[i];

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

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
