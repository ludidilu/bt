using System.Collections.Generic;
using System;

namespace bt
{
    public class RandomNode<T, U, V> : CompositeNode<T, U, V>
    {
        private Random random;

        private List<int> randomValue;

        internal void InitRandomValue(Random _random, List<int> _randomValue)
        {
            random = _random;

            randomValue = _randomValue;
        }

        public override bool Enter(T _t, U _u, V _v)
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

                int value = random.Next(d);

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

        public override bool TryEnter(T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
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

                int value = random.Next(d);

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
