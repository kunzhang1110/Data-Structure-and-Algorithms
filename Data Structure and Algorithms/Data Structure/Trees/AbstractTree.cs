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
                foreach (Node<E> child in Positions()) count++;
                return count;
            }
            set { }
        }

        public abstract Node<E>? Root { get; set; }
        public abstract Node<E>? Parent(Node<E> p);
        public abstract IEnumerable<Node<E>> Children(Node<E> p);

        public virtual int NumChildren(Node<E> p)
        {
            int count = 0;
            foreach (Node<E> child in Children(p)) count++;
            return count;
        }

        public int Depth(Node<E> p)
        {
            if (IsRoot(p))
                return 0;
            else
                return 1 + Depth(Parent(p));
        }

        public int Height(Node<E> p)
        {
            int h = 0;                          // base case if p is external
            foreach (Node<E> c in Children(p))
                h = Math.Max(h, 1 + Height(c));
            return h;
        }

        public bool IsInternal(Node<E> p) { return NumChildren(p) > 0; }
        public bool IsExternal(Node<E> p) { return NumChildren(p) == 0; }
        public bool IsRoot(Node<E> p) { return p == Root; }
        public bool IsEmpty() { return Size == 0; }


        private void PreorderSubtree(Node<E> p, ArrayList<Node<E>> snapshot)
        {
            snapshot.Add(p);                       // for preorder, we add position p before exploring subtrees
            foreach (Node<E> c in Children(p))
                PreorderSubtree(c, snapshot);
        }

        private IEnumerable<Node<E>> Preorder()
        {
            var snapshot = new ArrayList<Node<E>>();
            if (!IsEmpty())
                PreorderSubtree(Root, snapshot);   // fill the snapshot recursively
            return snapshot;
        }

        public virtual IEnumerable<Node<E>> Positions() { return Preorder(); }

        public IEnumerable<Node<E>> Breadthfirst()
        {// Returns an iterable collection of positions of the tree in breadth-first order.
            var snapshot = new ArrayList<Node<E>>();
            if (!IsEmpty())
            {
                var fringe = new Data_Structure.Lists.Queue<Node<E>>();
                fringe.Enqueue(Root);                 // start with the root
                while (!fringe.IsEmpty())
                {
                    Node<E>? p = fringe.Dequeue();     // remove from front of the queue
                    snapshot.Add(p);                      // report this position
                    foreach (Node<E> c in Children(p))
                        fringe.Enqueue(c);                  // add children to back of queue
                }
            }
            return snapshot;
        }


        //IEnumerable<E> Implmentation
        public class ElementEnumerator : IEnumerator<E>
        {
            public IEnumerator<Node<E>> PositionEnumerator;
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

