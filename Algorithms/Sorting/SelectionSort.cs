using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
    public class SelectionSort<A> where A : IComparable
    {
        public static void Sort(A[] data)
        {
            int size = data.Length;
            for (int step = 0; step < size - 1; step++)
            {
                int min_idx = step;

                for (int i = step + 1; i < size; i++) //get the minimum
                {
                    if (data[i].CompareTo(data[min_idx]) < 0)
                    {
                        min_idx = i;
                    }
                }
                // swap min at the correct position
                (data[min_idx], data[step]) = (data[step], data[min_idx]);
            }
        }
    }
}
