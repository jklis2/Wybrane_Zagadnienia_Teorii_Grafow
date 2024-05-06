using System;
using System.Collections.Generic;
using System.IO;

class UnionFind
{
    private Dictionary<string, string> parent;
    private Dictionary<string, int> rank;

    public UnionFind()
    {
        parent = new Dictionary<string, string>();
        rank = new Dictionary<string, int>();
    }

    public string Find(string x)
    {
        if (!parent.ContainsKey(x))
        {
            parent[x] = x;
            rank[x] = 0;
        }
        if (parent[x] != x)
        {
            parent[x] = Find(parent[x]);
        }
        return parent[x];
    }

    public void Union(string x, string y)
    {
        string rootX = Find(x);
        string rootY = Find(y);

        if (rootX == rootY) return;

        if (rank[rootX] > rank[rootY])
        {
            parent[rootY] = rootX;
        }
        else if (rank[rootX] < rank[rootY])
        {
            parent[rootX] = rootY;
        }
        else
        {
            parent[rootY] = rootX;
            rank[rootX]++;
        }
    }

    public bool Connected(string x, string y)
    {
        return Find(x) == Find(y);
    }
}

class Program
{
    static void Main(string[] args)
    {
        using (StreamReader reader = new StreamReader(Console.OpenStandardInput()))
        using (StreamWriter writer = new StreamWriter(Console.OpenStandardOutput()))
        {
            writer.AutoFlush = false;
            UnionFind uf = new UnionFind();
            string line;

            while ((line = reader.ReadLine()) != null && line != "")
            {
                string[] parts = line.Split();
                string command = parts[0];
                string ip1 = parts[1];
                string ip2 = parts[2];

                if (command == "B")
                {
                    uf.Union(ip1, ip2);
                }
                else if (command == "T")
                {
                    bool isConnected = uf.Connected(ip1, ip2);
                    writer.WriteLine(isConnected ? "T" : "N");
                }
            }
            writer.Flush();
        }
    }
}
