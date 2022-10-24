using System;

namespace Data_Structure.Lists
{
    public partial class PositionalList<E>
    {
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
            var newest = new Node<E>(e, pre, next);
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

        public Node<E> AddFirst(E e)
        {
            return AddBetween(e, Header, Header.Next);
        }

        public Node<E> AddLast(E e)
        {
            return AddBetween(e, Trailer.Prev, Trailer);
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




