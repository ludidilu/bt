namespace bt
{
    public class ActionNode<T, U> : INode<T, U>
    {
        public virtual bool Enter(T _t, U _u)
        {
            return true;
        }

        public bool TryEnter(T _t, U _u, ref ActionNode<T, U> _actionNode)
        {
            _actionNode = this;

            return true;
        }
    }
}
