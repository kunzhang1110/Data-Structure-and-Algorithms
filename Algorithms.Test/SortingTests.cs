using Algorithms.Sorting;
using System.Drawing;

namespace Algorithms.Test
{

    [TestClass]
    public class SortingTests
    {
        private readonly char[] dataArray = new char[] { 'C', 'E', 'B', 'D', 'A', 'I', 'J', 'L', 'K', 'H', 'G', 'F' };
        private readonly Queue<char> dataQueue = new();

        public SortingTests()
        {
            foreach (var c in dataArray)
                dataQueue.Enqueue(c);
        }

        [TestMethod]
        public void BubbleSort_Test()
        {
            BubbleSort<char>.Sort(dataArray);
            foreach (var c in dataArray) Console.WriteLine($"{c} ");
        }
        [TestMethod]
        public void SelectionSort_Test()
        {
            SelectionSort<char>.Sort(dataArray);
            foreach (var c in dataArray) Console.WriteLine($"{c} ");
        }
        [TestMethod]
        public void InsertionSort_Test()
        {
            InsertionSort<char>.Sort(dataArray);
            foreach (var c in dataArray) Console.WriteLine($"{c} ");
        }

        [TestMethod]
        public void MergeSort_Array_Test()
        {
            MergeSort<char>.Sort(dataArray);
            foreach (var c in dataArray) Console.WriteLine($"{c} ");
        }
        [TestMethod]
        public void MergeSort_Queue_Test()
        {
            MergeSort<char>.Sort(dataQueue);
            foreach (var c in dataQueue) Console.WriteLine($"{c} ");
        }

        [TestMethod]
        public void QuickSort_Array_Test()
        {
            QuickSort<char>.Sort(dataArray, 0, dataArray.Length - 1);
            foreach (var c in dataArray) Console.WriteLine($"{c} ");
        }

        [TestMethod]
        public void QuickSort_Queue_Test()
        {
            QuickSort<char>.Sort(dataQueue);
            foreach (var c in dataQueue) Console.WriteLine($"{c} ");
        }

        [TestMethod]
        public void BucketSort_Decimal_Test()
        {
            var data = new float[] {(float) 0.42, (float) 0.32, (float) 0.33, (float) 0.52, (float) 0.37, (float) 0.47,
        (float) 0.51};
            BucketSort.Sort(data, null);
            foreach (var i in data) Console.WriteLine($"{i} ");
        }

        [TestMethod]
        public void BucketSort_Integer_Test()
        {
            var data = new float[] { (float)23, (float)11, (float)16, (float)24, (float)50 };
            int GetIndexPositiveFloat(float x)
            {
                var min = data.Min();
                var range = data.Max() + 1 - min;
                return (int)((int)(x - min) / range * data.Length);
            }
            BucketSort.Sort(data, GetIndexPositiveFloat);
            foreach (var i in data) Console.WriteLine($"{i} ");
        }

        [TestMethod]
        public void RadixSort_Integer_Test()
        {
            var data = new int[] { 121, 432, 564, 23, 1, 45, 788 };

            RadixSort.Sort(data, 3);
            foreach (var i in data) Console.WriteLine($"{i} ");
        }

        [TestMethod]
        public void HeapSort_Test()
        {
            HeapSort<char>.Sort(dataArray);
            foreach (var i in dataArray) Console.WriteLine($"{i} ");
        }


    }
}