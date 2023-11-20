using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Basic_Structures;

namespace Data_Structure.Lists
{
    public class Queue<E>
    {
        public SinglyLinkedList<E> list = new SinglyLinkedList<E>();
        public int Size { get; set; }
        public bool IsEmpty() { return Size == 0; }
        public void Enqueue(E element) { list.AddLast(element); }
        public E? Dequeue() { return list.FirstElement(); }
    }
}



