using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Priority_Queues
{
    public abstract partial class AbstractPriorityQueue<K, V>
    {
        public virtual int Size { get; set; }
        public Comparer<K> Comp { get; set; }

        public abstract PQEntry<K, V> Insert(K key, V value);
        public abstract PQEntry<K, V>? Min();
        public abstract PQEntry<K, V>? RemoveMin();
        public abstract void PrintPQ();

        public AbstractPriorityQueue()
        {
            Comp = Comparer<K>.Default;
        }
        public AbstractPriorityQueue(Comparer<K> comp)
        {
            Comp = comp;
        }

        public int Compare(PQEntry<K, V> a, PQEntry<K, V> b)
        {
            return Comp.Compare(a.Key, b.Key);
        }
    }
}




