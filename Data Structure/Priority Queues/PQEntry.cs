namespace Data_Structure.Priority_Queues
{
    public class PQEntry<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public PQEntry(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }

}




