// Copyright (c) 2019 under MIT license.

using System.Text.Json.Nodes;

namespace Day_13;

public static class Run
{
    public static object FirstInput(string input)
    {
        var pairs = input.Split(Environment.NewLine + Environment.NewLine)
            .Select(packet => (
                left: packet.Split(Environment.NewLine)[0],
                right: packet.Split(Environment.NewLine)[1]))
            .ToList();

        var valid = new List<int>();
        for (var index = 0; index < pairs.Count; index++)
        {
            var (left, right) = pairs[index];
            if  (Diff(left, right) < 0) valid.Add(index + 1);
        }

        return valid.Sum();
    } 
    
    public static object SecondInput(string input)
    {
        var dividers = new List<string>() { "[[2]]", "[[6]]" };
        var pairs = input.Split(Environment.NewLine + Environment.NewLine)
            .Select(packet => packet.Split(Environment.NewLine))
            .ToList()
            .Aggregate(new List<string>(dividers), (acc, curr) => acc.Concat(curr).ToList())
            .OrderBy(item => item, Comparer<string>.Create(Diff)).ToList();

        return dividers.Aggregate(1, (result, divider) => result * (pairs.IndexOf(divider) + 1));
    }

    public static int Diff(string left, string right) => Diff(JsonNode.Parse(left), JsonNode.Parse(right));

    public static int Diff(JsonNode left, JsonNode right) => (left is JsonValue && right is JsonValue) switch
    {
        true => left.AsValue().GetValue<int>() - right.AsValue().GetValue<int>(),
        false => Enumerable
            .Zip(JsonAsArray(left), JsonAsArray(right))
            .Select(p => Diff(p.First, p.Second))
            .FirstOrDefault(v => v != 0, JsonAsArray(left).Count - JsonAsArray(right).Count)
    };

    public static JsonArray JsonAsArray(JsonNode node) => node as JsonArray ?? new JsonArray(node.GetValue<int>());
}