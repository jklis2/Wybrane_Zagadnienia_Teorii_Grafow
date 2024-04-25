using System;
using System.Collections.Generic;
using System.Linq;

class Graph
{
    private int Nodes;
    private List<int>[] AdjList;
    private bool IsDirected;
    private int[] discoveryTime;
    private int[] low;
    private bool[] visited;
    private bool[] articulationVertex;
    private int time;

    public Graph(int nodes, bool isDirected)
    {
        Nodes = nodes;
        IsDirected = isDirected;
        AdjList = new List<int>[nodes + 1];
        for (int i = 1; i <= nodes; i++)
            AdjList[i] = new List<int>();
        discoveryTime = new int[nodes + 1];
        low = new int[nodes + 1];
        visited = new bool[nodes + 1];
        articulationVertex = new bool[nodes + 1];
    }

    public void AddEdge(int from, int to, int weight)
    {
        AdjList[from].Add(to);
        if (!IsDirected) AdjList[to].Add(from);
    }

    public void PrintGraph()
    {
        Console.WriteLine(IsDirected ? 1 : 0);
        int edgesCount = AdjList.Sum(x => x.Count / (IsDirected ? 1 : 2));
        Console.WriteLine($"{Nodes} {edgesCount}");
        for (int i = 1; i <= Nodes; i++)
            foreach (var edge in AdjList[i].Where(e => IsDirected || e >= i))
                Console.WriteLine($"{i} {edge} 1");  
    }

    public void BreadthFirstSearch(int start)
    {
        var visited = new bool[Nodes + 1];
        var parent = new int[Nodes + 1];
        var queue = new Queue<int>();
        visited[start] = true;
        queue.Enqueue(start);

        Console.WriteLine("\nBFS:");
        while (queue.Count > 0)
        {
            int node = queue.Dequeue();
            Console.Write($"{node} ");
            foreach (var neighbor in AdjList[node])
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                    parent[neighbor] = node;
                }
        }
        Console.WriteLine("\n\nBFS Paths:");
        for (int i = 1; i <= Nodes; i++)
        {
            if (visited[i])
            {
                Stack<int> path = new Stack<int>();
                for (int current = i; current != 0; current = parent[current])
                    path.Push(current);

                Console.WriteLine(string.Join(" ", path));
            }
        }
    }

    public void DepthFirstSearch(int start)
    {
        var visited = new bool[Nodes + 1];
        var parent = new int[Nodes + 1];
        var stack = new Stack<int>();
        stack.Push(start);

        Console.WriteLine("\nDFS:");
        while (stack.Count > 0)
        {
            int node = stack.Pop();
            if (!visited[node])
            {
                visited[node] = true;
                Console.Write($"{node} ");
                foreach (var neighbor in AdjList[node].Where(e => !visited[e]))
                {
                    stack.Push(neighbor);
                    parent[neighbor] = node;
                }
            }
        }
        Console.WriteLine("\n\nDFS Paths:");
        for (int i = 1; i <= Nodes; i++)
        {
            if (visited[i])
            {
                Stack<int> path = new Stack<int>();
                for (int current = i; current != 0; current = parent[current])
                    path.Push(current);

                Console.WriteLine(string.Join(" ", path));
            }
        }
    }

    public void FindArticulationPoints()
    {
        for (int i = 1; i <= Nodes; i++)
            if (!visited[i])
                DepthFirstSearch(i, -1);

        Console.WriteLine("\nArticulation Vertices:");
        for (int i = 1; i <= Nodes; i++)
            if (articulationVertex[i])
                Console.Write(i + " ");
        Console.WriteLine();
    }

    public void FindConnectedComponents()
    {
        var componentVisited = new bool[Nodes + 1];
        List<List<int>> components = new List<List<int>>();
        Console.WriteLine("\nConnected Components:");

        for (int i = 1; i <= Nodes; i++)
        {
            if (!componentVisited[i])
            {
                List<int> component = new List<int>();
                DepthFirstSearchForComponent(i, component, componentVisited);
                component.Sort();  
                components.Add(component);
                Console.WriteLine($"C{components.Count}: {string.Join(" ", component)}");
            }
        }
    }

    private void DepthFirstSearchForComponent(int start, List<int> component, bool[] visited)
    {
        Stack<int> stack = new Stack<int>();
        stack.Push(start);
        visited[start] = true;

        while (stack.Count > 0)
        {
            int node = stack.Pop();
            component.Add(node);
            foreach (var neighbor in AdjList[node])
            {
                if (!visited[neighbor])
                {
                    stack.Push(neighbor);
                    visited[neighbor] = true;
                }
            }
        }
    }

    private void DepthFirstSearch(int u, int parent)
    {
        visited[u] = true;
        discoveryTime[u] = low[u] = ++time;
        int childCount = 0;

        foreach (var v in AdjList[u])
        {
            if (!visited[v])
            {
                childCount++;
                DepthFirstSearch(v, u);
                low[u] = Math.Min(low[u], low[v]);

                if (parent != -1 && low[v] >= discoveryTime[u])
                    articulationVertex[u] = true;
            }
            else if (v != parent)
            {
                low[u] = Math.Min(low[u], discoveryTime[v]);
            }
        }

        if (parent == -1 && childCount > 1)
            articulationVertex[u] = true;
    }
}

class Program
{
    static void Main()
    {
        int d = int.Parse(Console.ReadLine());
        var line = Console.ReadLine().Split().Select(int.Parse).ToArray();
        int n = line[0], e = line[1];

        Graph graph = new Graph(n, d == 1);
        for (int i = 0; i < e; i++)
        {
            var edge = Console.ReadLine().Split().Select(int.Parse).ToArray();
            graph.AddEdge(edge[0], edge[1], edge[2]);
        }

        graph.PrintGraph();
        graph.BreadthFirstSearch(1);
        graph.DepthFirstSearch(1);
        graph.FindConnectedComponents();
        graph.FindArticulationPoints();
    }
}
