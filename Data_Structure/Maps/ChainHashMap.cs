using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Lists;

namespace Data_Structure.Maps
{
    public class ChainHashMap<K, V> : AbstractHashMap<K, V>
    {
        public UnsortedListMap<K, V>[] table; //cretae an array of chains

        public ChainHashMap(int cap) : base(cap) { table = new UnsortedListMap<K, V>[capacity]; }
        public ChainHashMap() : this(50) { }

        public override V? BucketGet(int h, K k)
        {
            var bucket = table[h];
            if (bucket == null) return default;
            return bucket.Get(k);
        }

        public override V? BucketPut(int h, K k, V v)
        {
            var bucket = table[h];
            bucket ??= table[h] = new UnsortedListMap<K, V>();
            int oldSize = bucket.Size;
            V? answer = bucket.Put(k, v);
            Size += (bucket.Size - oldSize);
            return answer;
        }

        public override V? BucketRemove(int h, K k)
        {
            var bucket = table[h];
            if (bucket == null) return default;
            var oldSize = bucket.Size;
            V? answer = bucket.Remove(k);
            Size -= (oldSize - bucket.Size);
            return answer;
        }

        public override IEnumerable<Entry<K, V>> EntrySet()
        {
            var buffer = new ArrayList<Entry<K, V>>(Size);
            for (int h = 0; h < capacity; h++)
            {
                if (table[h] != null)
                {
                    foreach (Entry<K, V> entry in table[h].EntrySet())
                        buffer.Add(entry);
                }
            }
            return buffer;
        }
    }
}




