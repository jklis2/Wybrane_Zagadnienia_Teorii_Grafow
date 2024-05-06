using System;
using System.Collections.Generic;

public class LongestPathInTree
{
    static List<int>[] graph;
    static bool[] visited;
    static int farthestNode = 0;
    static int maxDistance = 0;

    public static void Main(string[] args)
    {
        int N = int.Parse(Console.ReadLine());
        graph = new List<int>[N + 1];

        for (int i = 1; i <= N; i++)
        {
            graph[i] = new List<int>();
        }

        for (int i = 1; i < N; i++)
        {
            string[] tokens = Console.ReadLine().Split(' ');
            int u = int.Parse(tokens[0]);
            int v = int.Parse(tokens[1]);

            graph[u].Add(v);
            graph[v].Add(u);
        }

        visited = new bool[N + 1];
        BFS(1);

        visited = new bool[N + 1];
        BFS(farthestNode);

        Console.WriteLine(maxDistance);
    }

    static void BFS(int startNode)
    {
        Queue<(int node, int distance)> queue = new Queue<(int, int)>();
        queue.Enqueue((startNode, 0));
        visited[startNode] = true;

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();
            int currentNode = current.node;
            int currentDistance = current.distance;

            if (currentDistance > maxDistance)
            {
                maxDistance = currentDistance;
                farthestNode = currentNode;
            }

            foreach (int neighbor in graph[currentNode])
            {
                if (!visited[neighbor])
                {
                    visited[neighbor] = true;
                    queue.Enqueue((neighbor, currentDistance + 1));
                }
            }
        }
    }
}
