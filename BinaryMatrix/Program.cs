using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        string[] dimensions = Console.ReadLine().Split();
        int m = int.Parse(dimensions[0]);
        int n = int.Parse(dimensions[1]);

        int[] rowSums = Console.ReadLine().Split().Select(int.Parse).ToArray();

        int[] colSums = Console.ReadLine().Split().Select(int.Parse).ToArray();

        long totalRowSum = rowSums.Sum();
        long totalColSum = colSums.Sum();

        if (totalRowSum != totalColSum)
        {
            Console.WriteLine("NIE");
            return;
        }

        int source = m + n;
        int sink = m + n + 1;
        int verticesCount = m + n + 2;

        int[,] capacity = new int[verticesCount, verticesCount];

        for (int i = 0; i < m; i++)
        {
            capacity[source, i] = rowSums[i];
        }

        for (int j = 0; j < n; j++)
        {
            capacity[m + j, sink] = colSums[j];
        }

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                capacity[i, m + j] = 1;
            }
        }

        int maxFlow = MaxFlow(capacity, source, sink);

        if (maxFlow == totalRowSum)
        {
            Console.WriteLine("TAK");
        }
        else
        {
            Console.WriteLine("NIE");
        }
    }

    static int MaxFlow(int[,] capacity, int source, int sink)
    {
        int n = capacity.GetLength(0);
        int[,] flow = new int[n, n];
        int[] parent = new int[n];
        int maxFlow = 0;

        while (BFS(capacity, flow, source, sink, parent))
        {
            int pathFlow = int.MaxValue;
            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                pathFlow = Math.Min(pathFlow, capacity[u, v] - flow[u, v]);
            }

            for (int v = sink; v != source; v = parent[v])
            {
                int u = parent[v];
                flow[u, v] += pathFlow;
                flow[v, u] -= pathFlow;
            }

            maxFlow += pathFlow;
        }

        return maxFlow;
    }

    static bool BFS(int[,] capacity, int[,] flow, int source, int sink, int[] parent)
    {
        int n = capacity.GetLength(0);
        bool[] visited = new bool[n];
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(source);
        visited[source] = true;
        parent[source] = -1;

        while (queue.Count > 0)
        {
            int u = queue.Dequeue();

            for (int v = 0; v < n; v++)
            {
                if (!visited[v] && capacity[u, v] > flow[u, v])
                {
                    parent[v] = u;
                    visited[v] = true;
                    queue.Enqueue(v);

                    if (v == sink)
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
