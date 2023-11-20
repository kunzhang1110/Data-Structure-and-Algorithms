using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Lists;

namespace Data_Structure.Maps
{
    public class SortedTableMap<K, V> : AbstractSortedMap<K, V>
    {
        public ArrayList<Entry<K, V>> table = new();
        public int Size { get { return table.Size; } }
        public SortedTableMap() : base() { }
        public SortedTableMap(Comparer<K> c) : base(c) { }

        private int FindIndex(K key, int low, int high)
        {// Find the index of key using Binary Search from low to high
            if (high < low) return high + 1;         // no entry qualifies
            int mid = (low + high) / 2;
            int comp = Compare(key, table.Get(mid));
            if (comp == 0)
                return mid;                            // found exact match
            else if (comp < 0)
                return FindIndex(key, low, mid - 1);   // answer is left of mid (or possibly mid)
            else
                return FindIndex(key, mid + 1, high);  // answer is right of mid
        }

        private int FindIndex(K key)
        {//find the index of key from the whole table
            return FindIndex(key, 0, table.Size - 1);
        }

        public override V? Get(K key)
        {
            int i = FindIndex(key);
            if (i == Size || Compare(key, table.Get(i)) != 0)
                return default;   // no match
            return table.Get(i)!.Value;
        }

        public override V? Put(K key, V value)
        {
            int i = FindIndex(key);
            if (i < Size && Compare(key, table.Get(i)) == 0) // match exists
                return table.Get(i)!.Value = value;
            table.Add(i, new Entry<K, V>(key, value)); // otherwise new
            return default;
        }

        public override V? Remove(K key)
        {
            int i = FindIndex(key);
            if (i == Size || Compare(key, table.Get(i)) != 0)
                return default; // no match
            return table.Remove(i)!.Value;
        }

        public override IEnumerable<Entry<K, V>> EntrySet()
        {//return a copy of table
            var copy = new ArrayList<Entry<K, V>>();
            for (var i = 0; i < table.Size; i++)
                copy.Add(table.Get(i));
            return copy;
        }
        public IEnumerable<Entry<K, V>> SubMap(K fromKey, K toKey)
        {
            var subMap = new ArrayList<Entry<K, V>>();
            int i = FindIndex(fromKey);
            while (i < table.Size && Compare(toKey, table.Get(i)) > 0)
            {
                subMap.Add(table.Get(i++));
            }
            return subMap;
        }

        private Entry<K, V>? GetEntry(int i)
        {
            if (i < 0 || i >= table.Size) return default;
            return table.Get(i);
        }

        public Entry<K, V>? FirstEntry() { return GetEntry(0); }
        public Entry<K, V>? LastEntry() { return GetEntry(table.Size - 1); }

        public Entry<K, V>? CeilingEntry(K key)
        {
            return GetEntry(FindIndex(key));
        }

        public Entry<K, V>? FloorEntry(K key)
        {
            if (key == null) return null;
            int i = FindIndex(key);
            if (i == Size || !key.Equals(table.Get(i)!.Key))
                i--;
            return GetEntry(i);
        }

        public Entry<K, V>? LowerEntry(K key)
        {//Returns the entry with greatest key strictly less than given key
            return GetEntry(FindIndex(key) - 1);   // go strictly before the ceiling entry
        }

        public Entry<K, V>? HigherEntry(K key)
        {
            if (key == null) return null;
            int i = FindIndex(key);
            if (i < Size && key.Equals(table.Get(i)!.Key))
                i++;    // go past exact match
            return GetEntry(i);
        }
    }
}




