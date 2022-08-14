using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Lists;

namespace Data_Structure.Maps
{
    public class UnsortedListMap<K, V> : AbstractMap<K, V>
    {
        public ArrayList<Entry<K, V>> table = new();

        public int Size { get { return table.Size; } }

        private int FindIndex(K key)
        {
            for (int i = 0; i < table.Size; i++)
            {
                if (table.Get(i)!.Key!.Equals(key))
                    return i;
            }
            return -1;
        }

        public override V? Get(K key)
        {
            int i = FindIndex(key);
            if (i == -1) return default;
            return table.Get(i)!.Value;
        }

        public override V? Put(K key, V value)
        {
            int i = FindIndex(key);
            if (i == -1)
            {
                table.Add(new Entry<K, V>(key, value));
                return default;
            };
            return table.Get(i)!.Value;
        }

        public override V? Remove(K key)
        {
            int i = FindIndex(key);
            if (i == -1) return default;
            V answer = table.Get(i)!.Value;
            if (i != Size - 1)
            {
                table.Set(i, table.Get(Size - 1));
            }
            table.Remove(Size - 1);
            return answer;
        }

        public override IEnumerable<Entry<K, V>> EntrySet()
        {
            return new EntryEnumerable<Entry<K, V>>(this);
        }


        // EntryEnumerable
        private class EntryEnumerator<T> : IEnumerator<Entry<K, V>>
        {
            private int i = 0;

            public UnsortedListMap<K, V> map;
            public ArrayList<Entry<K, V>> table;

            public EntryEnumerator(UnsortedListMap<K, V> map)
            {
                this.map = map;
                this.table = map.table;
            }
            public Entry<K, V> Current => table.Get(i++)!;

            object? IEnumerator.Current => Current;

            public void Dispose() { }

            public bool MoveNext() => i < table.Size;

            public void Reset() => i = 0;

        }

        public class EntryEnumerable<T> : IEnumerable<Entry<K, V>>
        {
            public UnsortedListMap<K, V> map;
            public EntryEnumerable(UnsortedListMap<K, V> map) { this.map = map; }

            public IEnumerator GetEnumerator() => throw new NotImplementedException();

            IEnumerator<Entry<K, V>> IEnumerable<Entry<K, V>>.GetEnumerator() => new EntryEnumerator<Entry<K, V>>(map);

        }
    }
}




