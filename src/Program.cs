using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        string? inputPath = null;
        if(args.Length > 0) {
            inputPath = Path.GetFullPath(args[0]);
        }

        uint startNode = 0;
        if(args.Length > 1) {
            startNode = uint.Parse(args[1]);
        }

        if(!File.Exists(inputPath)) {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Could not find file: {inputPath}, please make sure the file exists and can be read by the program.");
            Console.ForegroundColor = previousColor;
            return;
        }

        Graph? graph = inputPath is not null?ReadGraphFromFile(inputPath):null;
        if(graph is not null) { 
            List<uint> graphTraversal = graph.DFS(startNode);
            WriteOutputFile(graphTraversal, inputPath + ".out");
        } else {
            var previousColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Error.WriteLine($"Error while parsing graph. Please make sure the file format of the given graph is correct.");
            Console.ForegroundColor = previousColor;
        }
    }

    private static void WriteOutputFile(List<uint> traversalOrder, string path)
    {
        using(StreamWriter writer = File.CreateText(path))
        {
            for(int i = 0; i < traversalOrder.Count; i++)
            {
                writer.Write(traversalOrder[i]);
                if(i < traversalOrder.Count -1) {
                    writer.Write(",");
                }
            }
            writer.Close();
        }
    }

    //This method writes a given graph to an XML file at the given path.
    private static void WriteGraphToPath(Graph graph, string path)
    {
        //Create a new file stream and XML writer for the given path.
        FileStream fs = File.Create(path);
        XmlWriter xml = XmlWriter.Create(fs,
            new XmlWriterSettings { Indent = true }
        );

        //Start writing the XML document.
        xml.WriteStartDocument();

        //Write the root element for the graph.
        xml.WriteStartElement("graph");
        //Write the number of nodes in the graph as a child element.
        xml.WriteElementString("nodes", graph.Nodes.ToString());
        //Write the adjacency lists for each node as a child element.
        xml.WriteStartElement("adjacencyLists");


        //Loop over each node in the graph.
        for (int i = 0; i < graph.Nodes; i++)
        {
            //Start writing the adjacency list for the current node
            xml.WriteStartElement("adjacencyList");
            //Loop over each adjacent node for the current node.
            for (int j = 0; j < graph.AdjacencyLists[i].Count; j++)
            {
                //Write the index of the adjacent node as a child element.
                xml.WriteElementString("edge", graph.AdjacencyLists[i][j].ToString());
            }
            //Finish writing the adjacency list for the current node.
            xml.WriteEndElement();
        }

        //Finish writing the adjacency lists for all nodes
        xml.WriteEndElement();
        //Finish writing the root element for the graph.
        xml.WriteEndElement();

        //Close the XML writer and file stream.
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