using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Graphs
{
    public class Partition<E>
    {
        public class Locator<A> : Position<E>
        {
            public E? Element { get; set; }
            public int Size;
            public Locator<A> Parent;
            public Locator(E elem)
            {
                Element = elem;
                Size = 1;
                Parent = this;          // convention for a cluster leader
            }
        }

        private Locator<E> Validate(Position<E> pos)
        {
            if (pos is not Locator<E>) throw new Exception("Invalid position");
            return (Locator<E>)pos;
        }

        // Makes a new cluster containing element e and returns its position. */
        public Position<E> MakeCluster(E e)
        {
            return new Locator<E>(e);
        }


        // Finds the cluster containing the element identified by Position p and returns the Position of the cluster's leader.
        public Position<E> Find(Position<E> p)
        {
            Locator<E> loc = Validate(p);
            if (loc.Parent != loc)
                loc.Parent = (Locator<E>)Find(loc.Parent);   // overwrite Parent after recursion
            return loc.Parent;
        }

        // Merges the clusters containing elements with positions p and q (if distinct).
        public void Union(Position<E> p, Position<E> q)
        {
            Locator<E> a = (Locator<E>)Find(p);
            Locator<E> b = (Locator<E>)Find(q);
            if (a != b)
                if (a.Size > b.Size)
                {
                    b.Parent = a;
                    a.Size += b.Size;
                }
                else
                {
                    a.Parent = b;
                    b.Size += a.Size;
                }
        }
    }
}
