public class Graph 
{
    private uint _nodes; //number of nodes in the graph
    private List<uint>[] _adjacencyLists; //array of all adjacency lists

    public uint Nodes {
        get { return _nodes; }
        private set { _nodes = value; }
    }

    public List<uint>[] AdjacencyLists {
        get { return _adjacencyLists; }
        private set { _adjacencyLists = value; }
    }

    public Graph(uint nodes) {
        _nodes = nodes;
        _adjacencyLists = new List<uint>[nodes];
        for(int i = 0; i < nodes; i++) {
            _adjacencyLists[i] = new List<uint>();
        }
    }

    public void AddEdge(uint u, uint v)
    {
        if(!_adjacencyLists[u].Contains(v)) _adjacencyLists[u].Add(v);
        if(!_adjacencyLists[v].Contains(u)) _adjacencyLists[v].Add(u);
    }
}