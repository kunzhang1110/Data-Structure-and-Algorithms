using Data_Structure.Trees;

namespace Data_Structure.Test
{
    [TestClass]
    public class TreeTests
    {

        private readonly LinkedBinaryTree<int> lbTree = new();
        private readonly Node<int> lbTree_five;
        private readonly TreeMap<int, string> bsTree = new();
        private readonly AVLTreeMap<int, string> avlTree = new();
        private readonly SplayTreeMap<int, string> spTree = new();

        public TreeTests()
        {
            //Linked Binary Tree
            var root = lbTree.AddRoot(1);
            lbTree.AddLeft(root, 3);
            var right = lbTree.AddRight(root, 2);
            lbTree_five = lbTree.AddLeft(right, 5)!;
            lbTree.AddRight(right, 4);

            //Binary Search Tree Map
            foreach (var key in new int[] { 44, 17, 32, 78, 50, 48, 62, 88 })
                bsTree.Put(key, "str " + key);

            //AVL Tree Map
            foreach (var key in new int[] { 44, 17, 32, 78, 50, 48, 62, 88 })
                avlTree.Put(key, "str " + key);

            //Splay Tree Map
            foreach (var key in new int[] { 44, 17, 32, 78, 50, 48, 62, 88 })
                spTree.Put(key, "str " + key);

        }
        [TestMethod]
        public void LinkedBinaryTree_InorderTraversal_Print31524()
        {
            foreach (var node in lbTree.Positions())
            {
                Console.WriteLine(node.Element);
            }
        }

        [TestMethod]
        public void LinkedBinaryTree_Remove_Print3124()
        {
            lbTree.Remove(lbTree_five);//remove 5

            foreach (var node in lbTree.Positions())
            {
                Console.WriteLine(node.Element);
            }
        }

        [TestMethod]
        public void LinkedBinaryTree_Attach_Print31768524()
        {
            LinkedBinaryTree<int> newTree = new();
            var root = newTree.AddRoot(6);
            newTree.AddLeft(root, 7);
            newTree.AddRight(root, 8);

            lbTree.Attach(lbTree_five, newTree, new LinkedBinaryTree<int>());

            foreach (var node in lbTree.Positions())
            {
                Console.WriteLine(node.Element);
            }
        }

        [TestMethod]
        public void TreeMap_Print_PrintAll()
        {
            bsTree.Print();
        }
        [TestMethod]
        public void TreeMap_TreeMin_Print88()
        {
            Console.WriteLine(bsTree.TreeMax(bsTree.tree.Root)!.Element.Key);
        }
        [TestMethod]
        public void TreeMap_CeilingEntry_Print50()
        {
            Console.WriteLine(bsTree.CeilingEntry(49)!.Key);
        }
        [TestMethod]
        public void TreeMap_LowerEntry_Print48()
        {
            Console.WriteLine(bsTree.LowerEntry(49)!.Key);
        }
        [TestMethod]
        public void TreeMap_SubMap_Print334448()
        {
            foreach (var entry in bsTree.SubMap(20, 50))
                Console.WriteLine(entry.Key);
        }

        [TestMethod]
        public void AVLTree_Print_PrintAll()
        {
            avlTree.Print();
        }

        [TestMethod]
        public void AVLTree_Insert54_Print()
        {
            avlTree.Put(54, "str " + 54);
            avlTree.Print();
        }

        [TestMethod]
        public void AVLTree_Insert54Remove32_Print()
        {
            avlTree.Put(54, "str " + 54);
            avlTree.Remove(32);
            avlTree.Print();
        }

        [TestMethod]
        public void SpTree_Insert54Remove32_Print()
        {
            spTree.Put(54, "str " + 54);
            spTree.Remove(32);
            spTree.Print();
        }
    }
}