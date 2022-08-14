namespace Algorithms.Sorting
{
    public class BubbleSort<A> where A : IComparable
    {
        public static void Sort(A[] data)
        {
            int size = data.Length;
            for (int i = 0; i < size - 1; i++)
            {
                for (int j = 0; j < size - i - 1; j++)
                    if (data[j].CompareTo(data[j + 1]) > 0)
                    {
                        (data[j + 1], data[j]) = (data[j], data[j + 1]);
                    }
            }
        }
    }
}
