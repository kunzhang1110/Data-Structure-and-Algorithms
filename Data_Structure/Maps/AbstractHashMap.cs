using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Lists;
using static System.Formats.Asn1.AsnWriter;

namespace Data_Structure.Maps
{
    public abstract class AbstractHashMap<K, V> : AbstractMap<K, V>
    {
        protected int capacity; //length of the table
        public int Size { get; set; }  //number of entries

        public AbstractHashMap(int capacity)
        {
            this.capacity = capacity;
        }
        public AbstractHashMap() : this(100) { }

        public abstract V? BucketGet(int h, K k);
        public abstract V? BucketPut(int h, K k, V v);
        public abstract V? BucketRemove(int h, K k);

        public int HashValue(K key)
        {
            return key!.GetHashCode() % capacity; //simple hash

            //// Multiply Add and Divide Method
            //var prime = 2147483647; //any prime bigger than capacity
            //var rand = new Random();
            //var scale = rand.NextInt64(prime - 1) + 1;
            //var shift = rand.NextInt64(prime);
            //return (int)((Math.Abs(key!.GetHashCode() * scale + shift) % prime) % capacity);
        }

        public void Resize(int newCap)
        {
            ArrayList<Entry<K, V>> buffer = new(Size);
            foreach (Entry<K, V> e in EntrySet())
                buffer.Add(e);
            capacity = newCap;
            Size = 0;
            foreach (Entry<K, V> e in buffer.data)
                Put(e.Key, e.Value);
        }

        public override V? Get(K key) { return BucketGet(HashValue(key), key); }

        public override V? Remove(K key) { return BucketRemove(HashValue(key), key); }

        public override V? Put(K key, V value)
        {
            V? answer = BucketPut(HashValue(key), key, value);
            if (Size > capacity / 2)
                Resize(2 * capacity - 1);
            return answer;
        }

    }
}




