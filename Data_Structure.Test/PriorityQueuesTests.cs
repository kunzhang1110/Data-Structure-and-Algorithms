using Data_Structure.Priority_Queues;


namespace Data_Structure.Test
{
    [TestClass]
    public class PriorityQueuesTests
    {
        private readonly UnsortedPriorityQueue<int, int?> unsortedPq;
        private readonly HeapPriorityQueue<int, int?> heapPq;
        private readonly HeapAdaptablePriorityQueue<int, int?> heapAdptPq;
        public PriorityQueuesTests()
        {
            var data = new int[] { 1, 3, 2, 5, 4 };
            unsortedPq = new UnsortedPriorityQueue<int, int?>();
            foreach (var i in data)
                unsortedPq.Insert(i, i);
            heapPq = new HeapPriorityQueue<int, int?>();
            foreach (var i in data)
                heapPq.Insert(i, i);
            heapAdptPq = new HeapAdaptablePriorityQueue<int, int?>();
            foreach (var i in data)
                heapAdptPq.Insert(i, i);
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

        private static PQEntry<int, int?>? GetEntry(int key, HeapPriorityQueue<int, int?> pq)
        {
            foreach(PQEntry<int, int?>? entry in pq.heap)
            {
                if (entry.Key == key)
                    return entry!;
            }
            return null;
        }

        [TestMethod]
        public void AdaptablePriorityQueue_Remove3_Print1425()
        {

            var entry = GetEntry(3, heapAdptPq);
            heapAdptPq.Remove(entry);
            heapAdptPq.PrintPQ();
        }

        [TestMethod]
        public void AdaptablePriorityQueue_ReplaceKey3By10_PrintKey142510()
        {

            var entry = GetEntry(3, heapAdptPq);
            heapAdptPq.ReplaceKey(entry,10);
            heapAdptPq.PrintPQ();
        }

        [TestMethod]
        public void AdaptablePriorityQueue_ReplaceValue3By10_PrintValue142510()
        {

            var entry = GetEntry(3, heapAdptPq);
            heapAdptPq.ReplaceValue(entry, 10);
            heapAdptPq.PrintPQ();
        }
    }
}