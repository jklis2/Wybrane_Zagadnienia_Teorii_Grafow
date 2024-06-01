using System;
using System.Collections.Generic;

class Edge : IComparable<Edge>
{
    public int s, t, w;

    public Edge(int S, int T, int W)
    {
        s = S;
        t = T;
        w = W;
    }

    public int CompareTo(Edge other)
    {
        return w.CompareTo(other.w);
    }
}

class Program
{
    static int[] id;
    static int[] size;

    static int Find(int p)
    {
        while (p != id[p])
        {
            id[p] = id[id[p]];
            p = id[p];
        }
        return p;
    }

    static void Union(int p, int q)
    {
        int rootP = Find(p);
        int rootQ = Find(q);
        if (rootP == rootQ) return;

        if (size[rootP] > size[rootQ])
        {
            id[rootQ] = rootP;
            size[rootP] += size[rootQ];
        }
        else
        {
            id[rootP] = rootQ;
            size[rootQ] += size[rootP];
        }
    }

    static void Main(string[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int i = 0; i < T; i++)
        {
            Console.ReadLine();

            int N = int.Parse(Console.ReadLine());

            id = new int[N];
            size = new int[N];
            for (int j = 0; j < N; j++)
            {
                id[j] = j;
                size[j] = 1;
            }

            List<Edge> edges = new List<Edge>();
            for (int j = 0; j < N - 1; j++)
            {
                string[] parts = Console.ReadLine().Split();
                int a = int.Parse(parts[0]) - 1;
                int b = int.Parse(parts[1]) - 1;
                int w = int.Parse(parts[2]);
                edges.Add(new Edge(a, b, w));
            }

            edges.Sort();

            long sum = 0;
            foreach (Edge e in edges)
            {
                int s = e.s;
                int t = e.t;
                int rootS = Find(s);
                int rootT = Find(t);

                if (rootS != rootT)
                {
                    sum += ((long)size[rootS] * size[rootT] - 1) * (e.w + 1) + e.w;
                    Union(rootS, rootT);
                }
            }

            Console.WriteLine(sum);
        }
    }
}
