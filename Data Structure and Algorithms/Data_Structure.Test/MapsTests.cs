using Data_Structure.Maps;

namespace Data_Structure.Test
{
    [TestClass]
    public class MapsTests
    {
        [TestMethod]
        public void UnsortedListMap_PutRemove_Print1425()
        {
            var listMap = new UnsortedListMap<int, string>();
            foreach (var i in new int[] { 1, 3, 2, 5, 4 })
                listMap.Put(i, i.ToString());
            listMap.Remove(3);
            foreach (var e in listMap.EntrySet())
            {
                Console.WriteLine($"{e.Key}");
            }
        }

        [TestMethod]
        public void ChainHashMap_Put_Print()
        {
            var chainHashMap = new ChainHashMap<int, string>(10);
            foreach (var i in new int[] { 1, 3, 2, 5, 4 })
                chainHashMap.Put(i, "str " + i.ToString());
            chainHashMap.Remove(44);
            for (var i = 0; i < chainHashMap.table.Length; i++)
            {
                if (chainHashMap.table[i] == null) continue;
                if (chainHashMap.table[i].Size > 0)
                {
                    Console.WriteLine($"Bucket{i}");
                    foreach (var e in chainHashMap.table[i].table.data)
                    {
                        if (e != null)
                            Console.WriteLine($"{e.Key} {e.Value}");
                    }
                }
            }
        }

        [TestMethod]
        public void ProbeHashMap_PutRemove_Print()
        {
            var probeHashMap = new ProbeHashMap<int, string>(10);
            foreach (var i in new int[] { 1, 3, 2, 5, 4 })
                probeHashMap.Put(i, "str " + i.ToString());
            probeHashMap.Remove(44);
            foreach (var e in probeHashMap.EntrySet())
            {
                Console.WriteLine($"{e.Key} {e.Value}");
            }
        }

        [TestMethod]
        public void SortedTable_PutRemove_Print1245()
        {
            var sortedTableMap = new SortedTableMap<int, string>();
            foreach (var i in new int[] { 1, 3, 2, 5, 4 })
                sortedTableMap.Put(i, "str " + i.ToString());
            sortedTableMap.Remove(3);
            foreach (var e in sortedTableMap.EntrySet())
            {
                Console.WriteLine($"{e.Key} {e.Value}");
            }
        }
        [TestMethod]
        public void SortedTable_SubMap_Print34()
        {
            var sortedTableMap = new SortedTableMap<int, string>();
            foreach (var i in new int[] { 1, 3, 2, 5, 4 })
                sortedTableMap.Put(i, "str " + i.ToString());
            foreach (var e in sortedTableMap.SubMap(3, 5))
            {
                Console.WriteLine($"{e.Key} {e.Value}");
            }
        }
    }
}