using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Priority_Queues
{
    public abstract class AbstractPriorityQueue<K, V>
    {
        public class Entry<A, B>
        {
            public K Key { get; set; }
            public V Value { get; set; }

            public Entry(K key, V value)
            {
                Key = key;
                Value = value;
            }
        }
        public virtual int Size { get; set; }
        public Comparer<K> Comp { get; set; }

        public abstract Entry<K, V> Insert(K key, V value);
        public abstract Entry<K, V>? Min();
        public abstract Entry<K, V>? RemoveMin();
        public abstract void PrintPQ();

        public AbstractPriorityQueue()
        {
            Comp = Comparer<K>.Default;
        }
        public AbstractPriorityQueue(Comparer<K> comp)
        {
            Comp = comp;
        }

        public int Compare(Entry<K, V> a, Entry<K, V> b)
        {
            return Comp.Compare(a.Key, b.Key);
        }
    }
}




