using System;
using System.Diagnostics;
using AVLTree;
using QuickGraph;
using QuickGraph.Graphviz;
using QuickGraph.Graphviz.Dot;

namespace AVLTreeTests
{
    public class OutputTree<T> where T : IComparable<T>
    {
        AdjacencyGraph<string, Edge<string>> graph = new AdjacencyGraph<string, Edge<string>>();

        public void AddEdges(TreeNode<T> node)
        {
            graph.AddVertex(node.Value.ToString());

            if (node.Parent != null)
            {
                graph.AddEdge(new Edge<string>(node.Parent.Value.ToString(), node.Value.ToString()));
            }

            if (node.Left != null)
            {
                AddEdges(node.Left);
            }

            if (node.Right != null)
            {
                AddEdges(node.Right);
            }
        }

        public void BuildGraph(TreeNode<T> root, string fileName)
        {
            AddEdges(root);

            var graphViz = new GraphvizAlgorithm<string, Edge<string>>(graph, @".", GraphvizImageType.Gif);
            graphViz.FormatVertex += FormatVertex;
            graphViz.FormatEdge += FormatEdge;
            fileName += ".dot";
            graphViz.Generate(new FileDotEngine(), fileName);
        }


        private static void FormatVertex(object sender, FormatVertexEventArgs<string> e)
        {
            e.VertexFormatter.Label = e.Vertex;
            e.VertexFormatter.Shape = GraphvizVertexShape.Circle;

            e.VertexFormatter.BottomLabel = e.Vertex;

            e.VertexFormatter.StrokeColor = GraphvizColor.Black;
            e.VertexFormatter.Font = new GraphvizFont("Calibri", 11);
        }

        private static void FormatEdge(object sender, FormatEdgeEventArgs<string, Edge<string>> e)
        {
            e.EdgeFormatter.Head.Label = e.Edge.Target;
            e.EdgeFormatter.Tail.Label = e.Edge.Source;
            e.EdgeFormatter.Font = new GraphvizFont("Calibri", 8);
            e.EdgeFormatter.FontGraphvizColor = GraphvizColor.Black;
            e.EdgeFormatter.StrokeGraphvizColor = GraphvizColor.Black;
        }

        public sealed class FileDotEngine : IDotEngine
        {
            public string Run(GraphvizImageType imageType, string dot, string outputFileName)
            {
                string output = outputFileName;
                System.IO.File.WriteAllText(output, dot);

                string newOutputFileName = outputFileName.Replace(".dot", "");

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = @"C:\Program Files (x86)\Graphviz2.38\bin\dot.exe";
                startInfo.Arguments = @"dot -Tgif " + newOutputFileName + ".dot -o " + newOutputFileName + ".png";

                Process.Start(startInfo);
                return output;
            }
        }

    }
}
