using System;
using Data_Structure.Lists;

namespace Data_Structure.Priority_Queues
{
    public class UnsortedPriorityQueue<K, V> : AbstractPriorityQueue<K, V>
    {
        private PositionalList<Entry<K, V>> list = new();

        public UnsortedPriorityQueue() : base() { }
        public UnsortedPriorityQueue(Comparer<K> comp) : base(comp) { }

        private Node<Entry<K, V>>? FindMin()
        {
            var small = list.First()!;
            if (small == null) return null;
            foreach (Node<Entry<K, V>> walk in list)
            {
                if (Compare(walk.Element, small.Element) < 0) small = walk;
            }
            return small;
        }

        public override Entry<K, V> Insert(K key, V value)
        {
            Entry<K, V> newest = new(key, value);
            list.AddLast(newest);
            return newest;
        }

        public override Entry<K, V>? Min()
        {
            if (list.IsEmpty()) return null;
            return FindMin()!.Element;
        }

        public override Entry<K, V>? RemoveMin()
        {
            if (list.IsEmpty()) return null;
            return list.Remove(FindMin());
        }

        public override void PrintPQ()
        {
            foreach (Node<Entry<K, V>> walk in list)
            {
                Console.WriteLine($"{walk.Element.Key.ToString()} {walk.Element.Value.ToString()}");
            }
        }
    }
}




