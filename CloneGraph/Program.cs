// namespace CloneGraph;
// using System;
// using System.Collections.Generic;

// public class Node {
//     public int val;
//     public IList<Node> neighbors;

//     public Node() {
//         val = 0;
//         neighbors = new List<Node>();
//     }

//     public Node(int _val) {
//         val = _val;
//         neighbors = new List<Node>();
//     }

//     public Node(int _val, List<Node> _neighbors) {
//         val = _val;
//         neighbors = _neighbors;
//     }
// }

public class Solution {
    private Node CloneHelper(Node node, Dictionary<Node, Node> visited) {
        if (visited.ContainsKey(node))
            return visited[node];

        Node clonedNode = new Node(node.val);
        visited.Add(node, clonedNode);

        foreach (var neighbor in node.neighbors)
            clonedNode.neighbors.Add(CloneHelper(neighbor, visited));

        return clonedNode;
    }
    
    public Node CloneGraph(Node node) {
        if (node == null) return null;

        Dictionary<Node, Node> visited = new Dictionary<Node, Node>();
        return CloneHelper(node, visited);
    }
}

// public class Program {
//     public static void Main(string[] args) {
//         Node node1 = new Node(1);
//         Node node2 = new Node(2);
//         Node node3 = new Node(3);
//         Node node4 = new Node(4);

//         node1.neighbors = new List<Node> { node2, node4 };
//         node2.neighbors = new List<Node> { node1, node3 };
//         node3.neighbors = new List<Node> { node2, node4 };
//         node4.neighbors = new List<Node> { node1, node3 };

//         Solution solution = new Solution();
//         Node clonedGraph = solution.CloneGraph(node1);

//         Console.WriteLine("Klonowanie zakończone!");
//     }
// }
