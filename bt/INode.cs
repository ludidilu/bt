namespace bt
{
    public interface INode<T, U, V>
    {
        bool Enter(T _t, U _u, V _v);

        bool TryEnter(T _t, U _u, V _v, ref ActionNode<T, U, V> _actionNode);
    }
}
