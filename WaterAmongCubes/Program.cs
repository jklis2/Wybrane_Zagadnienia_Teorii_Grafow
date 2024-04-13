using System;
using System.Collections.Generic;

class Program
{
    static int[] dx = { -1, 1, 0, 0 };
    static int[] dy = { 0, 0, -1, 1 };

    static void Main()
    {
        int t = int.Parse(Console.ReadLine().Trim());
        for (int k = 0; k < t; k++)
        {
            if (k > 0) Console.ReadLine(); 
            string[] dimensions = Console.ReadLine().Split();
            int n = int.Parse(dimensions[0]);
            int m = int.Parse(dimensions[1]);

            int[,] heights = new int[n, m];
            for (int i = 0; i < n; i++)
            {
                string[] row = Console.ReadLine().Split();
                for (int j = 0; j < m; j++)
                {
                    heights[i, j] = int.Parse(row[j]);
                }
            }

            Console.WriteLine(CalculateMaxWaterVolume(heights, n, m));
        }
    }

    static int CalculateMaxWaterVolume(int[,] heights, int n, int m)
    {
        bool[,] visited = new bool[n, m];
        SortedSet<(int height, int x, int y)> pq = new SortedSet<(int height, int x, int y)>();

        for (int i = 0; i < n; i++)
        {
            pq.Add((heights[i, 0], i, 0));
            pq.Add((heights[i, m - 1], i, m - 1));
            visited[i, 0] = true;
            visited[i, m - 1] = true;
        }

        for (int j = 1; j < m - 1; j++)
        {
            pq.Add((heights[0, j], 0, j));
            pq.Add((heights[n - 1, j], n - 1, j));
            visited[0, j] = true;
            visited[n - 1, j] = true;
        }

        int waterTrapped = 0;

        while (pq.Count > 0)
        {
            var current = pq.Min;
            pq.Remove(current);

            for (int i = 0; i < 4; i++)
            {
                int nx = current.x + dx[i];
                int ny = current.y + dy[i];

                if (nx >= 0 && nx < n && ny >= 0 && ny < m && !visited[nx, ny])
                {
                    visited[nx, ny] = true;
                    int newHeight = Math.Max(current.height, heights[nx, ny]);
                    waterTrapped += Math.Max(0, current.height - heights[nx, ny]);
                    pq.Add((newHeight, nx, ny));
                }
            }
        }

        return waterTrapped;
    }
}
