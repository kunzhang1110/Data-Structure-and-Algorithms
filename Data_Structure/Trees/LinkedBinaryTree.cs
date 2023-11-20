namespace Data_Structure.Trees
{
    public class LinkedBinaryTree<E> : AbstractBinaryTree<E>
{
    public override Node<E>? Root { get; set; }     // root of the tree
    public override int Size { get; set; }
        
    protected Node<E> Validate(Node<E> p)
    {
        if (p is not Node<E>)
            throw new Exception("Not valid position type");
        Node<E> node = (Node<E>)p;       // safe cast
        if (node.Parent == node)     // convention for defunct node
            throw new Exception("p is no longer in the tree");
        return node;
    }

    public virtual Node<E> CreateNode(E e, Node<E>? parent,
                            Node<E>? left, Node<E>? right)
    {
        return new Node<E>(e, parent, left, right);
    }

    public override Node<E>? Parent(Node<E> p)
    {
        var node = Validate(p);
        return node.Parent;
    }

    public override Node<E>? Left(Node<E> p)
    {
        Node<E> node = Validate(p);
        return node.Left;
    }

    public override Node<E>? Right(Node<E> p)
    {
        var node = Validate(p);
        return node.Right;
    }

    public Node<E> AddRoot(E? e)
    {

        if (!IsEmpty()) throw new Exception("Tree is not empty");
        Root = CreateNode(e, default, default, default);
        Size = 1;
        return Root;
    }

    public Node<E>? AddLeft(Node<E> p, E? e)
    {
        var parent = Validate(p);
        if (parent.Left != null)
            throw new Exception("p already has a left child");
        var child = CreateNode(e, parent, default, default);
        parent.Left = child;
        Size++;
        return child;
    }

    public Node<E>? AddRight(Node<E> p, E? e)
    {
        var parent = Validate(p);
        if (parent.Right != null)
            throw new Exception("p already has a left child");
        var child = CreateNode(e, parent, default, default);
        parent.Right = child;
        Size++;
        return child;
    }

    public E? Set(Node<E> p, E e)
    {
        var node = Validate(p);
        E? temp = node.Element;
        node.Element = e;
        return temp;
    }

    public E? Remove(Node<E> p)
    {
        var node = Validate(p);
        if (NumChildren(p) == 2)
            throw new Exception("p has two children");
        Node<E>? child = (node.Left != default ? node.Left : node.Right);
        if (child != default)
            child.Parent = node.Parent; // child's grandparent becomes its parent
        if (node == Root)
            Root = child;                       // child becomes root
        else
        {
            var parent = node.Parent;
            if (node == parent!.Left)
                parent.Left = child;
            else
                parent.Right = child;
        }
        Size--;
        return node.Element;
    }

    public void Attach(Node<E> p, LinkedBinaryTree<E> t1, LinkedBinaryTree<E> t2)
    {
        var node = Validate(p);
        if (IsInternal(p)) throw new Exception("p must be a leaf");
        Size += t1.Size + t2.Size;
        if (!t1.IsEmpty())
        {// attach t1 as left subtree of node
            t1.Root!.Parent = node;
            node.Left = (Node<E>)t1.Root;
        }
        if (!t2.IsEmpty())
        { // attach t2 as right subtree of node
            t2.Root!.Parent = node;
            node.Right = (Node<E>)t2.Root;
        }
    }
}
}

