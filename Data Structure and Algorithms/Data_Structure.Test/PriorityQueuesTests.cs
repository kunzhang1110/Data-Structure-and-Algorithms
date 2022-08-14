using Data_Structure.Priority_Queues;

namespace Data_Structure.Test
{
    [TestClass]
    public class PriorityQueuesTests
    {
        private readonly UnsortedPriorityQueue<int, int?> unsortedPq;
        private readonly HeapPriorityQueue<int, int?> heapPq;
        public PriorityQueuesTests()
        {
            var data = new int[] { 1, 3, 2, 5, 4 };
            unsortedPq = new UnsortedPriorityQueue<int, int?>();
            foreach (var i in data)
                unsortedPq.Insert(i, i);
            heapPq = new HeapPriorityQueue<int, int?>();
            foreach (var i in data)
                heapPq.Insert(i, i);
        }

        [TestMethod]
        public void UnsortedPriorityQueue_RemoveMin_Print3254()
        {
            unsortedPq.RemoveMin();
            unsortedPq.PrintPQ();
        }

        [TestMethod]
        public void HeapPriorityQueue_RemoveMin_Print3254()
        {
            heapPq.RemoveMin();
            heapPq.PrintPQ();
        }
        [TestMethod]
        public void HeapPriorityQueue_Constructor_Print13254()
        {
            var pq = new HeapPriorityQueue<int, int?>(new int[] { 1, 3, 2, 5, 4 }, new int?[] { 1, 3, 2, 5, 4 });
            pq.PrintPQ();
        }
    }
}