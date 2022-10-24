using Data_Structure.Maps;
using Data_Structure.Lists;

namespace Data_Structure.Trees
{
    public class TreeMap<K, V> : AbstractSortedMap<K, V>
    {
        public BalanceableBinaryTree<K, V> tree = new();

        public int Size { get => tree.Size - 1 / 2; }

        public TreeMap(Comparer<K> comp) : base(comp) { tree.AddRoot(default); }
        public TreeMap() : this(Comparer<K>.Default) { }

        public virtual void RebalanceInsert(Node<Entry<K, V>> p) { } //for balanced tree subclass
        public virtual void RebalanceDelete(Node<Entry<K, V>> p) { } //for balanced tree subclass
        public virtual void RebalanceAccess(Node<Entry<K, V>> p) { } //for balanced tree subclass

        //Add two leafs to new external
        private void ExpandExternal(Node<Entry<K, V>> p, Entry<K, V> entry)
        {
            tree.Set(p, entry);   // store new entry at p
            tree.AddLeft(p, default);
            tree.AddRight(p, default);
        }

        private Node<Entry<K, V>> TreeSearch(Node<Entry<K, V>> p, K key)
        {
            if (tree.IsExternal(p))
                return p;                          // key not found; return the final leaf
            int comp = Compare(key, p.Element);
            if (comp == 0)
                return p;                          // key found; return its position
            else if (comp < 0)
                return TreeSearch(tree.Left(p), key);   // search left subtree
            else
                return TreeSearch(tree.Right(p), key);  // search right subtree
        }

        //  Returns position with the minimal key in the subtree rooted at Node p.
        public Node<Entry<K, V>>? TreeMin(Node<Entry<K, V>> p)
        {
            Node<Entry<K, V>> walk = p;
            while (tree.IsInternal(walk))
                walk = tree.Left(walk)!;
            return tree.Parent(walk);
        }

        public Node<Entry<K, V>>? TreeMax(Node<Entry<K, V>> p)
        {
            Node<Entry<K, V>> walk = p;
            while (tree.IsInternal(walk))
                walk = tree.Right(walk)!;
            return tree.Parent(walk);
        }

        public override V? Get(K key)
        {
            var p = TreeSearch(tree.Root, key);
            RebalanceAccess(p);                     // hook for balanced tree subclasses
            if (tree.IsExternal(p)) return default;         // unsuccessful search
            return p.Element!.Value;     // match found
        }

        public override V? Put(K key, V value)
        {

            var newEntry = new Entry<K, V>(key, value);
            var p = TreeSearch(tree.Root, key);
            if (tree.IsExternal(p))  // key is new
            {
                ExpandExternal(p, newEntry);
                RebalanceInsert(p);                   // hook for balanced tree subclasses
                return default;
            }
            else  // replacing existing key
            {
                V old = p.Element!.Value;
                tree.Set(p, newEntry);
                RebalanceAccess(p);                   // hook for balanced tree subclasses
                return old;
            }
        }

        public override V? Remove(K key)
        {

            var p = TreeSearch(tree.Root, key);
            if (tree.IsExternal(p))
            {                    // key not found
                RebalanceAccess(p);                   // hook for balanced tree subclasses
                return default;
            }
            else
            {
                V old = p.Element.Value;
                if (tree.IsInternal(tree.Left(p)) && tree.IsInternal(tree.Right(p)))
                { // both children are internal
                    Node<Entry<K, V>> replacement = TreeMax(tree.Left(p));
                    tree.Set(p, replacement.Element);
                    p = replacement;
                } // now p has at most one child that is an internal node
                Node<Entry<K, V>>? leaf = (tree.IsExternal(tree.Left(p)) ? tree.Left(p) : tree.Right(p));
                Node<Entry<K, V>>? sib = tree.Sibling(leaf);
                tree.Remove(leaf);
                tree.Remove(p);                            // sib is promoted in p's place
                RebalanceDelete(sib);                 // hook for balanced tree subclasses
                return old;
            }
        }

        public Entry<K, V>? FirstEntry()
        {
            if (tree.IsEmpty()) return default;
            return TreeMin(tree.Root)!.Element;
        }

        public Entry<K, V>? LastEntry()
        {
            if (tree.IsEmpty()) return null;
            return TreeMax(tree.Root)!.Element;
        }

        //Returns the entry with least key greater than or equal to given key
        public Entry<K, V>? CeilingEntry(K key)
        {
            Node<Entry<K, V>> p = TreeSearch(tree.Root, key);
            if (tree.IsInternal(p)) return p.Element;   // exact match
            while (!tree.IsRoot(p))
            {
                if (p == tree.Left(tree.Parent(p)))
                    return tree.Parent(p)!.Element;          // parent has next greater key
                else
                    p = tree.Parent(p)!;
            }
            return null;                                // no such ceiling exists
        }

        public Entry<K, V>? FloorEntry(K key)
        {
            Node<Entry<K, V>> p = TreeSearch(tree.Root, key);
            if (tree.IsInternal(p)) return p.Element;   // exact match
            while (!tree.IsRoot(p))
            {
                if (p == tree.Right(tree.Parent(p)))
                    return tree.Parent(p)!.Element;          // parent has next greater key
                else
                    p = tree.Parent(p)!;
            }
            return null;                                // no such ceiling exists
        }
        //Returns the entry with greatest key strictly less than given key
        public Entry<K, V>? LowerEntry(K key)
        {
            Node<Entry<K, V>> p = TreeSearch(tree.Root, key);
            if (tree.IsInternal(p) && tree.IsInternal(tree.Left(p)))
                return TreeMax(tree.Left(p))!.Element;   // exact match
            while (!tree.IsRoot(p))
            {
                if (p == tree.Right(tree.Parent(p)))
                    return tree.Parent(p)!.Element;          // parent has next greater key
                else
                    p = tree.Parent(p)!;
            }
            return null;                                // no such ceiling exists
        }

        public Entry<K, V>? HigherEntry(K key)
        {
            Node<Entry<K, V>> p = TreeSearch(tree.Root, key);
            if (tree.IsInternal(p) && tree.IsInternal(tree.Right(p)))
                return TreeMax(tree.Right(p))!.Element;   // exact match
            while (!tree.IsRoot(p))
            {
                if (p == tree.Left(tree.Parent(p)))
                    return tree.Parent(p)!.Element;          // parent has next greater key
                else
                    p = tree.Parent(p)!;
            }
            return null;                                // no such ceiling exists
        }


        public IEnumerable<Entry<K, V>> SubMap(K fromKey, K toKey)
        {

            var buffer = new ArrayList<Entry<K, V>>(Size);
            if (Compare(fromKey, toKey) < 0)                  // ensure that fromKey < toKey
                SubMapRecurse(fromKey, toKey, tree.Root, buffer);
            return buffer;
        }

        private void SubMapRecurse(K fromKey, K toKey, Node<Entry<K, V>> p,
                            ArrayList<Entry<K, V>> buffer)
        {
            if (tree.IsInternal(p))
                if (Compare(p.Element, fromKey) < 0)
                    // p's key is less than fromKey, so any relevant entries are to the right
                    SubMapRecurse(fromKey, toKey, tree.Right(p), buffer);
                else
                {
                    SubMapRecurse(fromKey, toKey, tree.Left(p), buffer); // first consider left subtree
                    if (Compare(p.Element, toKey) < 0)
                    {       // p is within range
                        buffer.Add(p.Element);                      // so add it to buffer, and consider
                        SubMapRecurse(fromKey, toKey, tree.Right(p), buffer); // right subtree as well
                    }
                }
        }

        public override IEnumerable<Entry<K, V>> EntrySet()
        {
            var buffer = new ArrayList<Entry<K, V>>(Size);
            foreach (Node<Entry<K, V>> p in tree.Inorder())
                if (tree.IsInternal(p)) buffer.Add(p.Element);
            return buffer;
        }

        public void Print()
        {
            PrintRecurse(tree.Root, 0);
        }

        private void PrintRecurse(Node<Entry<K, V>> p, int depth)
        {
            String indent = (depth == 0 ? "" : new string(' ', depth * 2));
            if (tree.IsExternal(p))
                Console.WriteLine(indent + "leaf");
            else
            {
                Console.WriteLine(indent + p.Element.Value);
                PrintRecurse(tree.Left(p), depth + 1);
                PrintRecurse(tree.Right(p), depth + 1);
            }
        }
    }
}

