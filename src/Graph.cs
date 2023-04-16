public class Graph
{
    private uint _nodes; //number of nodes in the graph
    private List<uint>[] _adjacencyLists; //array of all adjacency lists

    public uint Nodes
    {
        get { return _nodes; }
        private set { _nodes = value; }
    }

    public List<uint>[] AdjacencyLists
    {
        get { return _adjacencyLists; }
        private set { _adjacencyLists = value; }
    }

    public Graph(uint nodes)
    {
        _nodes = nodes;
        _adjacencyLists = new List<uint>[nodes];
        for (int i = 0; i < nodes; i++)
        {
            _adjacencyLists[i] = new List<uint>();
        }
    }

    public void AddEdge(uint u, uint v)
    {
        if (!_adjacencyLists[u].Contains(v)) _adjacencyLists[u].Add(v);
        if (!_adjacencyLists[v].Contains(u)) _adjacencyLists[v].Add(u);
    }


    //Created this method for easy debugging
    public override string ToString()
    {
        string returnString = $"Graph{System.Environment.NewLine}Nodes: {_nodes}{System.Environment.NewLine}";
        for (int i = 0; i < _nodes; i++)
        {
            returnString += $"Node{i}: [";
            for (int j = 0; j < _adjacencyLists[i].Count; j++)
            {
                returnString += _adjacencyLists[i][j];
                if (j < _adjacencyLists[i].Count - 1) returnString += ",";
            }
            returnString += $"]" + System.Environment.NewLine;
        }
        return returnString;
    }

    //Graphs are equal even if the adjacency lists are stored in a diffent order
    public bool Equals(Graph other)
    {
        if (other.Nodes != _nodes) return false;
        for (int i = 0; i < _nodes; i++)
        {
            if (!(other.AdjacencyLists[i].Count == _adjacencyLists[i].Count)) return false;
            for (int j = 0; j < _adjacencyLists[i].Count; j++)
            {
                if (!other.AdjacencyLists[i].Contains(_adjacencyLists[i][j])) return false;
            }
        }
        return true;
    }

    //Starts a DFS at a specified node and returns the visited nodes in Order as a List<uint>
    public List<uint> DFS(uint startNode)
    {
        List<uint> visitOrder = new List<uint>();
        // Create a boolean array to keep track of visited nodes
        bool[] visited = new bool[_nodes];

        // Create a stack for DFS
        Stack<uint> stack = new Stack<uint>();

        // Mark the start node as visited and push it to the stack
        visited[startNode] = true;
        stack.Push(startNode);

        // Perform DFS
        while (stack.Count != 0)
        {
            // Pop a vertex from the stack and add to visitOrder
            uint currentNode = stack.Pop();
            visitOrder.Add(currentNode);

            // Get all adjacent vertices of the popped vertex
            foreach (uint neighbor in _adjacencyLists[currentNode])
            {
                // If the adjacent vertex is not visited, mark it as visited and push it to the stack
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    stack.Push(neighbor);
                }
            }
        }

        return visitOrder;
    }

}