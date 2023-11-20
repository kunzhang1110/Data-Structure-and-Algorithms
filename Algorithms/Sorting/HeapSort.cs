using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
    public class HeapSort<A> where A : IComparable
    {
public static void Sort(A[] data)
{
    int n = data.Length;

    // Build max heap
    for (int i = n / 2 - 1; i >= 0; i--)
    {
        Heapify(data, n, i);
    }

    // Heap sort
    for (int i = n - 1; i >= 0; i--)
    {
        (data[0], data[i]) = (data[i], data[0]);
        Heapify(data, i, 0);
    }
}

public static void Heapify(A[] data, int n, int i)
{
    // Find largest among root, Left child and Right child
    int largest = i;
    int left = 2 * i + 1;
    int right = 2 * i + 2;

    if (left < n && data[left].CompareTo(data[largest]) > 0)
        largest = left;

    if (right < n && data[right].CompareTo(data[largest]) > 0)
        largest = right;

    // Swap and continue heapifying if root is not largest
    if (largest != i)
    {
        (data[largest], data[i]) = (data[i], data[largest]);
        Heapify(data, n, largest);
    }
}
    }
}