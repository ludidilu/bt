using System.Collections.Generic;
using System;

namespace bt
{
    public class RandomNode<T, U> : CompositeNode<T, U>
    {
        private Random random;

        private List<int> randomValue;

        public void InitRandomValue(Random _random, List<int> _randomValue)
        {
            random = _random;

            randomValue = _randomValue;
        }

        public override bool Enter(T _t, U _u)
        {
            List<ActionNode<T, U>> actionList = null;

            List<int> valueList = null;

            for (int i = 0; i < children.Count; i++)
            {
                ActionNode<T, U> actionNode = null;

                if (children[i].TryEnter(_t, _u, ref actionNode))
                {
                    if (actionList == null)
                    {
                        actionList = new List<ActionNode<T, U>>();

                        valueList = new List<int>();
                    }

                    actionList.Add(actionNode);

                    valueList.Add(randomValue[i]);
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
                        actionList[i].Enter(_t, _u);

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

        public override bool TryEnter(T _t, U _u, ref ActionNode<T, U> _actionNode)
        {
            List<ActionNode<T, U>> actionList = null;

            List<int> valueList = null;

            for (int i = 0; i < children.Count; i++)
            {
                ActionNode<T, U> actionNode = null;

                if (children[i].TryEnter(_t, _u, ref actionNode))
                {
                    if (actionList == null)
                    {
                        actionList = new List<ActionNode<T, U>>();

                        valueList = new List<int>();
                    }

                    actionList.Add(actionNode);

                    valueList.Add(randomValue[i]);
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
