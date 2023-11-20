using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Basic_Structures;

namespace Data_Structure.Lists
{
    public class Stack<E>
    {
        public SinglyLinkedList<E> list = new SinglyLinkedList<E>();
        public int Size { get; set; }
        public void Push(E element) { list.AddFirst(element); }
        public E? Top() { return list.FirstElement(); }
        public E? Pop() { return list.RemoveFirst(); }
        public bool IsEmpty() { return list.Size == 0; }
    }
}




