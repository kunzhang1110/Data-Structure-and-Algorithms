using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
    public class RadixSort
    {
        private static int GetDigit(int x, int place)
        {
            for (var i = 1; i < place; i++) x /= 10;
            return x % 10;
        }

        public static void Sort(int[] data, int max_place)
        {
            // Apply counting sort to sort elements based on place value.
            for (int place = 1; place <= max_place; place++)
                BucketSort(data, null, place);
        }

        public static void BucketSort(int[] data, Func<int, int>? GetIndex, int place)
        {
            var digit_range = 10; //0~9 Intger Digits
            var size = data.Length;
            var bucket = new List<int>[digit_range];

            GetIndex ??= (int x) => x;//default Get Index

            // Create empty buckets
            for (int i = 0; i < digit_range; i++)
                bucket[i] = new List<int>();

            // Add elements into the buckets
            for (int i = 0; i < size; i++)
            {
                Console.WriteLine(i + " " + place + " " + data[i]);
                int bucketIndex = GetIndex(GetDigit(data[i], place));
                bucket[bucketIndex].Add(data[i]);
            }

            // Sort the elements of each bucket
            for (int i = 0; i < size; i++)
            {
                bucket[i].Sort();
            }

            // Get the sorted array
            int index = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0, count = bucket[i].Count; j < count; j++)
                {
                    data[index++] = bucket[i][j];
                }
            }
        }
    }
}