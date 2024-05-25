using System;
using System.Collections.Generic;

class Program
{
    public static void DisplayGrid(char[,] grid)
    {
        int rows = grid.GetLength(0);
        int columns = grid.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                Console.Write(grid[i, j]);
            }
            Console.WriteLine();
        }
    }

    static List<(int, int)> FindShortestPath(char[,] grid, (int, int) start, (int, int) goal)
    {
        (int, int)[] directions = new (int, int)[]
        {
            (-1, 0),
            (1, 0),
            (0, -1),
            (0, 1)
        };

        int rows = grid.GetLength(0);
        int columns = grid.GetLength(1);

        Queue<((int, int), List<(int, int)>)> queue = new();
        HashSet<(int, int)> visited = new();

        queue.Enqueue((start, new List<(int, int)>() { start }));
        visited.Add(start);

        while (queue.Count > 0)
        {
            var (currentPosition, currentPath) = queue.Dequeue();
            if (currentPosition == goal) return currentPath;

            foreach (var direction in directions)
            {
                int newRow = currentPosition.Item1 + direction.Item1;
                int newColumn = currentPosition.Item2 + direction.Item2;

                if (newRow >= 0 && newRow < rows && newColumn >= 0 && newColumn < columns)
                {
                    var neighbor = (newRow, newColumn);

                    if (grid[newRow, newColumn] == '.' && !visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        List<(int, int)> newPath = new(currentPath);
                        newPath.Add(neighbor);
                        queue.Enqueue((neighbor, newPath));
                    }
                }
            }
        }
        return null;
    }

    static void Main()
    {
        string[] map = new string[]
        {
            "..###.....#####.....#####",
            "..###.....#####.....#####",
            "..###.....#####.....#####",
            "...........###......#####",
            "......###...........#####",
            "...#..###..###......#####",
            "###############.#######..",
            "###############.#######.#",
            "###############.#######..",
            "................########.",
            "................#######..",
            "...#######..##.......##.#",
            "...#######..##.......##..",
            "...##...........########.",
            "#####...........#######..",
            "#####...##..##..#...#...#",
            "#####...##..##....#...#.."
        };

        char[,] grid = new char[map.Length, map[0].Length];
        for (int row = 0; row < map.Length; row++)
        {
            for (int column = 0; column < map[row].Length; column++)
            {
                grid[row, column] = map[row][column];
            }
        }

        List<(int, int)> path = FindShortestPath(grid, (0, 0), (grid.GetLength(0) - 1, grid.GetLength(1) - 1));

        if (path != null) {
            foreach (var point in path)
            {
                grid[point.Item1, point.Item2] = 'o';
            }

            DisplayGrid(grid);
        }
        else
        {
            Console.WriteLine("No path.");
        }
    }
}
