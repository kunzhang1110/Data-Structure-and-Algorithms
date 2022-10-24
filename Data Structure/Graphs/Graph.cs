using System;
using System.Collections.Generic;
using Data_Structure.Lists;
using Data_Structure.Maps;

namespace Data_Structure.Graphs
{
    public partial class Graph<V, E>
    {
        public bool IsDirected { get; set; }
        public PositionalList<Vertex<V>> Vertices { get; set; }
        public PositionalList<Edge<E>> Edges { get; set; }

        public Graph(bool directed)
        {
            IsDirected = directed;
            Vertices = new PositionalList<Vertex<V>>();
            Edges = new PositionalList<Edge<E>>();
        }

        public int NumVertices() { return Vertices.Size; }
        public int NumEdges() { return Edges.Size; }
        public int OutDegree(Vertex<V> v)
        {
            return v.Outgoing.Size;
        }
        public IEnumerable<Edge<E>> OutgoingEdges(Vertex<V> v)
        {
            return v.Outgoing.Values();
        }
        public int InDegree(Vertex<V> v)
        {
            return v.Incoming.Size;
        }
        public IEnumerable<Edge<E>> IncomingEdges(Vertex<V> v)
        {
            return v.Incoming.Values();   // Edges are the values in the adjacency map
        }

        /** Returns the edge from u to v, or null if they are not adjacent. */
        public Edge<E>? GetEdge(Vertex<V> origin, Vertex<V> dest)
        {
            return origin.Outgoing.Get(dest);    // will be null if no edge from u to v
        }

        /**
            * Returns the Vertices of edge e as an array of length two.
            * If the graph is directed, the first vertex is the origin, and
            * the second is the destination.  If the graph is undirected, the
            * order is arbitrary.
            */
        public Vertex<V>[] EndVertices(Edge<E> e)
        {
            return e.Endpoints;
        }

        /** Returns the vertex that is Opposite vertex v on edge e. */
        public Vertex<V> Opposite(Vertex<V> v, Edge<E> e)
        {
            Vertex<V>[]
            endpoints = e.Endpoints;
            if (endpoints[0] == v)
                return endpoints[1];
            else if (endpoints[1] == v)
                return endpoints[0];
            else
                throw new Exception("v is not incident to this edge");
        }

        /** Inserts and returns a new vertex with the given element. */
        public Vertex<V> InsertVertex(V element)
        {
            var v = new Vertex<V>(element, IsDirected, this);
            v.Node = Vertices.AddLast(v);
            return v;
        }

        /**
            * Inserts and returns a new edge between Vertices u and v, storing given element.
            *
            * @ if u or v are invalid Vertices, or if an edge already exists between u and v.
            */
        public Edge<E> InsertEdge(Vertex<V> origin, Vertex<V> dest, E? element)
        {
            if (GetEdge(origin, dest) == null) //no existing edges
            {
                var e = new Edge<E>(origin, dest, element, this);
                e.Node = Edges.AddLast(e);
                origin.Outgoing.Put(dest, e);
                dest.Incoming.Put(origin, e);
                return e;
            }
            else
                throw new Exception("Edge from u to v exists");
        }

        /** Removes a vertex and all its incident Edges from the graph. */
        public void RemoveVertex(Vertex<V> v)
        {
            // remove all incident Edges from the graph
            foreach (Edge<E> e in v.Outgoing.Values())
                RemoveEdge(e);
            foreach (Edge<E> e in v.Incoming.Values())
                RemoveEdge(e);
            // remove this vertex from the list of Vertices
            Vertices.Remove(v.Node);
            v.Node = null;             // invalidates the vertex
        }


        /** Removes an edge from the graph. */
        public void RemoveEdge(Edge<E> edge)
        {
            // remove this edge from Vertices' adjacencies
            var verts = edge.Endpoints;
            verts[0].Outgoing.Remove(verts[1]);
            verts[1].Incoming.Remove(verts[0]);
            // remove this edge from the list of Edges
            Edges.Remove(edge.Node);
            edge.Node = null;
        }

        //---------------- nested Vertex class ----------------
        public class Vertex<A>
        {
            public V Element { get; set; }
            public Node<Vertex<V>>? Node { get; set; }
            public AbstractHashMap<Vertex<V>, Edge<E>> Outgoing { get; set; }
            public AbstractHashMap<Vertex<V>, Edge<E>> Incoming { get; set; }

            private readonly Graph<V, E> _graph;

            public Vertex(V elem, bool graphIsDirected, Graph<V, E> graph)
            {
                Element = elem;
                Outgoing = new ProbeHashMap<Vertex<V>, Edge<E>>();
                if (graphIsDirected)
                    Incoming = new ProbeHashMap<Vertex<V>, Edge<E>>();
                else
                    Incoming = Outgoing;    // if undirected, alias outgoing map
                _graph = graph;
            }

            /** Validates that this vertex instance belongs to the given graph. */
            public bool Validate(Graph<V, E> graph)
            {
                return (_graph == graph && Node != null);
            }
        } //------------ end of InnerVertex class ------------

        //---------------- nested InnerEdge class ----------------
        public class Edge<B>
        {
            public E Element { get; set; }
            public Node<Edge<E>>? Node { get; set; }
            public Vertex<V>[] Endpoints { get; set; }
            private readonly Graph<V, E> _graph;

            public Edge(Vertex<V> u, Vertex<V> v, E elem, Graph<V, E> graph)
            {
                Element = elem;
                Endpoints = new Vertex<V>[] { u, v };  // array of length 2
                _graph = graph;
            }

            /** Validates that this edge instance belongs to the given graph. */
            public bool Validate(Graph<V, E> graph)
            {
                return _graph == graph && Node != null;
            }
        } //------------ end of InnerEdge class ------------

        public override String ToString()
        {
            var sb = new StringBuilder();
            foreach (Node<Vertex<V>> node in Vertices)
            {
                var v = node.Element!;
                sb.Append("Vertex " + v.Element + "\n");
                if (IsDirected)
                    sb.Append(" [outgoing]");
                sb.Append(" " + OutDegree(v) + " adjacencies:");
                foreach (Edge<E> e in OutgoingEdges(v))
                    sb.Append(String.Format(" ({0}, {1})", Opposite(v, e).Element, e.Element));
                sb.Append("\n");
                if (IsDirected)
                {
                    sb.Append(" [incoming]");
                    sb.Append(" " + InDegree(v) + " adjacencies:");
                    foreach (Edge<E> e in IncomingEdges(v))
                        sb.Append(String.Format(" ({0}, {1})", Opposite(v, e).Element, e.Element));
                    sb.Append("\n");
                }
            }
            return sb.ToString();
        }
    }
}
