using Data_Structure.Lists;

namespace Data_Structure.Trees
{
    public abstract class AbstractBinaryTree<E> : AbstractTree<E>
    {
        public abstract Node<E>? Left(Node<E> p);
        public abstract Node<E>? Right(Node<E> p);

        public Node<E>? Sibling(Node<E> p)
        {
            Node<E>? parent = Parent(p);
            if (parent == default) return default;   // p must be the root
            if (p == Left(parent))
                return Right(parent);
            else
                return Left(parent);
        }

        public override int NumChildren(Node<E> p)
        {
            int count = 0;
            if (Left(p) != default)
                count++;
            if (Right(p) != default)
                count++;
            return count;
        }
        public override IEnumerable<Node<E>> Children(Node<E> p)
        {
            var snapshot = new ArrayList<Node<E>>(2);    // max capacity of 2
            if (Left(p) != default)
                snapshot.Add(Left(p));
            if (Right(p) != default)
                snapshot.Add(Right(p));
            return snapshot;
        }

        private void InorderSubtree(Node<E> p, ArrayList<Node<E>> snapshot)
        {

            if (Left(p) != default)
                InorderSubtree(Left(p), snapshot);
            snapshot.Add(p);
            if (Right(p) != default)
                InorderSubtree(Right(p), snapshot);
        }

        public IEnumerable<Node<E>> Inorder()
        {
            var snapshot = new ArrayList<Node<E>>();
            if (!IsEmpty())
                InorderSubtree(Root, snapshot);   // fill the snapshot recursively
            return snapshot;
        }

        public override IEnumerable<Node<E>> Positions()
        {
            return Inorder();
        }
    }
}

