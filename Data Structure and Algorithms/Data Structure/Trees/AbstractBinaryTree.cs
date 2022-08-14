using Data_Structure.Lists;

namespace Data_Structure.Trees
{
    public abstract class AbstractBinaryTree<E> : AbstractTree<E>
    {
        public abstract Position<E>? Left(Position<E> p);
        public abstract Position<E>? Right(Position<E> p);

        public Position<E>? Sibling(Position<E> p)
        {
            Position<E>? parent = Parent(p);
            if (parent == default) return default;   // p must be the root
            if (p == Left(parent))
                return Right(parent);
            else
                return Left(parent);
        }

        public override int NumChildren(Position<E> p)
        {
            int count = 0;
            if (Left(p) != default)
                count++;
            if (Right(p) != default)
                count++;
            return count;
        }
        public override IEnumerable<Position<E>> Children(Position<E> p)
        {
            var snapshot = new ArrayList<Position<E>>(2);    // max capacity of 2
            if (Left(p) != default)
                snapshot.Add(Left(p));
            if (Right(p) != default)
                snapshot.Add(Right(p));
            return snapshot;
        }

        private void InorderSubtree(Position<E> p, ArrayList<Position<E>> snapshot)
        {

            if (Left(p) != default)
                InorderSubtree(Left(p), snapshot);
            snapshot.Add(p);
            if (Right(p) != default)
                InorderSubtree(Right(p), snapshot);
        }

        public IEnumerable<Position<E>> Inorder()
        {
            var snapshot = new ArrayList<Position<E>>();
            if (!IsEmpty())
                InorderSubtree(Root, snapshot);   // fill the snapshot recursively
            return snapshot;
        }

        public override IEnumerable<Position<E>> Positions()
        {
            return Inorder();
        }
    }
}

