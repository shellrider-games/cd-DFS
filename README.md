# DFS

This is a console application written in C# that reads a graph from an XML file, performs a depth-first traversal starting from a specified node, and writes the traversal order to an output file.
Requirements

    .NET Framework 6
    An XML file containing a graph in the following format:
```xml

<graph>
  <nodes>...</nodes>
  <adjacencyLists>
    <adjacencyList>
      <edge>...</edge>
      ...
    </adjacencyList>
    ...
  </adjacencyLists>
</graph>
```

## Usage

To run the program, open a command prompt or terminal and navigate to the directory containing the executable file.

> dotnet run [input file] [start node]

Where [input file] is the path to the XML file containing the graph and [start node] is an optional parameter specifying the index of the node to start the traversal from. If no start node is specified, the traversal will start from node 0.

The program will output the traversal order to a file with the same name as the input file, but with ".out" appended to the end.
## License

This program is licensed under the BSD 3-Clause License. See the LICENSE file for details.