using System;
using System.Collections.Generic;
using Data_Structure.Lists;

namespace Data_Structure.Priority_Queues
{
public class HeapAdaptablePriorityQueue<K, V> : HeapPriorityQueue<K, V>
{
    //---------------- nested AdaptablePQEntry class ----------------
    public class AdaptablePQEntry<A, B> : PQEntry<K, V>
    {
        public int Index { get; set; }          // entry's current index within the heap
        public AdaptablePQEntry(K key, V value, int j) : base(key, value)
        {

            Index = j;                // this sets the new field
        }
    }
    public HeapAdaptablePriorityQueue() : base() { }
    public HeapAdaptablePriorityQueue(Comparer<K> comp) : base(comp) { }

    public AdaptablePQEntry<K, V> Validate(PQEntry<K, V> entry)

    {
        if (!(entry is AdaptablePQEntry<K, V>)) throw new Exception("Invalid entry");
        var locator = entry as AdaptablePQEntry<K, V>;   // safe
        var j = locator.Index;
        if (j >= heap.Size || heap.Get(j) != locator)
            throw new Exception("Invalid entry");
        return locator;
    }

    public override void Swap(int i, int j)
    {
        PQEntry<K, V> temp = heap.Get(i)!;
        heap.Set(i, heap.Get(j));
        heap.Set(j, temp);
        ((AdaptablePQEntry<K, V>)heap.Get(i)!).Index = i; // reset entry's index
        ((AdaptablePQEntry<K, V>)heap.Get(j)!).Index = j;
    }


    public void Bubble(int j)
    {
        if (j > 0 && Comp.Compare(heap.Get(j)!.Key, heap.Get(Parent(j))!.Key) < 0)
            Upheap(j);
        else
            Downheap(j);
    }

    // public methods
    public override PQEntry<K, V> Insert(K key, V value)
    {
        var newest = new AdaptablePQEntry<K, V>(key, value, heap.Size);
        heap.Add(newest);                // add to the end of the list
        Upheap(heap.Size - 1);         // upheap newly added entry
        return newest;
    }

    public void Remove(PQEntry<K, V> entry)
    {
        AdaptablePQEntry<K, V> locator = Validate(entry);
        int j = locator.Index;
        if (j == heap.Size - 1)        // entry is at last position
            heap.Remove(heap.Size - 1);  // so just remove it
        else
        {
            Swap(j, heap.Size - 1);      // swap entry to last position
            heap.Remove(heap.Size - 1);  // then remove it
            Bubble(j);                     // and fix entry displaced by the swap
        }
    }

    public void ReplaceKey(PQEntry<K, V> entry, K key)

    {
        var locator = Validate(entry);

        locator.Key = key;             // method inherited from PQEntry
        Bubble(locator.Index);      // with new key, may need to move entry
    }

    public void ReplaceValue(PQEntry<K, V> entry, V value)

    {
        var locator = Validate(entry);
        locator.Value = value;         // method inherited from PQEntry
    }
}
}




