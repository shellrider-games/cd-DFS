using System.Xml;

internal class Program
{
    private static void Main(string[] args)
    {
        Graph graph = new Graph(5);
        graph.AddEdge(0, 1);
        graph.AddEdge(0, 2);
        graph.AddEdge(1, 3);
        graph.AddEdge(1, 4);

        string outputPath = Path.Combine(Environment.CurrentDirectory, "graphs/graph0.xml");
        Console.WriteLine(graph);
        WriteGraphToPath(graph, outputPath);
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

        for(int i = 0; i < graph.Nodes; i++){
            xml.WriteStartElement("adjacencyList");
            for(int j = 0; j < graph.AdjacencyLists[i].Count; j++)
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
}