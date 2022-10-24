using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Maps
{
public abstract class AbstractMap<K, V>
{
    public abstract IEnumerable<Entry<K, V>> EntrySet();
    public abstract V? Get(K key);
    public abstract V? Put(K key, V value);
    public abstract V? Remove(K key);

    public IEnumerable<K> KeySet()
    {
        return new KeyEnumerable<K>(this);
    }

    public IEnumerable<V> Values()
    {
        return new ValueEnumerable<V>(this);
    }

    //KeyEnumerable
    private class KeyEnumerator<T> : IEnumerator<K>
    {
        private IEnumerator<Entry<K, V>> entries;
        public AbstractMap<K, V> map;

        public KeyEnumerator(AbstractMap<K, V> map)
        {
            this.map = map;
            this.entries = map.EntrySet().GetEnumerator();
        }
        public K Current => entries.Current.Key;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() { }

        public bool MoveNext() => entries.MoveNext();

        public void Reset() => entries.Reset();

    }

    public class KeyEnumerable<T> : IEnumerable<K>
    {
        public AbstractMap<K, V> map;
        public KeyEnumerable(AbstractMap<K, V> map) { this.map = map; }

        public IEnumerator<K> GetEnumerator() => new KeyEnumerator<K>(map);

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }


    // ValueEnumerable
    private class ValueEnumerator<T> : IEnumerator<V>
    {
        private IEnumerator<Entry<K, V>> entries;
        public AbstractMap<K, V> map;

        public ValueEnumerator(AbstractMap<K, V> map)
        {
            this.map = map;
            this.entries = map.EntrySet().GetEnumerator();
        }

        public V Current => entries.Current.Value;

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose() { }

        public bool MoveNext() => entries.MoveNext();

        public void Reset() => entries.Reset();

    }

    public class ValueEnumerable<T> : IEnumerable<V>
    {
        public AbstractMap<K, V> map;
        public ValueEnumerable(AbstractMap<K, V> map) { this.map = map; }

        public IEnumerator<V> GetEnumerator() => new ValueEnumerator<V>(map);

        IEnumerator IEnumerable.GetEnumerator() => throw new NotImplementedException();
    }
}
}
