using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Basic_Structures
{
    public class SinglyLinkedList<E>
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

        public Node<E>? Head { get; set; }
        public Node<E>? Tail { get; set; }
        public int Size { get; set; }

        // update methods
        public void AddFirst(E e) // add element E to the front
        {
            Head = new Node<E>(e, Head); // next is current Head; Head points to new Node
            if (IsEmpty())
                Tail = Head;
            Size++;
        }

        public void AddLast(E e)
        { // add element E to the last
            var newest = new Node<E>(e, default);
            if (IsEmpty())
                Head = newest; // Head points to newest
            else
                Tail!.Next = newest; // set current Tail points to new Node
            Tail = newest; // newest is the Tail
            Size++;
        }

        public E? RemoveFirst()
        { // remove and return first element
            if (IsEmpty()) return default;
            E returnElement = FirstElement()!; // get first element
            Head = Head!.Next; // Head points to Head.next
            Size--;
            if (IsEmpty())
                Tail = null;
            return returnElement;
        }

        public void PrintList()
        {
            if (!IsEmpty())
            {
                var pointer = Head!; // pointer to current element
                for (var i = Size; i > 0; i--)
                {
                    Console.WriteLine(pointer!.Element!.ToString());
                    pointer = pointer.Next; // pointer points to next Node
                }
            }
        }

        public SinglyLinkedList<E> Clone()
        {
            SinglyLinkedList<E> copy = new SinglyLinkedList<E>();
            if (Size > 0)
            {
                var pointer = Head;
                while (pointer != null)
                {
                    copy.AddLast(pointer.Element);
                    pointer = pointer.Next;
                }
            }
            return copy;
        }

        // access methods
        public bool IsEmpty()
        {
            return Size == 0;
        }
        public E? FirstElement()
        {
            if (IsEmpty())
                return default;
            return Head!.Element;
        }
        public E? LastElement()
        {
            if (IsEmpty())
                return default;
            return Tail!.Element;
        }
    }
}
