namespace Data_Structure.Trees
{
    public class AVLTreeMap<K, V> : TreeMap<K, V>
{
    public int Height(Node<Entry<K, V>> p)
    {
        return tree.GetAux(p);
    }

    public void RecomputeHeight(Node<Entry<K, V>> p)
    {
        tree.SetAux(p, 1 + Math.Max(Height(tree.Left(p)), Height(tree.Right(p))));
    }

    public bool IsBalanced(Node<Entry<K, V>> p)
    {
        return Math.Abs(Height(tree.Left(p)) - Height(tree.Right(p))) <= 1;
    }

    //Returns a child of p with Height no smaller than that of the other child.
    public Node<Entry<K, V>>? TallerChild(Node<Entry<K, V>> p)
    {
        if (Height(tree.Left(p)) > Height(tree.Right(p))) return tree.Left(p);
        if (Height(tree.Left(p)) < Height(tree.Right(p))) return tree.Right(p);
        // equal Height children; break tie while matching parent's orientation
        if (tree.IsRoot(p)) return tree.Left(p);
        if (p == tree.Left(tree.Parent(p))) return tree.Left(p);    // return aligned child
        else return tree.Right(p);
    }

    public void Rebalance(Node<Entry<K, V>> p)
    {
        int oldHeight, newHeight;
        do
        {
            oldHeight = Height(p);
            if (!IsBalanced(p))
            {   // perform trinode restructuring, setting p to resulting root,
                // and recompute new local heights after the restructuring
                p = tree.Restructure(TallerChild(TallerChild(p)));
                RecomputeHeight(tree.Left(p));
                RecomputeHeight(tree.Right(p));
            }
            RecomputeHeight(p);
            newHeight = Height(p);
            p = tree.Parent(p);
        } while (oldHeight != newHeight && p != null);
    }
    public override void RebalanceInsert(Node<Entry<K, V>> p)
    {
        Rebalance(p);
    }

    public override void RebalanceDelete(Node<Entry<K, V>> p)
    {
        if (!tree.IsRoot(p))
            Rebalance(tree.Parent(p));
    }
}
}

