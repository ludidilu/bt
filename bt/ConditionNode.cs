namespace bt
{
    public class ConditionNode<T, U> : INode<T, U>
    {
        public virtual bool Enter(T _t, U _u)
        {
            return false;
        }

        public bool TryEnter(T _t, U _u, ref ActionNode<T, U> _actionNode)
        {
            return Enter(_t, _u);
        }
    }
}
