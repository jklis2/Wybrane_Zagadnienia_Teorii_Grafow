using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Foxlings
{
    static int Find(int i, Dictionary<int, int> parent)
    {
        if (parent[i] != i)
            parent[i] = Find(parent[i], parent);
        return parent[i];
    }

    static void Union(int a, int b, Dictionary<int, int> parent, Dictionary<int, int> size)
    {
        int rootA = Find(a, parent);
        int rootB = Find(b, parent);

        if (rootA != rootB)
        {
            if (size[rootA] > size[rootB])
            {
                parent[rootB] = rootA;
                size[rootA] += size[rootB];
            }
            else
            {
                parent[rootA] = rootB;
                size[rootB] += size[rootA];
            }
        }
    }

    static void Main()
    {
        FastIO io = new FastIO();
        
        var input = io.NextLine().Split();
        int n = int.Parse(input[0]);
        int m = int.Parse(input[1]);

        var parent = new Dictionary<int, int>();
        var size = new Dictionary<int, int>();
        int cnt = 0;

        for (int i = 0; i < m; i++)
        {
            var edge = io.NextLine().Split();
            int a = int.Parse(edge[0]);
            int b = int.Parse(edge[1]);

            if (!parent.ContainsKey(a))
            {
                cnt++;
                parent[a] = a;
                size[a] = 1;
            }

            if (!parent.ContainsKey(b))
            {
                cnt++;
                parent[b] = b;
                size[b] = 1;
            }

            Union(a, b, parent, size);
        }

        int c = 0;
        var visited = new HashSet<int>();

        foreach (var p in parent.Keys)
        {
            int root = Find(p, parent);
            if (!visited.Contains(root))
            {
                c++;
                visited.Add(root);
            }
        }

        io.Write(c + (n - cnt));
        io.Flush();
    }
}

class FastIO
{
    private BufferedStream bs;
    private StreamWriter sw;
    private byte[] buffer;
    private int bufferLength, bufferPosition;
    private StringBuilder sb;

    public FastIO()
    {
        bs = new BufferedStream(Console.OpenStandardInput());
        sw = new StreamWriter(Console.OpenStandardOutput());
        buffer = new byte[1 << 16];
        bufferLength = 0;
        bufferPosition = 0;
        sb = new StringBuilder();
    }

    public string NextLine()
    {
        sb.Clear();
        while (true)
        {
            if (bufferPosition == bufferLength)
            {
                bufferLength = bs.Read(buffer, 0, buffer.Length);
                if (bufferLength == 0) break;
                bufferPosition = 0;
            }
            char c = (char)buffer[bufferPosition++];
            if (c == '\n') break;
            sb.Append(c);
        }
        return sb.ToString();
    }

    public void Write(object obj)
    {
        sw.Write(obj);
    }

    public void Flush()
    {
        sw.Flush();
    }
}
