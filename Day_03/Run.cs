// Copyright (c) 2019 under MIT license.

namespace Day_03;
using static Core.Helpers;

public static class Run
{
    public static (string left, string right) SplitIntoCommpartment(string data) => (
        left: data.Substring(0, data.Length / 2 ), right: data.Substring(data.Length / 2));

    public static char? GetInterselection((string left, string right) pair) =>
        pair.left.Intersect(pair.right).Any() ? pair.left.Intersect(pair.right).First() : null;

    public static int GetItemWeight(char? input) => input == null ? 0 :
        Char.IsLower(input.GetValueOrDefault()) ? (input.GetValueOrDefault() - 'a' + 1) :
        (26 + GetItemWeight(Char.ToLower(input.GetValueOrDefault())));

    public static int CalculateDuplicatesWeight(string input) => input.Pipe(
            input => input.Split(Environment.NewLine))
        .Select(line => line.Pipe(SplitIntoCommpartment).Pipe(GetInterselection).Pipe(GetItemWeight))
        .Pipe(items => items.Sum());

    public static char GetGroupBadge(IEnumerable<string> group) => IntersectAll(group)[0];

    public static int CalculateGroupBadgesWeight(string input) => input.Pipe(
            input => input.Split(Environment.NewLine).Chunk(3))
        .Select(group => group.Pipe(GetGroupBadge).Pipe(c => GetItemWeight(c)))
        .Pipe(items => items.Sum());


}