using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Lists;

namespace Data_Structure.Maps
{
    public class ProbeHashMap<K, V> : AbstractHashMap<K, V>
    {
        public Entry<K, V>[] table;
        public Entry<K, V> AVAILABLE = new(default, default); //sentinel

        public ProbeHashMap(int cap) : base(cap) { table = new Entry<K, V>[capacity]; }
        public ProbeHashMap() : this(50) { }

        private bool IsAvailable(int i)
        {
            return (table[i] == null | table[i] == AVAILABLE);
        }

        private int FindSlot(int h, K k)
        {
            int avail = -1;
            int i = h;
            do
            {
                if (IsAvailable(i)) //empty or available
                {
                    if (avail == -1) avail = i; // first available slot
                    if (table[i] == null) break; // empty
                }
                else if (table[i].Key!.Equals(k))
                {
                    return i;
                }
                i = (i + 1) % capacity; //search cyclically
            } while (i != h);
            return -(avail + 1); //search failed
        }

        public override V? BucketGet(int h, K k)
        {
            int i = FindSlot(h, k);
            if (i < 0) return default;
            return table[i].Value;
        }

        public override V? BucketPut(int h, K k, V v)
        {
            int i = FindSlot(h, k);
            if (i >= 0)
            {
                table[i].Value = v;
                return v;
            }
            table[-(i + 1)] = new Entry<K, V>(k, v);
            Size++;
            return default;
        }

        public override V? BucketRemove(int h, K k)
        {
            int i = FindSlot(h, k);
            if (i < 0) return default;
            V answer = table[i].Value;
            table[i] = AVAILABLE;
            Size--;
            return answer;
        }

        public override IEnumerable<Entry<K, V>> EntrySet()
        {
            var buffer = new ArrayList<Entry<K, V>>(Size);
            for (int h = 0; h < capacity; h++)
            {
                if (!IsAvailable(h)) buffer.Add(table[h]);
            }
            return buffer!;
        }
    }
}

