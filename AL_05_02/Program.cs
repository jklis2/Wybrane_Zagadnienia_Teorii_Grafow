using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        int t = int.Parse(Console.ReadLine());
        for (int i = 0; i < t; i++)
        {
            string graphType = Console.ReadLine().Trim();
            int n = int.Parse(Console.ReadLine());
            List<string> edges = new List<string>();
            
            for (int j = 0; j < n; j++)
            {
                string edge = Console.ReadLine();
                edges.Add(edge);
            }

            switch (graphType)
            {
                case "g":
                    Console.WriteLine("graph {");
                    foreach (var edge in edges)
                    {
                        var nodes = edge.Split();
                        Console.WriteLine($"{nodes[0]} -- {nodes[1]};");
                    }
                    Console.WriteLine("}");
                    break;
                case "d":
                    Console.WriteLine("digraph {");
                    foreach (var edge in edges)
                    {
                        var nodes = edge.Split();
                        Console.WriteLine($"{nodes[0]} -> {nodes[1]};");
                    }
                    Console.WriteLine("}");
                    break;
                case "gw":
                    Console.WriteLine("graph {");
                    foreach (var edge in edges)
                    {
                        var parts = edge.Split();
                        Console.WriteLine($"{parts[0]} -- {parts[1]} [label = {parts[2]}];");
                    }
                    Console.WriteLine("}");
                    break;
                case "dw":
                    Console.WriteLine("digraph {");
                    foreach (var edge in edges)
                    {
                        var parts = edge.Split();
                        Console.WriteLine($"{parts[0]} -> {parts[1]} [label = {parts[2]}];");
                    }
                    Console.WriteLine("}");
                    break;
            }
        }
    }
}
