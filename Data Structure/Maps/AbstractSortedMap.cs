using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Maps
{
    public abstract class AbstractSortedMap<K, V> : AbstractMap<K, V>
    {
        private readonly Comparer<K> Comp;

        public AbstractSortedMap(Comparer<K> c) { Comp = c; }

        public AbstractSortedMap() : this(Comparer<K>.Default) { }

        //Compare Methods
        public int Compare(Entry<K, V> a, Entry<K, V> b) { return Comp.Compare(a.Key, b.Key); }

        public int Compare(K a, Entry<K, V> b) { return Comp.Compare(a, b.Key); }

        public int Compare(Entry<K, V> a, K b) { return Comp.Compare(a.Key, b); }

        public int Compare(K a, K b) { return Comp.Compare(a, b); }

    }
}




