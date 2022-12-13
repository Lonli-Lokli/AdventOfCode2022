// Copyright (c) 2019 under MIT license.

using System.Collections.Immutable;
using Core;

namespace Day_12;

public static class Run
{
    public static object FirstInput(string input)
    {
        var graph = CreateGraph(input);

        var start = graph.Single(n => n.Value == 'S');
        var end = graph.Single(n => n.Value == 'E');
        return graph.NodesBetween(start, end).Count() - 1;
    }

    public static object SecondInput(string input)
    {
        var graph = CreateGraph(input);

        var starts = graph
            .Where(n => n.Value == 'a')
            .Append(graph.Single(n => n.Value == 'S'));
        var end = graph.Single(n => n.Value == 'E');
        return starts.Select(start => graph.NodesBetween(start, end).Count() - 1).Where(length => length > 0).Min();
    }
    
    private static Graph<char> CreateGraph(string input)
    {
        var matrix = input.Split(Environment.NewLine)
            .Select(line => line.ToCharArray().Select(ch => new GraphNode<char>(ch)).ToList())
            .ToList();
        var graph = new Graph<char>(matrix.Aggregate(new List<GraphNode<char>>(),
            (all, part) => all.Concat(part).ToList()));
        for (var rowNo = 0; rowNo < matrix.Count; rowNo++)
        {
            var row = matrix[rowNo];
            for (var columnNo = 0; columnNo < row.Count; columnNo++)
            {
                var from = GetNode(matrix, rowNo, columnNo);
                AddNeighbour(graph, from, GetNode(matrix, rowNo - 1, columnNo)); // up
                AddNeighbour(graph, from, GetNode(matrix, rowNo, columnNo - 1)); // left
                AddNeighbour(graph, from, GetNode(matrix, rowNo + 1, columnNo)); // down
                AddNeighbour(graph, from, GetNode(matrix, rowNo, columnNo + 1)); // right
            }
        }

        return graph;
    }
    public static void AddNeighbour(Graph<char> graph, GraphNode<char> from, GraphNode<char>? to)
    {
        if (to == null) return;
        if (GetElevation(to.Value) - GetElevation(from.Value) > 1) return;
        
        graph.AddEdge(from, to);
    }
    public static char GetElevation(char value) => value switch
    {
        'S' => 'a',
        'E' => 'z',
        _ => value
    };
    public static GraphNode<char>? GetNode(List<List<GraphNode<char>>> nodes, int rowNo, int colNo) =>
        rowNo >= 0 && colNo >= 0 && nodes.Count > rowNo && nodes[rowNo].Count > colNo ? nodes[rowNo][colNo] : null;
    

}