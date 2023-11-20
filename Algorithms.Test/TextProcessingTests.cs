using Algorithms.TextProcessing;

namespace Algorithms.Test
{

    [TestClass]
    public class TextProcessingTests
    {
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

        [TestMethod]
        public void PatternMatching_BruteForce_Equals10()
        {
            var text = "abacaabadcabacabaabb";
            var pattern = "abacab";
            var expected = 10;
            Assert.AreEqual(expected, PatternMatching.FindBrute(text, pattern));
        }

        [TestMethod]
        public void PatternMatching_BoyerMoore_Equals10()
        {
            var text = "abacaabadcabacabaabb";
            var pattern = "abacab";
            var expected = 10;
            Assert.AreEqual(expected, PatternMatching.FindBoyerMoore(text, pattern));
        }

        [TestMethod]
        public void PatternMatching_KMP_Equals10()
        {
            var text = "abacaabadcabacabaabb";
            var pattern = "abacab";
            var expected = 10;
            Assert.AreEqual(expected, PatternMatching.FindKMP(text, pattern));
        }
    }
}