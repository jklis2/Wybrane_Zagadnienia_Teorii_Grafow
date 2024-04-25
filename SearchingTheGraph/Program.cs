using System;
using System.Collections.Generic;

class Graph
{
    private int numVertices;
    private List<int>[] adjacencyList;

    private void DepthFirstSearchRecursive(int vertex, bool[] visited)
    {
        visited[vertex] = true;
        Console.Write($"{vertex + 1} ");
        foreach (int neighbor in adjacencyList[vertex])
        {
            if (!visited[neighbor])
                DepthFirstSearchRecursive(neighbor, visited);
        }
    }

    public Graph(int numVertices)
    {
        this.numVertices = numVertices;
        adjacencyList = new List<int>[numVertices];
        for (int i = 0; i < numVertices; i++)
            adjacencyList[i] = new List<int>();
    }

    public void AddEdge(int startVertex, int endVertex)
    {
        adjacencyList[startVertex].Add(endVertex);
    }

    public void DepthFirstSearch(int startVertex)
    {
        bool[] visited = new bool[numVertices];
        DepthFirstSearchRecursive(startVertex, visited);
        Console.WriteLine();
    }

    public void BreadthFirstSearch(int startVertex)
    {
        bool[] visited = new bool[numVertices];
        Queue<int> queue = new Queue<int>();
        visited[startVertex] = true;
        queue.Enqueue(startVertex);

        while (queue.Count > 0)
        {
            int current = queue.Dequeue();
            Console.Write($"{current + 1} ");

            foreach (int neighbor in adjacencyList[current])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        int numTestCases, numVertices, vertexIndex, numEdges, targetVertex, operation;
        int caseCount = 0;
        numTestCases = int.Parse(Console.ReadLine());
        while (numTestCases-- > 0)
        {
            numVertices = int.Parse(Console.ReadLine());
            Graph graph = new Graph(numVertices);
            for (int i = 0; i < numVertices; i++)
            {
                string[] inputs = Console.ReadLine().Split();
                vertexIndex = int.Parse(inputs[0]);
                numEdges = int.Parse(inputs[1]);
                for (int j = 0; j < numEdges; j++)
                {
                    int connectedVertex = int.Parse(inputs[j + 2]);
                    graph.AddEdge(vertexIndex - 1, connectedVertex - 1);
                }
            }
            Console.WriteLine($"graph {++caseCount}");
            while (true)
            {
                string[] query = Console.ReadLine().Split();
                targetVertex = int.Parse(query[0]);
                operation = int.Parse(query[1]);
                if (targetVertex == 0 && operation == 0)
                    break;
                if (operation == 0)
                    graph.DepthFirstSearch(targetVertex - 1);
                else
                    graph.BreadthFirstSearch(targetVertex - 1);
            }
        }
    }
}
