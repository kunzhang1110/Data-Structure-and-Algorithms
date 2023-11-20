using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Trees
{
    public class Node<E> 
    {
        public E? Element { get; set; }
        public Node<E>? Parent { get; set; }
        public Node<E>? Left { get; set; }
        public Node<E>? Right { get; set; }
        public Node(E element, Node<E>? parent, Node<E>? left, Node<E>? right)
        {
            Element = element;
            Parent = parent;
            Left = left;
            Right = right;
        }
    }

    public class BSTNode<E> : Node<E>
    {
        public int Aux { get; set; }
        public BSTNode(E e, Node<E> parent, Node<E> leftChild, Node<E> rightChild) : base(e, parent, leftChild, rightChild) { }
    }
}
