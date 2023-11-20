using Data_Structure.Graphs;
using Data_Structure.Lists;
using Newtonsoft.Json.Linq;
using System;


namespace Data_Structure.Test
{
    [TestClass]
    public class GraphTests
    {
        private readonly Graph<string, int> Fig14_8Graph;
        private readonly Graph<string, int> Fig14_11Graph;
        private readonly Graph<string, int> Fig14_13Graph;
        private readonly Graph<string, int> Fig14_15Graph;
        public GraphTests()
        {
            var Fig14_8Edges = new string[16][] {
                  new string[2]{"BOS","SFO"}, new string[2]{"BOS","JFK"}, new string[2]{"BOS","MIA"}, new string[2]{"JFK","BOS"},
                  new string[2]{"JFK","DFW"}, new string[2]{"JFK","MIA"}, new string[2]{"JFK","SFO"}, new string[2]{"ORD","DFW"},
                  new string[2]{"ORD","MIA"}, new string[2]{"LAX","ORD"}, new string[2]{"DFW","SFO"}, new string[2]{"DFW","ORD"},
                  new string[2]{"DFW","LAX"}, new string[2]{"MIA","DFW"}, new string[2]{"MIA","LAX"}, new string[2]{"SFO","LAX"},
                };
            Fig14_8Graph = GraphFromEdgelist(Fig14_8Edges, true);
            var Fig14_11Edges = new string[13][] {
                  new string[2]{"BOS","JFK"}, new string[2]{"BOS","MIA"}, new string[2]{"JFK","BOS"},
                  new string[2]{"JFK","DFW"}, new string[2]{"JFK","MIA"}, new string[2]{"JFK","SFO"}, new string[2]{"ORD","DFW"},
                  new string[2]{"LAX","ORD"}, new string[2]{"DFW","SFO"}, new string[2]{"DFW","ORD"},
                  new string[2]{"DFW","LAX"}, new string[2]{"MIA","DFW"}, new string[2]{"MIA","LAX"},
                };
            Fig14_11Graph = GraphFromEdgelist(Fig14_11Edges, true);
            var Fig14_13Edges = new string[12][]{
                new string[2]{"A","C"},new string[2] {"A","D"},new string[2] {"B","D"},new string[2] {"B", "F"},new string[2] {"C","D"}, new string[2]{"C","E"},
                new string[2]   {"C","H"},new string[2] {"D","F"}, new string[2]{"E","G"}, new string[2]{"F","G"}, new string[2]{"F","H"},new string[2] {"G","H"}
             };
            Fig14_13Graph = GraphFromEdgelist(Fig14_13Edges, true);
            var Fig14_15Edges = new string[19][]
            {
                new string[3]{"SFO", "LAX", "337"}, new string[3]{"SFO", "BOS", "2704"}, new string[3]{"SFO", "ORD", "1846"},
                new string[3] {"SFO", "DFW", "1464"}, new string[3]{"LAX", "DFW", "1235"}, new string[3]{"LAX", "MIA", "2342"},
                new string[3] {"DFW", "ORD", "802"}, new string[3]{"DFW", "JFK", "1391"},new string[3] {"DFW", "MIA", "1121"},
                new string[3]{"ORD", "BOS", "867"}, new string[3]{"ORD", "PVD", "849"}, new string[3]{"ORD", "JFK", "740"},
                new string[3]{"ORD", "BWI", "621"}, new string[3]{"MIA", "BWI", "946"}, new string[3]{"MIA", "JFK", "1090"},
                new string[3]{"MIA", "BOS", "1258"}, new string[3]{"BWI", "JFK", "184"}, new string[3]{"JFK", "PVD", "144"},
                new string[3] {"JFK", "BOS", "187"}
            };
            Fig14_15Graph = GraphFromEdgelist(Fig14_15Edges, false);

        }


        private static Graph<string, int> GraphFromEdgelist(string[][] edges, bool directed)
        {
            var g = new Graph<string, int>(directed);

            // first pass to get sorted set of vertex labels
            var labels = new SortedSet<string>();
            foreach (var edge in edges)
            {
                labels.Add(edge[0]);
                labels.Add(edge[1]);
            }

            //create vertices (in alphabetical order)
            var verts = new Dictionary<string, Graph<string, int>.Vertex<string>>();
            foreach (var label in labels)
            {
                verts.Add(label, g.InsertVertex(label));
            }

            // now add edges to the graph
            foreach (string[] edge in edges)
            {
                var cost = (edge.Length == 2 ? 1 : int.Parse(edge[2]));
                g.InsertEdge(verts[edge[0]], verts[edge[1]], cost);
            }
            return g;
        }

        private static Graph<string, int>.Vertex<string>? FindVertexByName(PositionalList<Graph<string, int>.Vertex<string>> vertices, string name)
        {
            foreach (Node<Graph<string, int>.Vertex<string>> node in vertices)
            {
                var vertex = node.Element;
                if (vertex.Element == name)
                    return vertex;
            }
            return null;
        }

        [TestMethod]
        public void Graph_Print_Fig14_6()
        {
            var g = new Graph<string, string>(false);
            var verts = new Dictionary<string, Graph<string, string>.Vertex<string>>();
            foreach (var s in new string[] { "u", "v", "w", "z" })
            {
                verts.Add(s, g.InsertVertex(s));
            }

            g.InsertEdge(verts["u"], verts["v"], "e");
            g.InsertEdge(verts["v"], verts["w"], "f");
            g.InsertEdge(verts["u"], verts["w"], "g");
            g.InsertEdge(verts["w"], verts["z"], "h");
            Console.WriteLine(g.ToString());
        }


        [TestMethod]
        public void Graph_Print_Fig14_8()
        {
            Console.WriteLine(Fig14_8Graph.ToString());
        }

        [TestMethod]
        public void DFSComplete_Print()
        {


            var forest = Graph<string, int>.DFSComplete(Fig14_8Graph);
            foreach (var keyValuePair in forest)
            {
                var edge = keyValuePair.Value;
                Console.WriteLine($"{edge.Endpoints[0].Element} {edge.Endpoints[1].Element}");
            }
        }

        [TestMethod]
        public void ConstructPath_Print()
        {
            var forest = Graph<string, int>.DFSComplete(Fig14_8Graph);
            var origin = FindVertexByName(Fig14_8Graph.Vertices, "BOS");
            var dest = FindVertexByName(Fig14_8Graph.Vertices, "JFK");
            var path = Graph<string, int>.ConstructPath(Fig14_8Graph, origin, dest, forest);
            foreach (Node<Graph<string, int>.Edge<int>> node in path)
            {
                var edge = node.Element;
                Console.WriteLine($"{edge.Endpoints[0].Element} {edge.Endpoints[1].Element}");
            }
        }

        [TestMethod]
        public void BFSComplete_Print()
        {
            var forest = Graph<string, int>.BFSComplete(Fig14_8Graph);
            foreach (var keyValuePair in forest)
            {
                var edge = keyValuePair.Value;
                Console.WriteLine($"{edge.Endpoints[0].Element} {edge.Endpoints[1].Element}");
            }
        }

        [TestMethod]
        public void TransistiveClosure_Print()
        {
            Graph<string, int>.TransitiveClosure(Fig14_11Graph);
            Console.WriteLine(Fig14_11Graph.ToString());
        }

        [TestMethod]
        public void TopologicalSort_Print()
        {
            var result = Graph<string, int>.TopologicalSort(Fig14_13Graph);
            foreach (Node<Graph<string, int>.Vertex<string>> node in result)
            {
                Console.WriteLine(node.Element!.Element);
            }
        }

        [TestMethod]
        public void ShortestPathLengths_Print()
        {
            var src = FindVertexByName(Fig14_15Graph.Vertices, "BWI");
            var result = Graph<string, int?>.ShortestPathLengths(Fig14_15Graph, src);
            foreach (var entry in result.EntrySet())
                Console.WriteLine($"{entry.Key.Element} {entry.Value}");
        }

        [TestMethod]
        public void GetShortestPathTree_Print()
        {
            var src = FindVertexByName(Fig14_15Graph.Vertices, "BWI");
            var cloud = Graph<string, int?>.ShortestPathLengths(Fig14_15Graph, src);
            var shortestPathTree = Graph<string, int?>.GetShortestPathTree(Fig14_15Graph, src, cloud);
            foreach (var entry in shortestPathTree.EntrySet())
            {
                var edge = entry.Value;
                Console.WriteLine($"{edge.Endpoints[0].Element}  {edge.Endpoints[1].Element} {edge.Element}");
            }
        }

        /*
         * Computes a minimum spanning tree of connected, weighted graph g using Kruskal's algorithm.
         * Result is returned as a list of edges that comprise the MST (in arbitrary order).
         */
        [TestMethod]
        public void MST_Test()
        {
            var result = Graph<string, int?>.MST(Fig14_15Graph);

            foreach (Node<Graph<string, int>.Edge<int>> node in result)
            {
                var edge = node.Element;
                Console.WriteLine($"{edge.Endpoints[0].Element}  {edge.Endpoints[1].Element} {edge.Element}");
            }
        }
    }
}