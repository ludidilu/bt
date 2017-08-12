namespace bt
{
    internal class SequenceNode<T, U, V> : CompositeNode<T, U, V>, INode<T, U, V>
    {
        protected override bool EnterReal(T _t, U _u, V _v)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (!children[i].Enter(_t, _u, _v))
                {
                    return false;
                }
            }

            return true;
        }

        bool INode<T, U, V>.TryEnter(T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            for (int i = 0; i < children.Count; i++)
            {
                if (!children[i].TryEnter(_t, _u, _v, ref _actionNode))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
