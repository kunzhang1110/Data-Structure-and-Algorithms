using Data_Structure.Lists;

namespace Data_Structure.Trees
{
    public abstract class AbstractTree<E> : IEnumerable<E>
    {
        public virtual int Size
        {
            get
            {
                int count = 0;
                foreach (Position<E> child in Positions()) count++;
                return count;
            }
            set { }
        }

        public abstract Position<E>? Root { get; set; }
        public abstract Position<E>? Parent(Position<E> p);
        public abstract IEnumerable<Position<E>> Children(Position<E> p);

        public virtual int NumChildren(Position<E> p)
        {
            int count = 0;
            foreach (Position<E> child in Children(p)) count++;
            return count;
        }

        public int Depth(Position<E> p)
        {
            if (IsRoot(p))
                return 0;
            else
                return 1 + Depth(Parent(p));
        }

        public int Height(Position<E> p)
        {
            int h = 0;                          // base case if p is external
            foreach (Position<E> c in Children(p))
                h = Math.Max(h, 1 + Height(c));
            return h;
        }

        public bool IsInternal(Position<E> p) { return NumChildren(p) > 0; }
        public bool IsExternal(Position<E> p) { return NumChildren(p) == 0; }
        public bool IsRoot(Position<E> p) { return p == Root; }
        public bool IsEmpty() { return Size == 0; }


        private void PreorderSubtree(Position<E> p, ArrayList<Position<E>> snapshot)
        {
            snapshot.Add(p);                       // for preorder, we add position p before exploring subtrees
            foreach (Position<E> c in Children(p))
                PreorderSubtree(c, snapshot);
        }

        private IEnumerable<Position<E>> Preorder()
        {
            var snapshot = new ArrayList<Position<E>>();
            if (!IsEmpty())
                PreorderSubtree(Root, snapshot);   // fill the snapshot recursively
            return snapshot;
        }

        public virtual IEnumerable<Position<E>> Positions() { return Preorder(); }

        public IEnumerable<Position<E>> Breadthfirst()
        {// Returns an iterable collection of positions of the tree in breadth-first order.
            var snapshot = new ArrayList<Position<E>>();
            if (!IsEmpty())
            {
                var fringe = new Data_Structure.Lists.Queue<Position<E>>();
                fringe.Enqueue(Root);                 // start with the root
                while (!fringe.IsEmpty())
                {
                    Position<E>? p = fringe.Dequeue();     // remove from front of the queue
                    snapshot.Add(p);                      // report this position
                    foreach (Position<E> c in Children(p))
                        fringe.Enqueue(c);                  // add children to back of queue
                }
            }
            return snapshot;
        }


        //IEnumerable<E> Implmentation
        public class ElementEnumerator : IEnumerator<E>
        {
            public IEnumerator<Position<E>> PositionEnumerator;
            private readonly AbstractTree<E> tree;
            public ElementEnumerator(AbstractTree<E> tree)
            {
                this.tree = tree;
                PositionEnumerator = this.tree.Positions().GetEnumerator();

            }

            public E Current => PositionEnumerator.Current.Element!;

            object IEnumerator.Current => Current!;

            public void Dispose() { }

            public bool MoveNext() => PositionEnumerator.MoveNext();
            public void Reset() => throw new NotImplementedException();
        }

        public IEnumerator<E> GetEnumerator() { return new ElementEnumerator(this); }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

    }
}

