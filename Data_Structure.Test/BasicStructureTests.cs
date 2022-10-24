using Data_Structure.Basic_Structures;

namespace Data_Structure.Test
{
    [TestClass]
    public class BasicStructureTests
    {

        [TestMethod]
        public void DynamicArray_AddThreeElements_ArrayLengthIsFour()
        {
            var dArray = new DynamicArray<int>();
            dArray.Add(0, 1);
            dArray.Add(1, 3);
            dArray.Add(2, 2);
            Assert.AreEqual(4, dArray.data.Length);
        }

        [TestMethod]
        public void SinglyLinkedList_AddFirst3Elements_Remove1Eement_Print231()
        {
            var list = new SinglyLinkedList<int?>();
            foreach (var i in new int[] { 1, 3, 2, 5 })
                list.AddFirst(i);
            list.RemoveFirst();
            list.PrintList();
        }

        [TestMethod]
        public void SinglyLinkedList_Clone_Print1325()
        {
            var list = new SinglyLinkedList<int?>();
            foreach (var i in new int[] { 1, 3, 2, 5 })
                list.AddLast(i);
            var copy = list.Clone();
            copy.PrintList();
        }

        [TestMethod]
        public void DoublyLinkedList_Add3Elements_Print1325()
        {
            var list = new DoublyLinkedList<int?>();
            foreach (var i in new int[] { 1, 3, 2, 5 })
                list.AddLast(i);
            list.PrintList();
        }

        [TestMethod]
        public void DoublyLinkedList_RemoveElement3_Print12()
        {
            var list = new DoublyLinkedList<int?>();
            list.AddLast(1);
            list.AddLast(3);
            var pointer = list.Last();
            list.AddLast(2);
            list.Remove(pointer);
            list.PrintList();
        }


        [TestMethod]
        public void CircularLinkedList_AddFirstRemoveFirst_Print231()
        {
            var list = new CircularLinkedList<int?>();
            foreach (var i in new int[] { 1, 3, 2, 5 })
                list.AddFirst(i);
            list.RemoveFirst();
            list.PrintList();
        }

        [TestMethod]
        public void CircularLinkedList_AddLast_Print132()
        {
            var list = new CircularLinkedList<int?>();
            foreach (var i in new int[] { 1, 3, 2, 5 })
                list.AddLast(i);
            list.RemoveLast();
            list.PrintList();
        }

    }
}