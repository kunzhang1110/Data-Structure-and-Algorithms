using Data_Structure.Graphs;
using System;

namespace Data_Structure.Test
{
    [TestClass]
    public class GraphTests
    {
   
        
        public GraphTests()
        {

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

        private static Graph<string, int> GraphFromEdgelist(string[][] edges, bool directed)
        {
            var g = new Graph<string, int>(directed);

            // first pass to get sorted set of vertex labels
            var labels = new HashSet<string>();
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

        [TestMethod]
        public void Graph_Print_Fig14_8()
        {
            var edges = new string[16][] {
                  new string[2]{"BOS","SFO"}, new string[2]{"BOS","JFK"}, new string[2]{"BOS","MIA"}, new string[2]{"JFK","BOS"},
                  new string[2]{"JFK","DFW"}, new string[2]{"JFK","MIA"}, new string[2]{"JFK","SFO"}, new string[2]{"ORD","DFW"},
                  new string[2]{"ORD","MIA"}, new string[2]{"LAX","ORD"}, new string[2]{"DFW","SFO"}, new string[2]{"DFW","ORD"},
                  new string[2]{"DFW","LAX"}, new string[2]{"MIA","DFW"}, new string[2]{"MIA","LAX"}, new string[2]{"SFO","LAX"},
                };
            var g = GraphFromEdgelist(edges, true);
            Console.WriteLine(g.ToString());
        }

        [TestMethod]
        public void DFS()
        {
            var edges = new string[16][] {
                  new string[2]{"BOS","SFO"}, new string[2]{"BOS","JFK"}, new string[2]{"BOS","MIA"}, new string[2]{"JFK","BOS"},
                  new string[2]{"JFK","DFW"}, new string[2]{"JFK","MIA"}, new string[2]{"JFK","SFO"}, new string[2]{"ORD","DFW"},
                  new string[2]{"ORD","MIA"}, new string[2]{"LAX","ORD"}, new string[2]{"DFW","SFO"}, new string[2]{"DFW","ORD"},
                  new string[2]{"DFW","LAX"}, new string[2]{"MIA","DFW"}, new string[2]{"MIA","LAX"}, new string[2]{"SFO","LAX"},
                };
            var g = GraphFromEdgelist(edges, true);
            var forest = Graph<string, int>.DFSComplete(g);
            foreach(var keyValuePair in forest)
            {
                Console.WriteLine(keyValuePair.Key.Element + " " + keyValuePair.Value.Element);
            }
        }


    }
}