using Algorithms.Others;

namespace Algorithms.Test
{

    [TestClass]
    public class OthersTests
    {
        private readonly int[] sortedArray = new int[] { 1, 3, 5, 8, 10, 15, 20 };


        public OthersTests()
        {

        }

        [TestMethod]
        public void BinarySearch_Test()
        {
            var result = BinarySearch<int>.Search(sortedArray, 15, null, null);
            Assert.AreEqual(5, result);
            var notFoundResult = BinarySearch<int>.Search(sortedArray, 2, null, null);
            Assert.AreEqual(-1, notFoundResult);
        }

        [TestMethod]
        public void Huffman_Encoding_Test()
        {
            var input = "BCAADDDCCACACAC";
            var coder = new HuffmanCoding();
            var compressedInput = coder.Encode(input);
            var trie = coder.Trie;
            var result = HuffmanCoding.Decode(compressedInput, trie);
            Assert.AreEqual(input, result);
        }

        [TestMethod]
        public void LCS_Test()
        {
            String S1 = "ACADB";
            String S2 = "CBDA";
            int m = S1.Length;
            int n = S2.Length;
            LongestCommonSubsequence.GetLCS(S1, S2, m, n);
        }
    }
}