using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Structure.Trees;

namespace Data_Structure
{
    public interface Position<E>
    {
        E? Element { get; set; }
        Node<E>? Parent { get; set; }
    }
}
