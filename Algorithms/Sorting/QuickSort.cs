using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
    public class QuickSort<A> where A : IComparable
    {
        //Array Based in place

        public static void Sort(A[] S, int a, int b)
        {

            if (a <= b)
            {
                int left = a;
                int right = b - 1;
                A pivot = S[b];

                while (left <= right)
                {
                    // scan unti value equal or larger than pivot or Right
                    while (left <= right && S[left].CompareTo(pivot) < 0)
                        left++;
                    // scan until value equal or smaller than pivot or Left marker
                    while (left <= right && S[right].CompareTo(pivot) > 0)
                        right--;
                    if (left <= right)
                    {  // swap values and shrink range
                        (S[left], S[right]) = (S[right], S[left]);
                        left++;
                        right--;
                    }
                }
                // put pivot into its final place (currently marked by Left)
                (S[left], S[b]) = (S[b], S[left]);
                Sort(S, a, left - 1);
                Sort(S, left + 1, b);
            }
        }

        //Queue Based
        public static void Sort(Queue<A> S)
        {
            var size = S.Count;
            if (size > 1)
            {// divide
                var pivot = S.First(); //using first as arbitrary pivot
                var L = new Queue<A>();
                var E = new Queue<A>();
                var G = new Queue<A>();
                while (S.Count != 0)
                { // divide original into L, E, and G
                    A element = S.Dequeue();
                    int c = element.CompareTo(pivot);
                    if (c < 0) // element is less than pivot
                        L.Enqueue(element);
                    else if (c == 0)
                        E.Enqueue(element);
                    else // element is greater than pivot
                        G.Enqueue(element);
                }
                //conquer
                Sort(L); // sort elements less than pivot
                Sort(G); // sort elements greater than pivot

                // Combine results
                while (L.Count != 0)
                    S.Enqueue(L.Dequeue());
                while (E.Count != 0)
                    S.Enqueue(E.Dequeue());
                while (G.Count != 0)
                    S.Enqueue(G.Dequeue());
            }
        }
    }
}
