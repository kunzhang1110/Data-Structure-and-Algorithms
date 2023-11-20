using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
    public class MergeSort<A> where A : IComparable
    {
        //range copy array inclusive start exclusive end
        public static A[] RangeCopy(A[] data, int start, int end)
        {
            var length = end - start;
            A[] result = new A[length];
            Array.Copy(data, start, result, 0, length);
            return result;
        }

        //Array Based
        public static void Merge(A[] S1, A[] S2, A[] S)
        {
            int i = 0, j = 0; //S1, S2 index
            while (i + j < S.Length)
            {
                if (j == S2.Length || (i < S1.Length && S1[i].CompareTo(S2[j]) < 0))
                    S[i + j] = S1[i++]; // copy ith element of S1 and increment i
                else
                    S[i + j] = S2[j++]; // copy jth element of S2 and increment j
            }
        }
        public static void Sort(A[] S)
        {
            var size = S.Length;
            if (size > 1)
            {
                int mid = size / 2;
                A[] S1 = RangeCopy(S, 0, mid);   // copy of first half
                A[] S2 = RangeCopy(S, mid, size);   // copy of second half
                Sort(S1);
                Sort(S2);
                Merge(S1, S2, S);    // merge sorted halves back into original
            }
        }

        //Queue Based
        public static void Merge(Queue<A> S1, Queue<A> S2, Queue<A> S)
        {
            while (S1.Count != 0 && S2.Count != 0)
            {
                if (S1.First().CompareTo(S2.First()) < 0)
                    S.Enqueue(S1.Dequeue()); // take next element from S1
                else
                    S.Enqueue(S2.Dequeue()); // take next element from S2
            }
            while (S1.Count != 0)
                S.Enqueue(S1.Dequeue());             // move any elements that remain in S1
            while (S2.Count != 0)
                S.Enqueue(S2.Dequeue());             // move any elements that remain in S2
        }

        public static void Sort(Queue<A> S)
        {
            var size = S.Count;
            if (size > 1)
            {
                var S1 = new Queue<A>();
                var S2 = new Queue<A>();
                while (S1.Count < size / 2) // move the first n/2 elements to S1
                    S1.Enqueue(S.Dequeue());
                while (S.Count != 0)
                    S2.Enqueue(S.Dequeue());
                Sort(S1);
                Sort(S2);
                Merge(S1, S2, S);    // merge sorted halves back into original
            }
        }
    }
}
