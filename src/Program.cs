using System.Xml;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        Graph graph1 = new Graph(5);
        graph1.AddEdge(0, 1);
        graph1.AddEdge(0, 2);
        graph1.AddEdge(1, 3);
        graph1.AddEdge(1, 4);

        string outputPath = Path.Combine(Environment.CurrentDirectory, $"graphs{Path.PathSeparator}graph0.xml");
        Console.WriteLine(graph1);

        WriteGraphToPath(graph1, outputPath);
        Graph? readGraph = ReadGraphFromFile(outputPath);

        Debug.Assert(readGraph.Equals(graph1));
        Console.WriteLine(readGraph);
    }

    private static void WriteGraphToPath(Graph graph, string path)
    {
        FileStream fs = File.Create(path);
        XmlWriter xml = XmlWriter.Create(fs,
            new XmlWriterSettings { Indent = true }
        );
        xml.WriteStartDocument();

        xml.WriteStartElement("graph");
        xml.WriteElementString("nodes", graph.Nodes.ToString());
        xml.WriteStartElement("adjacencyLists");

        for (int i = 0; i < graph.Nodes; i++)
        {
            xml.WriteStartElement("adjacencyList");
            for (int j = 0; j < graph.AdjacencyLists[i].Count; j++)
            {
                xml.WriteElementString("edge", graph.AdjacencyLists[i][j].ToString());
            }
            xml.WriteEndElement();
        }

        xml.WriteEndElement();
        xml.WriteEndElement();

        xml.Close();
        fs.Close();
    }

    //This method reads a graph from a file at the given path and returns it.
    //If any errors occur during the parsing of the file, null is returned
    private static Graph? ReadGraphFromFile(string path)
    {
        Graph? returnGraph = null;

        //Create an XmlDocument and load the file at the given path.
        var file = new XmlDocument();
        file.Load(path);

        //Select the "nodes" element from the XML file and parse it as an unsigned integer.
        //If the "nodes" element doesn't exist or can't be parsed as an unsigned integer, return null.
        var nodesElement = file.SelectSingleNode("/graph/nodes");
        if (nodesElement == null || !uint.TryParse(nodesElement.InnerText, out uint nodes)) return null;

        //Create a new graph with the parsed number of nodes.
        returnGraph = new Graph(nodes);

        //Select all the "adjacencyList" nodes from the XML file.
        //If the number of "adjacencyList" nodes doesn't match the number of nodes in the graph, return null.
        var adjacencyListNodes = file.SelectNodes("/graph/adjacencyLists/adjacencyList");
        if (adjacencyListNodes == null || adjacencyListNodes.Count != nodes) return null;

        //Loop over each node in the graph.
        for (int i = 0; i < nodes; i++)
        {
            //Select the "adjacencyList" node for the current node.
            var adjacencyListNode = adjacencyListNodes[i];
            var edgeNodes = adjacencyListNode?.SelectNodes("edge");

            if (edgeNodes is not null)
            {
                foreach (XmlNode edgeNode in edgeNodes)
                {
                    //Parse the text of the "edge" node as an unsigned integer.
                    //If the text can't be parsed as an unsigned integer, return null.
                    if (uint.TryParse(edgeNode.InnerText, out uint adjacentNodeIndex))
                    {
                        //Add an edge from the current node to the adjacent node with the parsed index.
                        returnGraph.AddEdge((uint)i, adjacentNodeIndex);
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        return returnGraph;
    }

}