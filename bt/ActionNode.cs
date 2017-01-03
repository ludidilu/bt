namespace bt
{
    public class ActionNode<T, U, V> : INode<T, U, V> where V : new()
    {
        public virtual bool Enter(T _t, U _u, V _v)
        {
            return true;
        }

        public bool TryEnter(T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode)
        {
            _actionNode = this;

            return true;
        }
    }
}
