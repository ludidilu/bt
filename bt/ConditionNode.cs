namespace bt
{
    public class ConditionNode<T, U, V> : INode<T, U, V>
    {
        public virtual bool Enter(T _t, U _u, V _v)
        {
            return false;
        }

        bool INode<T, U, V>.TryEnter(T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            return Enter(_t, _u, _v);
        }
    }
}
