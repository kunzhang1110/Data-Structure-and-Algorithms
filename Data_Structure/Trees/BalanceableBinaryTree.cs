using Data_Structure.Maps;
using Data_Structure.Lists;

namespace Data_Structure.Trees
{
    public class BalanceableBinaryTree<K, V> : LinkedBinaryTree<Entry<K, V>>
    {
        public override Node<Entry<K, V>> CreateNode(Entry<K, V> e, Node<Entry<K, V>>? parent,
                Node<Entry<K, V>>? left, Node<Entry<K, V>>? right)
        {
            return new BSTNode<Entry<K, V>>(e, parent, left, right);
        }

        public int GetAux(Node<Entry<K, V>> p)
        {
            if (p is not BSTNode<Entry<K, V>>)
                throw new Exception("Not valid type");
            var bstNode = (BSTNode<Entry<K, V>>)p;
            return bstNode.Aux;
        }

        public void SetAux(Node<Entry<K, V>> p, int value)
        {
            var bstNode = (BSTNode<Entry<K, V>>)p;
            bstNode.Aux = value;
        }

        private void Relink(Node<Entry<K, V>> parent, Node<Entry<K, V>> child,
                bool makeLeftChild)
        {
            child.Parent = parent;
            if (makeLeftChild)
                parent.Left = child;
            else
                parent.Right = child;
        }

        public void Rotate(Node<Entry<K, V>> p)
        {    //Rotates Node p above its parent.
            Node<Entry<K, V>> x = Validate(p);
            Node<Entry<K, V>> y = x.Parent!;    //assume parent exists
            Node<Entry<K, V>>? z = y.Parent;    //z is grandparent of x
            if (z == null)
            {
                Root = x;
                x.Parent = default;
            }
            else
                Relink(z, x, y == z.Left);  // x becomes direct child of z

            if (x == y.Left)
            {
                Relink(y, x.Right, true);   // x's right child becomes y's left
                Relink(x, y, false);    // y becomes x's right child
            }
            else
            {
                Relink(y, x.Left, false);   // x's left child becomes y's right
                Relink(x, y, true);     // y becomes left child of x
            }
        }

        public Node<Entry<K, V>> Restructure(Node<Entry<K, V>> x)
        {
            Node<Entry<K, V>>? y = Parent(x);
            Node<Entry<K, V>>? z = Parent(y);
            if ((x == Right(y)) == (y == Right(z))) // matching alignments
            {
                Rotate(y);  // single rotation (of y)
                return y;
            }
            else    // opposing alignments
            {
                Rotate(x);  // double rotation (of x)
                Rotate(x);
                return x;
            }
        }
    }
}

