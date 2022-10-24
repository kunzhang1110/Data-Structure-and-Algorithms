using System;
using System.Collections.Generic;
using Data_Structure.Maps;
using Data_Structure.Lists;
using Data_Structure.Priority_Queues;

namespace Data_Structure.Graphs
{
    public partial class Graph<V, E>
    {
        /**
        * Performs depth-first search of the unknown portion of Graph g starting at Vertex u.
*/
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

        /** Performs DFS for the entire graph and returns the DFS forest as a map. */
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

        /** Returns an ordered list of edges comprising the directed path from u to v.*/
        public static PositionalList<Edge<E>> ConstructPath(Graph<V, E> g, Vertex<V> u, Vertex<V> v,
                Dictionary<Vertex<V>, Edge<E>> forest)
        {
            var path = new PositionalList<Edge<E>>();
            if (forest[v] != null)
            {
                Vertex<V> walk = v;                  // we construct the path from back to front
                while (walk != u)
                {
                    Edge<E> edge = forest[walk];
                    path.AddFirst(edge);               // add edge to *front* of path
                    walk = g.Opposite(walk, edge);     // repeat with opposite endpoint
                }
            }
            return path;
        }

        /** Performs depth-first search of the unknown portion of Graph g starting at Vertex u.*/
        public static void BFS(Graph<V, E> g, Vertex<V> s,
                    HashSet<Vertex<V>> visited, Dictionary<Vertex<V>, Edge<E>> forest)
        {
            var level = new PositionalList<Vertex<V>>();
            visited.Add(s);
            level.AddLast(s); // first level includes only s
            while (!level.IsEmpty())
            {
                var nextLevel = new PositionalList<Vertex<V>>();
                foreach (Node<Vertex<V>> node in level)
                {
                    var u = node.Element;
                    foreach (Edge<E> e in g.OutgoingEdges(u))
                    {
                        Vertex<V> v = g.Opposite(u, e);
                        if (!visited.Contains(v))
                        {
                            visited.Add(v);
                            forest.Add(v, e);   // e is the tree edge that discovered v
                            nextLevel.AddLast(v);   // v will be further considered in next pass
                        }
                    }
                    level = nextLevel;  // relabel 'next' level to become the current
                }
            }
        }

        /** Performs BFS for the entire graph and returns the BFS forest as a map.*/
        public static Dictionary<Vertex<V>, Edge<E>> BFSComplete(Graph<V, E> g)
        {
            var visited = new HashSet<Vertex<V>>();
            var forest = new Dictionary<Vertex<V>, Edge<E>>();
            foreach (Node<Vertex<V>> node in g.Vertices)
            {
                var u = node.Element!;
                if (!visited.Contains(u))
                    BFS(g, u, visited, forest);
            }
            return forest;
        }

        /** Converts graph g into its transitive closure useing the Floyd-Warshall algorithm.  */
        public static void TransitiveClosure(Graph<V, E> g)
        {
            foreach (Node<Vertex<V>> nodeK in g.Vertices)
            {
                var k = nodeK.Element;
                foreach (Node<Vertex<V>> nodeI in g.Vertices)
                {
                    var i = nodeI.Element;
                    // verify that edge (i,k) exists in the partial closure
                    if (i != k && g.GetEdge(i, k) != null)
                        foreach (Node<Vertex<V>> nodeJ in g.Vertices)
                        {
                            var j = nodeJ.Element;
                            // verify that edge (k,j) exists in the partial closure
                            if (i != j && j != k && g.GetEdge(k, j) != null)
                                // if (i,j) not yet included, add it to the closure
                                if (g.GetEdge(i, j) == null)
                                    g.InsertEdge(i, j, default);
                        }
                }
            }
        }

        /** Returns a list of verticies of directed acyclic graph g in topological order. */
        public static PositionalList<Vertex<V>> TopologicalSort(Graph<V, E> g)
        {

            var topo = new PositionalList<Vertex<V>>();  // list of vertices placed in topological order
            var ready = new Data_Structure.Lists.Stack<Vertex<V>>(); // vertices that have 0 remaining constraints
            var incomingCount = new ProbeHashMap<Vertex<V>, int>(); // map keeping track of remaining incoming edges for each vertex
            foreach (Node<Vertex<V>> node in g.Vertices)
            {
                var u = node.Element;
                incomingCount.Put(u, g.InDegree(u)); // initialize with actual incoming edges
                if (incomingCount.Get(u) == 0) // if u has no incoming edges,
                    ready.Push(u); // it is free of constraints
            }
            while (!ready.IsEmpty())
            {
                var u = ready.Pop();
                topo.AddLast(u);
                foreach (Edge<E> e in g.OutgoingEdges(u))
                { // consider all outgoing neighbors of u
                    Vertex<V> v = g.Opposite(u, e);
                    incomingCount.Put(v, incomingCount.Get(v) - 1); // v has one less constraint without u
                    if (incomingCount.Get(v) == 0)
                        ready.Push(v);
                }
            }
            return topo;
        }

        /** Computes shortest-path distances from src vertex to all reachable vertices using Dijkstra's algorithm*/
        public static ProbeHashMap<Graph<V, int>.Vertex<V>, int?> ShortestPathLengths(Graph<V, int> g, Graph<V, int>.Vertex<V> src)
        {

            var d = new ProbeHashMap<Graph<V, int>.Vertex<V>, int>();  // d.Get(v) is upper bound on distance from src to v
            var cloud = new ProbeHashMap<Graph<V, int>.Vertex<V>, int?>(); // map reachable v to its d value
            var pq = new HeapAdaptablePriorityQueue<int, Graph<V, int>.Vertex<V>>();  // pq will have vertices as elements, with d.Get(v) as key
            var pqTokens = new ProbeHashMap<Graph<V, int>.Vertex<V>, PQEntry<int, Graph<V, int>.Vertex<V>>>();   // maps from vertex to its pq locator


            // for each vertex v of the graph, add an entry to the priority queue, with
            // the source having distance 0 and all others having infinite distance
            foreach (Node<Graph<V, int>.Vertex<V>> node in g.Vertices)
            {
                var v = node.Element;
                if (v == src)
                    d.Put(v, 0);
                else
                    d.Put(v, int.MaxValue);
                var entry = pq.Insert(d.Get(v), v);
                pqTokens.Put(v, entry); // save entry for future updates
            }
            // now begin adding reachable vertices to the cloud
            while (!pq.IsEmpty())
            {
                var entry = pq.RemoveMin();
                int key = entry!.Key;
                var u = entry.Value;
                cloud.Put(u, key); // this is actual distance to u
                pqTokens.Remove(u); // u is no longer in pq
                foreach (Graph<V, int>.Edge<int> e in g.OutgoingEdges(u))
                {
                    var v = g.Opposite(u, e);
                    if (cloud.Get(v) == null)
                    {
                        // perform relaxation step on edge (u,v)
                        int wgt = e.Element;
                        if (d.Get(u) + wgt < d.Get(v))
                        { // better path to v?
                            d.Put(v, d.Get(u) + wgt); // update the distance
                            pq.ReplaceKey(pqTokens.Get(v), d.Get(v)); // update the pq entry
                        }
                    }
                }
            }
            return cloud; // only includes reachable vertices
        }

        /*
            * Reconstructs a shortest-path tree rooted at vertex s, given distance map d.
            * The tree is represented as a map from each reachable vertex v (other than s)
            * to the edge e = (u,v) that is used to reach v from its parent u in the tree.
            */
        public static AbstractHashMap<Graph<V, int>.Vertex<V>, Graph<V, int>.Edge<int>> GetShortestPathTree(Graph<V, int> g, Graph<V, int>.Vertex<V> s, AbstractHashMap<Graph<V, int>.Vertex<V>, int?> d)
        {
            var tree = new ProbeHashMap<Graph<V, int>.Vertex<V>, Graph<V, int>.Edge<int>>();
            foreach (Graph<V, int>.Vertex<V> v in d.KeySet())
                if (v != s)
                    foreach (Graph<V, int>.Edge<int> e in g.IncomingEdges(v))
                    { // consider INCOMING edges
                        var u = g.Opposite(v, e);
                        int wgt = e.Element;
                        if (d.Get(v) == d.Get(u) + wgt)
                            tree.Put(v, e); // edge is is used to reach v
                    }
            return tree;
        }


        /**
            * Computes a minimum spanning tree of connected, weighted graph g using Kruskal's algorithm.
            * Result is returned as a list of edges that comprise the MST (in arbitrary order).
            */
        public static PositionalList<Graph<V, int>.Edge<int>> MST(Graph<V, int> g)
        {

            var tree = new PositionalList<Graph<V, int>.Edge<int>>();    // tree is where we will store result as it is computed
            var pq = new HeapPriorityQueue<int, Graph<V, int>.Edge<int>>();   // pq entries are edges of graph, with weights as keys
            var forest = new Partition<Graph<V, int>.Vertex<V>>(); // union-Find forest of components of the graph
            var positions = new ProbeHashMap<Graph<V, int>.Vertex<V>, Position<Graph<V, int>.Vertex<V>>>(); // map each vertex to the forest position

            foreach (Node<Graph<V, int>.Vertex<V>> node in g.Vertices)
            {
                var v = node.Element;
                positions.Put(v, forest.MakeCluster(v));
            }

            foreach (Node<Graph<V, int>.Edge<int>> node in g.Edges)
            {
                var edge = node.Element;
                pq.Insert(edge!.Element, edge);

            }

            int size = g.NumVertices();

            // while tree not spanning and unprocessed edges remain...
            while (tree.Size != size - 1 && !pq.IsEmpty())
            {
                var entry = pq.RemoveMin();
                var edge = entry!.Value;
                var endpoints = g.EndVertices(edge);
                var a = forest.Find(positions.Get(endpoints[0]));
                var b = forest.Find(positions.Get(endpoints[1]));
                if (a != b)
                {
                    tree.AddLast(edge);
                    forest.Union(a, b);
                }
            }
            return tree;
        }
    }

}
