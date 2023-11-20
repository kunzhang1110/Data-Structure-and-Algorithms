namespace Data_Structure.Trees
{
    public class SplayTreeMap<K, V> : TreeMap<K, V>
{
    public SplayTreeMap(Comparer<K> comp) : base(comp) { }
    public SplayTreeMap() : this(Comparer<K>.Default) { }

    private void Splay(Node<Entry<K, V>> p)
    {
        while (!tree.IsRoot(p))
        {
            Node<Entry<K, V>> parent = tree.Parent(p);
            Node<Entry<K, V>> grand = tree.Parent(parent);
            if (grand == null)// zig case
                tree.Rotate(p);
            else if ((parent == tree.Left(grand)) == (p == tree.Left(parent)))
            {  // zig-zig case
                tree.Rotate(parent);      // move PARENT upward
                tree.Rotate(p);           // then move p upward
            }
            else
            {// zig-zag case
                tree.Rotate(p);           // move p upward
                tree.Rotate(p);           // move p upward again
            }
        }
    }
    public override void RebalanceAccess(Node<Entry<K, V>> p)
    {
        if (tree.IsExternal(p)) p = tree.Parent(p);
        if (p != null) Splay(p);
    }

    public override void RebalanceInsert(Node<Entry<K, V>> p)
    {
        Splay(p);
    }

    public override void RebalanceDelete(Node<Entry<K, V>> p)
    {
        if (!tree.IsRoot(p))
            Splay(tree.Parent(p));
    }
}
}

