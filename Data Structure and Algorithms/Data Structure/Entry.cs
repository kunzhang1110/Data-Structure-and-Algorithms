using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Structure
{
    public class Entry<A, B>
    {
        public A Key { get; set; }
        public B Value { get; set; }
        public Entry(A key, B value)
        {
            Key = key;
            Value = value;
        }
    }
}
