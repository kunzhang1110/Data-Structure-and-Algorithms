using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Basic_Structures
{
    public class DoublyLinkedList<E>
    {
        public class Node<T>
        {
            public T? Element { get; set; }
            public Node<T>? Prev { get; set; }
            public Node<T>? Next { get; set; }
            public Node(T? e, Node<T>? prev, Node<T>? next)
            {
                Element = e;
                Prev = prev;
                Next = next;
            }
        }
        public Node<E> Header { get; set; } // Header sentinel
        public Node<E> Trailer { get; set; }// Trailer sentinel
        public int Size { get; set; }


        public DoublyLinkedList()
        { // constructor
            Header = new Node<E>(default, default, default); ;
            Trailer = new Node<E>(default, Header, default);
            Header.Next = Trailer;
        }

        public void AddBetween(E e, Node<E> pre, Node<E> next)
        {
            Node<E> newest = new Node<E>(e, pre, next);
            pre.Next = newest;
            next.Prev = newest;
            Size++;
        }

        public void AddLast(E e)
        {
            AddBetween(e, Trailer.Prev, Trailer);
        }

        public E Remove(Node<E> node)
        {
            Node<E> pre = node.Prev!;
            Node<E> next = node.Next!;
            pre.Next = next;
            next.Prev = pre;
            Size--;
            return node.Element!;
        }

        public void PrintList()
        {
            if (!IsEmpty())
            {
                Node<E> pointer = Header; // pointer to current element
                for (var i = Size; i >= 0; i--)
                {
                    Console.WriteLine(pointer!.Element!.ToString() + "");
                    pointer = pointer.Next!; // pointer points to next Node
                }
            }
        }

        // access methods
        public bool IsEmpty()
        {
            return Size == 0;
        }

        public Node<E>? GetHeader()
        {
            return Header;
        }

        public Node<E>? First()
        {
            return Header.Next;
        }

        public Node<E>? Last()
        {
            return Trailer.Prev;
        }
    }
}
