using System.Drawing;

namespace Algorithms.TextProcessing
{
    public class HuffmanCoding
    {
        private readonly Dictionary<char, string> EncodingDic = new();
        public HuffmanNode Trie { get; set; }

        public HuffmanCoding()
        {
            this.Trie = new HuffmanNode();
        }

        public class HuffmanNode //node class for trie
        {
            public int Freq { get; }
            public char Character { get; }
            public HuffmanNode? Left { get; set; }
            public HuffmanNode? Right { get; set; }

            public HuffmanNode() { }
            public HuffmanNode(int freq, char c, HuffmanNode? left = null, HuffmanNode? right = null)
            {
                this.Freq = freq;
                this.Character = c;
                this.Left = left;
                this.Right = right;
            }
        }

        public string Encode(string input)
        {
            var frequencyDic = new Dictionary<char, int>();
            var pq = new PriorityQueue<HuffmanNode, int>();

            //calculat character frequency
            foreach (char c in input)
            {
                if (frequencyDic.ContainsKey(c))
                    frequencyDic[c]++;
                else
                    frequencyDic.Add(c, 1);
            }

            //put frequency in priority queue
            foreach (var pair in frequencyDic)
            {
                var node = new HuffmanNode(pair.Value, pair.Key); //frequency, character
                pq.Enqueue(node, pair.Value);
            }

            //construct Trie
            while (pq.Count > 1)
            {
                var f1 = pq.Peek().Freq;
                var t1 = pq.Dequeue();
                var f2 = pq.Peek().Freq;
                var t2 = pq.Dequeue();
                var node = new HuffmanNode(f1 + f2, '*', t1, t2);
                pq.Enqueue(node, f1 + f2);
            }
            Trie = pq.Dequeue();

            GetCode(Trie, ""); // Get character codes from Trie

            //encode input string use character codes
            var endcodedInput = "";
            foreach (var c in input)
                endcodedInput += EncodingDic[c];
            return endcodedInput;
        }

        // Get character codes from Trie
        public void GetCode(HuffmanNode node, string code)
        {
            if (node.Left == null && node.Right == null)
                EncodingDic.Add(node.Character, code);
            else
            {
                if (node.Left != null) GetCode(node.Left, code + "0");
                if (node.Right != null) GetCode(node.Right, code + "1");
            }
        }

        //Decode encodedString using trie
        public static string Decode(string encodedString, HuffmanNode root)
        {
            var decodedString = "";
            var cursor = root; //cursor points to root of trie
            for (var i = 0; i < encodedString.Length; i++)
            {
                if (encodedString[i] == '0')
                {
                    cursor = cursor.Left;
                }
                else
                {
                    cursor = cursor.Right;
                }
                if (cursor!.Left == null && cursor.Right == null)
                {
                    decodedString += cursor.Character;
                    cursor = root;
                }
            }
            return decodedString;
        }
    }
}
