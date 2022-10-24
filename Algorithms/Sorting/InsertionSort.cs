using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
    public class InsertionSort<A> where A : IComparable
    {
        public static void Sort(A[] data)
        {
            var size = data.Length;
            for (int step = 1; step < size; step++) // begin with second character
            {
                A current = data[step];  //insert current=data[k]
                int i = step;
                while (i > 0 && data[i - 1].CompareTo(current) > 0)
                {
                    data[i] = data[i - 1];  // slide data[j-1] rightward
                    i--;
                }
                data[i] = current;
            }
        }
    }
}
