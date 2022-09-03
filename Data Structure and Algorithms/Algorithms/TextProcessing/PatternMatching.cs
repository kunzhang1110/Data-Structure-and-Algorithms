namespace Algorithms.TextProcessing
{
    public class PatternMatching
    {
        /** Returns the lowest index at which substring pattern begins in text (or else -1).*/

        public static int FindBrute(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;
            for (var i = 0; i <= n - m; i++)
            {
                int k = 0;
                while (k < m && text[i + k] == pattern[k]) // kth character of pattern matches
                    k++;
                if (k == m)
                    return i;
            }
            return -1;
        }

        public static int FindBoyerMoore(string text, string pattern)
        {
            int n = text.Length;
            int m = pattern.Length;
            if (m == 0) return 0;
            int i, k;

            var last = new Dictionary<char, int>(); // the 'last' map
            for (i = 0; i < n; i++)
                last[text[i]] = -1;  // set -1 as default for all text characters
            for (k = 0; k < m; k++)
                last[pattern[k]] = k;    // rightmost occurrence in pattern is last

            // start with the end of the pattern aligned at index m-1 of the text
            i = m - 1; // an index into the text
            k = m - 1; // an index into the pattern
            while (i < n)
            {
                if (text[i] == pattern[k]) // a matching character
                {
                    if (k == 0) return i;   // entire pattern has been found
                    i--;    //examine previous characters of text/pattern
                    k--;
                }
                else
                {
                    if (last[text[i]] < k) //case 1, last < k;
                    {
                        i += m - (last[text[i]] + 1);
                    }
                    else //case 2, last > k
                    {
                        i += m - k;
                    }
                    k = m - 1; // restart at end of pattern
                }
            }
            return -1;
        }


        public static int FindKMP(string text, string pattern)
        {
            var n = text.Length;
            var m = pattern.Length;
            if (m == 0) return 0;
            var fail = ComputeFailKMP(pattern);
            var j = 0;  // index into text
            var k = 0;  // index into pattern
            while (j < n)
            {
                if (text[j] == pattern[k])
                {
                    if (k == m - 1) return j - m + 1;   // match is complete
                    j++;
                    k++;
                }
                else if (k > 0)
                    k = fail[k - 1];    // reuse suffix of P[0..k-1]
                else
                    j++;
            }
            return -1;
        }

        private static int[] ComputeFailKMP(string pattern)
        {
            var m = pattern.Length;
            var fail = new int[m];
            var j = 1;
            var k = 0;
            while (j < m)
            { // compute fail[j]
                if (pattern[j] == pattern[k])
                { // k + 1 characters match thus far
                    fail[j] = k + 1;
                    j++;
                    k++;
                }
                else if (k > 0) // k follows a matching prefix
                    k = fail[k - 1];
                else
                    j++;
            }
            return fail;
        }
    }
}
