using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Lists
{
    public class ArrayList<E> : IEnumerable<E>
    {
        public int Size { get; set; }
        public E?[] data; //initial capacity is 2

        public ArrayList(int capacity)
        {
            this.data = new E[capacity];
        }

        public ArrayList() : this(20) { }


        public E? Get(int i)
        {
            return data[i];
        }

        public E? Set(int i, E e)
        {
            E? temp = data[i];
            data[i] = e;
            return temp;
        }

        public void Resize(int capacity) //O(n)
        {
            E?[] temp = new E[capacity];
            for (int k = 0; k < Size; k++) //copy exising elements
                temp[k] = data[k];
            data = temp;
        }

        public void Add(int i, E e)
        {//Add e to position i and move data[i+1] rightwards
            if (Size == data.Length)
                Resize(2 * data.Length);
            for (int k = Size - 1; k >= i; k--) data[k + 1] = data[k];
            data[i] = e;
            Size++;
        }

        public void Add(E e)
        {//Add last
            Add(Size, e);
        }

        public E? Remove(int i) //O(n)
        {
            E? temp = data[i];
            for (int k = i; k < Size - 1; k++) // shift elements to fill hole
                data[k] = data[k + 1];
            Size--;
            return temp;
        }

        IEnumerator<E> IEnumerable<E>.GetEnumerator()
        {
            var copy = new E[Size];
            for (var i = 0; i < Size; i++) copy[i] = data[i]!;
            return copy.Cast<E>().GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            var copy = new E[Size];
            for (var i = 0; i < Size; i++) copy[i] = data[i]!;
            return copy.GetEnumerator();
        }
    }

}





