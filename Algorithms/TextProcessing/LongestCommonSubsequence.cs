namespace Algorithms.TextProcessing
{
    public class LongestCommonSubsequence
    {

public static void GetLCS(String S1, String S2, int m, int n)
{
    int[,] LCS_table = new int[m + 1, n + 1]; //LCS length table
    int i, j;
    // Building the mtrix in bottom-up way
    for (i = 0; i <= m; i++)
    {
        for (j = 0; j <= n; j++)
        {
            if (i == 0 || j == 0)
                LCS_table[i, j] = 0;
            else if (S1[i - 1] == S2[j - 1])
                LCS_table[i, j] = LCS_table[i - 1, j - 1] + 1;
            else
                LCS_table[i, j] = Math.Max(LCS_table[i - 1, j], LCS_table[i, j - 1]);
        }
    }

    int LCS_Length = LCS_table[m, n]; //LCS length
    int index = LCS_Length;

    var lcs = new char[LCS_Length]; //longest subsequence

    //use LCS Table to find path
    i = m;
    j = n;
    while (i > 0 && j > 0)
    {
        Console.WriteLine(i + " " + j);
        if (S1[i-1] == S2[j-1] ) //if matching character
        {
            lcs[index - 1] = S1[i - 1];
            i--;
            j--;
            index--;
        }
        else if (LCS_table[i - 1, j] >= LCS_table[i, j - 1]) //previous column >= previous row
            i--;
        else
            j--;
    }
    // Printing the sub sequences
    Console.Write(String.Join("", lcs));
}
}
}
