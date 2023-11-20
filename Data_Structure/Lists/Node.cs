namespace Data_Structure.Lists
{
    public class Node<E>:Position<E>
    {
        public E? Element { get; set; }
        public Node<E>? Prev { get; set; }
        public Node<E>? Next { get; set; }

        public Node() { }
        public Node(E? e, Node<E>? prev, Node<E>? next)
        {
            Element = e;
            Prev = prev;
            Next = next;
        }
    }
}




