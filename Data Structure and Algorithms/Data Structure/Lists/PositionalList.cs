using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Lists
{
    public partial class PositionalList<E>
    {
        public class Node<T>
        {
            public E? Element { get; set; }
            public Node<E>? Prev { get; set; }
            public Node<E>? Next { get; set; }
            public Node(E? e, Node<E>? prev, Node<E>? next)
            {
                Element = e;
                Prev = prev;
                Next = next;
            }
        }
        public Node<E> Header { get; set; } // Header sentinel
        public Node<E> Trailer { get; set; }// Trailer sentinel
        public int Size { get; set; }

        public PositionalList()
        {
            Header = new Node<E>(default, default, default); ;
            Trailer = new Node<E>(default, Header, default);
            Header.Next = Trailer;
        }

        public Node<E> AddBetween(E e, Node<E> pre, Node<E> next)
        {
            Node<E> newest = new Node<E>(e, pre, next);
            pre.Next = newest;
            next.Prev = newest;
            Size++;
            return newest;
        }

        public Node<E> AddBefore(Node<E> node, E e)
        {
            return AddBetween(e, node.Prev, node);
        }

        public Node<E> AddAfter(Node<E> node, E e)
        {
            return AddBetween(e, node, node.Next);
        }

        public void AddLast(E e)
        {
            AddBetween(e, Trailer.Prev, Trailer);
        }

        public E? Set(Node<E> node, E e)
        {
            E? old = node.Element;
            node.Element = e;
            return old;
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
                Node<E> pointer = Header.Next!; // pointer to current element
                if (pointer == null) return;
                for (var i = Size; i > 0; i--)
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




