using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Lists;

namespace Data_Structure.Priority_Queues
{
public class HeapPriorityQueue<K, V> : AbstractPriorityQueue<K, V>
{
    public ArrayList<Entry<K, V>> heap = new();
    public override int Size { get { return heap.Size; } }

    public HeapPriorityQueue() : base() { }
    public HeapPriorityQueue(Comparer<K> comp) : base(comp) { }

    public HeapPriorityQueue(K[] keys, V[] values) : base()
    {

        for (int i = 0; i < Math.Min(keys.Length, values.Length); i++)
            heap.Add(new Entry<K, V>(keys[i], values[i]));
        Heapify();
    }

    private int Parent(int j) { return (j - 1) / 2; }     // truncating division
    private int Left(int j) { return 2 * j + 1; }
    private int Right(int j) { return 2 * j + 2; }
    private bool HasLeft(int j) { return Left(j) < heap.Size; }
    private bool HasRight(int j) { return Right(j) < heap.Size; }
    private void Swap(int i, int j)
    {
        Entry<K, V> temp = heap.Get(i)!;
        heap.Set(i, heap.Get(j));
        heap.Set(j, temp);
    }

    protected void Upheap(int i)
    {
        while (i > 0)
        {            // continue until reaching root (or break statement)
            int p = Parent(i);
            if (Compare(heap.Get(i), heap.Get(p)) >= 0) break; // heap property verified
            Swap(i, p);
            i = p;                                // continue from the parent's location
        }
    }

    /** Moves the entry at index j lower, if necessary, to restore the heap property. */
    protected void Downheap(int i)
    {
        while (HasLeft(i))
        {               // continue to bottom (or break statement)
            int leftIndex = Left(i);
            int smallChildIndex = leftIndex;     // although right may be smaller
            if (HasRight(i))
            {
                int rightIndex = Right(i);
                if (Compare(heap.Get(leftIndex), heap.Get(rightIndex)) > 0)
                    smallChildIndex = rightIndex;  // right child is smaller
            }
            if (Compare(heap.Get(smallChildIndex), heap.Get(i)) >= 0)
                break;                             // heap property has been restored
            Swap(i, smallChildIndex);
            i = smallChildIndex;                 // continue at position of the child
        }
    }

    protected void Heapify()
    {
        int startIndex = Parent(Size - 1);    // start at PARENT of last entry
        for (int j = startIndex; j >= 0; j--)   // loop until processing the root
            Downheap(j);
    }

    public override Entry<K, V> Insert(K key, V value)
    {
        var newest = new Entry<K, V>(key, value);
        heap.Add(newest);                      // add to the end of the list
        Upheap(heap.Size - 1);               // upheap newly added entry
        return newest;
    }

    public override Entry<K, V>? Min()
    {
        if (heap.Size == 0) return null;
        return heap.Get(0);
    }

    public override Entry<K, V>? RemoveMin()
    {
        if (heap.Size == 0) return null;
        Entry<K, V> answer = heap.Get(0)!;
        Swap(0, heap.Size - 1);              // put minimum item at the end
        heap.Remove(heap.Size - 1);          // and remove it from the list;
        Downheap(0);                           // then fix new root
        return answer;
    }

    public override void PrintPQ()
    {
        foreach (Entry<K, V> e in heap)
        {
            Console.Write($"{e.Key} ");
        }
    }
}
}




