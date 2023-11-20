using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure.Lists
{
    public partial class PositionalList<E> : IEnumerable
    {

        public IEnumerator GetEnumerator()
        {
            return new PositionalEnumerator(this);
        }

        public class PositionalEnumerator : IEnumerator
        {
            private readonly PositionalList<E> _list;
            private Node<E>? _cursor;
            public Node<E>? Current
            {
                get
                {
                    return _cursor!;
                }
            }
            public PositionalEnumerator(PositionalList<E> list)
            {
                _list = list;
                _cursor = list.Header;
            }

            object? IEnumerator.Current
            {
                get { return Current; }
            }

            public bool MoveNext()
            {
                if (_cursor != _list.Trailer)
                {
                    _cursor = _cursor!.Next;
                }
                return (_cursor != _list.Trailer);
            }
            public void Reset()
            {
                _cursor = _list.Header;
            }
        }
    }
}




