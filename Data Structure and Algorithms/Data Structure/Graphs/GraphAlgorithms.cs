using System;
using System.Collections.Generic;
using Data_Structure.Maps;
using Data_Structure.Lists;

namespace Data_Structure.Graphs
{
    public partial class Graph<V, E>
    {
        public static void DFS(Graph<V, E> g, Vertex<V> u,
                  HashSet<Vertex<V>> visited, Dictionary<Vertex<V>, Edge<E>> forest)
        {
            visited.Add(u); // u has been discovered
            foreach (Edge<E> e in g.OutgoingEdges(u))
            {// for every outgoing edge from u
                Vertex<V> v = g.Opposite(u, e);
                if (!visited.Contains(v))
                {
                    forest.Add(v, e); // e is the tree edge that discovered v
                    DFS(g, v, visited, forest); // recursively explore from v
                }
            }
        }

        public static Dictionary<Vertex<V>, Edge<E>> DFSComplete(Graph<V, E> g)
        {
            var visited = new HashSet<Vertex<V>>();
            var forest = new Dictionary<Vertex<V>, Edge<E>>();
            foreach (Node<Vertex<V>> node in g.Vertices)
            {
                var u = node.Element!;
                if (!visited.Contains(u))
                    DFS(g, u, visited, forest);            // (re)start the DFS process at u
            }

            return forest;
        }

    }
}
