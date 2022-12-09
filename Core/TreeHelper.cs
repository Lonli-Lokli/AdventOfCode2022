// Copyright (c) 2019 under MIT license.

namespace Core;

public static class TreeHelper
{
    public static IEnumerable<Node> IterateOver(this Node root)
    {
        var nodes = new Stack<Node>(new[] { root });
        while (nodes.Any())
        {
            var node = nodes.Pop();
            yield return node;
            foreach (var n in node.Children)
            {
                nodes.Push(n);
            }
        }
    }
}

public class Node
{
    public string Key { get; set;}
    public int Size { 
        get { return Files.Sum() + Children.Sum(n => n.Size); }
    }
    public IEnumerable<Node> Children { get; set;} = Enumerable.Empty<Node>();
    public IEnumerable<int> Files { get; set;} = Enumerable.Empty<int>();
}