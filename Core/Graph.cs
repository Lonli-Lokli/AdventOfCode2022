// Copyright (c) 2019 under MIT license.

using System.Collections;
using System.Diagnostics;

namespace Core;

[DebuggerDisplay("Value: {Value}")]
public record GraphNode<T>(T Value);

public class Graph<T> : IEnumerable<GraphNode<T>>
{
    private readonly List<GraphNode<T>> _nodes;
    private readonly Dictionary<GraphNode<T>, List<GraphNode<T>>> _adjacency = new();

    public Graph(IEnumerable<GraphNode<T>> nodes)
    {
        _nodes = new List<GraphNode<T>>(nodes);
    }
    public void AddEdge(GraphNode<T> from, GraphNode<T> to)
    {
        if (!_adjacency.ContainsKey(from))
        {
            _adjacency[from] = new List<GraphNode<T>>();
        }

        _adjacency[from].Add(to);
    }

    public IEnumerator<GraphNode<T>> GetEnumerator() => _nodes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    public IEnumerable<GraphNode<T>> GetNeighbours(GraphNode<T> node) =>
        _adjacency.ContainsKey(node) ? _adjacency[node].ToList() : Enumerable.Empty<GraphNode<T>>();
}

public static class GraphHelpers
{
    public static IEnumerable<GraphNode<T>> NodesBetween<T>(this Graph<T> graph, GraphNode<T> start, GraphNode<T> end)
    {
        var map = new Dictionary<GraphNode<T>, GraphNode<T>>();
        var queue = new Queue<GraphNode<T>>();
        queue.Enqueue(start);

        while (queue.Any())
        {
            var node  = queue.Dequeue();
            foreach (var neighbour in graph.GetNeighbours(node))
            {
                if (map.ContainsKey(neighbour))
                    continue;

                map[neighbour] = node;
                queue.Enqueue(neighbour);
            }
        }

        var between = new List<GraphNode<T>>();

        var current = end;
        while (!current.Equals(start) && map.ContainsKey(current))
        {
            between.Add(current);
            current = map[current];
        }
        between.Add(start);
        between.Reverse();

        return between;
    }
}