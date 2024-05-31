using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class FriendCircle
{
    private Dictionary<string, string> parent;
    private Dictionary<string, int> rank;
    private Dictionary<string, int> size;

    public FriendCircle()
    {
        parent = new Dictionary<string, string>();
        rank = new Dictionary<string, int>();
        size = new Dictionary<string, int>();
    }

    private string Find(string person)
    {
        if (parent[person] != person)
        {
            parent[person] = Find(parent[person]);
        }
        return parent[person];
    }

    public int Union(string person1, string person2)
    {
        if (!parent.ContainsKey(person1))
        {
            parent[person1] = person1;
            rank[person1] = 0;
            size[person1] = 1;
        }
        if (!parent.ContainsKey(person2))
        {
            parent[person2] = person2;
            rank[person2] = 0;
            size[person2] = 1;
        }

        string root1 = Find(person1);
        string root2 = Find(person2);

        if (root1 != root2)
        {
            if (rank[root1] > rank[root2])
            {
                parent[root2] = root1;
                size[root1] += size[root2];
                return size[root1];
            }
            else if (rank[root1] < rank[root2])
            {
                parent[root1] = root2;
                size[root2] += size[root1];
                return size[root2];
            }
            else
            {
                parent[root2] = root1;
                size[root1] += size[root2];
                rank[root1]++;
                return size[root1];
            }
        }
        return size[root1];
    }
}

public class Program
{
    public static void Main()
    {
        using var reader = new StreamReader(Console.OpenStandardInput());
        using var writer = new StreamWriter(Console.OpenStandardOutput());
        int T = int.Parse(reader.ReadLine());

        for (int t = 0; t < T; t++)
        {
            int N = int.Parse(reader.ReadLine());
            FriendCircle friendCircle = new FriendCircle();

            StringBuilder output = new StringBuilder();

            for (int n = 0; n < N; n++)
            {
                string[] friends = reader.ReadLine().Split();
                string person1 = friends[0];
                string person2 = friends[1];

                int circleSize = friendCircle.Union(person1, person2);
                output.AppendLine(circleSize.ToString());
            }

            writer.Write(output.ToString());
        }
    }
}
