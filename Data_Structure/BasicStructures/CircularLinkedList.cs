using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Basic_Structures
{
    public class CircularLinkedList<E>
    {
        public class Node<T>
        {
            public T? Element { get; set; }
            public Node<T>? Next { get; set; }
            public Node(T? e, Node<T>? n)
            {
                Element = e;
                Next = n;
            }
        }

        public Node<E>? Tail { get; set; }
        public int Size { get; set; }

        public void Rotate()
        {
            if (Tail != null)
                Tail = Tail.Next!;  //old Head becomes new Tail
        }
        // update methods

        public void AddFirst(E e)
        {
            var newest = new Node<E>(e, default);
            if (IsEmpty())
            {
                Tail = newest;
                Tail.Next = newest;
            }
            else
            {
                var head = Tail!.Next;
                Tail.Next = newest;
                newest.Next = head;
            }
            Size++;
        }
        public void AddLast(E e)
        {
            AddFirst(e);
            Rotate();
        }

        public void PrintList()
        {
            if (!IsEmpty())
            {
                var head = Tail!.Next; // pointer to Head
                for (var i = Size; i > 0; i--)
                {
                    Console.WriteLine(head!.Element!.ToString());
                    head = head.Next; // pointer points to next Node
                }
            }
        }

        public E? RemoveFirst()
        {
            if (IsEmpty()) return default;
            var head = Tail!.Next;
            Size--;
            if (head == null) return Tail.Element;
            else
            {
                Tail.Next = head.Next;
                return head.Element!;
            }
        }

        public E? RemoveLast()
        {
            for (var i = 0; i < Size - 1; i++) Rotate();
            return RemoveFirst();
        }

        // access methods
        public bool IsEmpty()
        {
            return Size == 0;
        }

    }
}
