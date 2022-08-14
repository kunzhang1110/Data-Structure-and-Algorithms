using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Lists
{
    public partial class PositionalList<E>
    {
        public PositionalIterator GetIterator()
        {
            return new PositionalIterator(this);

        }
        public class PositionalIterator
        {
            private readonly PositionalList<E> _list;
            private Node<E>? _cursor;
            private Node<E>? _recent;

            public PositionalIterator(PositionalList<E> list)
            {
                _list = list;
                _cursor = list.Header;
                _recent = null;
            }

            public bool HasNext()
            {
                return (_cursor != null);
            }

            public Node<E> Next()
            {
                if (_cursor == null)
                    throw new Exception("nothing left");
                _recent = _cursor; // element at this position might later be removed
                _cursor = _cursor.Next;
                return _recent;
            }

            public void Remove()
            {
                if (_recent == null)
                    throw new Exception("nothing to remove");
                _list.Remove(_recent);
                _recent = null; // do not allow remove again until next is called
            }
        }
    }
}




