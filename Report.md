# Final Project Report
Course: C# Development SS 2023 (4 ECTS, 3 SWS)

Student ID: cc211007

BCC Group: B

Name: Georg Becker

Your Project Topic: Depth First Search (DFS)

#

### A Short Summary about the Algorithm (What is the Background and the Motivation of having such an algorithm?):

DFS stands for “Depth-First Search”. It is a graph traversal algorithm that explores the graph as far as possible before backtracking. DFS can find cycles and bridges in graphs, find strongly connected components in directed graphs, solve puzzles and mazes and has many more applications. It could also be used for pathfinding, although there exist better algorithms for that use case.

DFS has a time complexity of O(V+E) where V is the number of nodes and E is the number of edges in the graph. In the worst case, DFS must visit every vertex and every edge in the graph (for example if the graph is a List).

### Implementation Detail

#### Implementation Logic Explanation:
I implemented DFS with help of a Graph data structure using adjacency lists.
This allows me to use a stack to keep track of the nodes to visit and a boolean array of length n where n = number of nodes to keep track of the nodes I already visited. To begin I put the start node in the stack. Then in every iteration I pop the node from the stack and set it to visited in the boolean array and put it into my visiting order list.
Then I put all neighbours that have not been visited onto the stack. Then I go to the next iteration until the stack is empty.
At the end I return the list of the nodes in order they have been visited.

#### Achievements:
I do not think this project includes any special achievements other than I could implement it pretty fast. 

### Learned Knowledge from the Project

#### Major Challenges and Solutions:
1. Reading the xml data
For me the biggest challenge of the project was to read in my xml data and parse it to a graph. I could not use the serializer since I do not have a 0 argument constructor in my Graph class, so I figured out a data schema myself. Reading in that xml file felt arcane to me. In future projects I will probably go back to .json files, which I understand better.

#### Minor Challenges and Solutions:


### Reflections on the Own Project:

Next time I would focus on creating a proper usecase rather than I/O parsing

### Reflections on the Projects learned during the Presentation:

I learned about algorithms to find circles in graphs other than DFS.