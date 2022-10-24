namespace Data_Structure.Basic_Structures
{
    public class DynamicArray<E>
    {

        public int Size { get; set; }
        public E?[] data = new E[2]; //initial capacity is 2
        public E? this[int key] //indexer
        {
            get => data[key];
            set => data[key] = value;
        }

        public void Resize(int capacity) //O(n)
        {
            E?[] temp = new E[capacity];
            for (int k = 0; k < Size; k++) //copy exising elements
                temp[k] = data[k];
            data = temp;
        }

        public void Add(int i, E e)
        {
            if (Size == data.Length)
                Resize(2 * data.Length);
            for (int k = Size - 1; k >= i; k--) data[k + 1] = data[k];
            data[i] = e;
            Size++;
        }

        public E? Remove(int i) //O(n)
        {
            E? temp = data[i];
            for (int k = i; k < Size - 1; k++) // shift elements to fill hole
                data[k] = data[k + 1];
            Size--;
            return temp;
        }


    }
}