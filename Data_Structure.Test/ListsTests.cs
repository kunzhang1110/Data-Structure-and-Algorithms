using Data_Structure.Lists;

namespace Data_Structure.Test
{
    [TestClass]
    public class ListsTests
    {
        [TestMethod]
        public void ArrayList_Enumerator_Print132()
        {
            var list = new ArrayList<string?>();
            foreach (var i in new string[] { "1", "3", "2"})
                list.Add(i);
            foreach (var e in list) Console.WriteLine(e.ToString());
        }

        [TestMethod]
        public void PositionalIterator_HasNext_Print13254()
        {
            var list = new PositionalList<int?>();
            foreach (var i in new int[] { 1, 3, 2, 5, 4 })
                list.AddLast(i);
            var iterator = list.GetIterator();
            while (iterator.HasNext())
            {
                Console.WriteLine(iterator.Next().Element.ToString());
            }
        }

        [TestMethod]
        public void PositionalEnumerator_ForEach_Print13254()
        {
            var list = new PositionalList<int?>();
            foreach (var i in new int[] { 1, 3, 2, 5, 4 })
                list.AddLast(i);
            foreach (Node<int?> node in list)
            {
                Console.WriteLine(node.Element!.Value.ToString());
            }
        }
    }
}