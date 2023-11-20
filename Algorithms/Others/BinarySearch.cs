namespace Algorithms.Others
{
    public class BinarySearch<A> where A : IComparable<A>
    {
        public static int Search(A[] data, A target, int? low, int? high)
        {
            low ??= 0;
            high ??= data.Length - 1;

            int mid = (int)(low + high) / 2;
            if (low > high) return -1;
            if (target.CompareTo(data[mid]) == 0)
                return mid;
            else if (target.CompareTo(data[mid]) > 0)
                return Search(data, target, mid + 1, high);
            else
                return Search(data, target, low, mid - 1);
        }

    }
}
