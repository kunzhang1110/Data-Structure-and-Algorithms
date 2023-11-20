using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.Sorting
{
    public class BucketSort
    {   //GetIndex maps data[i] into bucket's index
        public static void Sort(float[] data, Func<float, int>? GetIndex)
        {
            GetIndex ??= (float x) => //default Get Index
            {
                var min = data.Min();
                var range = data.Max() + 1 - min;
                return (int)((int)(x - min) / range * data.Length);
            };

            var size = data.Length;
            var bucket = new List<float>[size];

            // Create empty buckets
            for (int i = 0; i < size; i++)
                bucket[i] = new List<float>();

            // Add elements into the buckets
            for (int i = 0; i < size; i++)
            {

                int bucketIndex = GetIndex(data[i]);
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