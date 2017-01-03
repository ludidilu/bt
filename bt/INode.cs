namespace bt
{
    public interface INode<T, U>
    {
        bool Enter(T _t, U _u);

        bool TryEnter(T _t, U _u, ref ActionNode<T, U> _actionNode);
    }
}
