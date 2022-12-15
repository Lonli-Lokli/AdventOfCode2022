// Copyright (c) 2019 under MIT license.

using Core;
using OneOf;
using OneOf.Types;

namespace Day_14;

public static class Run
{
    public static object FirstInput(string input)
    {
        var cave = new Cave(input, new Address(500, 0), false);
        while (!cave.IsFull)
        {
            cave.AddSand();
        }

        return cave.SandItems;
    }

    public static object SecondInput(string input)
    {
        var cave = new Cave(input, new Address(500, 0), true);
        while (!cave.IsFull)
        {
            cave.AddSand();
        }

        return cave.SandItems;
    }
}

public record Address(int X, int Y)
{
    public Address GetDown() => new Address(X, Y + 1);
    public Address GetDownLeft() => new Address(X - 1, Y + 1);
    public Address GetDownRight() => new Address(X + 1, Y + 1);
};
public record Rock(int X, int Y);
public record Sand();

public class Cave
{
    private readonly int _maxRows;
    private readonly Address _entryPoint;
    private readonly Dictionary<Address, OneOf<Rock, Sand, None>> _items;
    public int SandItems => _items.Values.Count(v => v.IsT1);
    public bool IsFull { get; private set; }

    public Cave(string input, Address entryPoint, bool hasFloor)
    {
        _entryPoint = entryPoint;
        var rocks = BuildRocks(input);
        var allRocks = hasFloor
            ? rocks.Concat(Enumerable.Range(0, 1000).Select(n => new Rock(n, rocks.Max(r => r.Y) + 2))).ToHashSet()
            : rocks;
        var minX = allRocks.Min(r => r.X);
        var maxX = allRocks.Max(r => r.X);
        var maxY = allRocks.Max(r => r.Y);

     
        Func<int, int, OneOf<Rock, Sand, None>> addItem = (x, y) =>
            allRocks.Any(rock => rock.X == x && rock.Y == y)
                ? allRocks.Single(rock => rock.X == x && rock.Y == y)
                : new None();
        _items = Enumerable.Range(minX, maxX - minX + 1).Select(xCoord => Enumerable.Range(0, maxY + 1)
                .Select(yCoord => (coord: (x: xCoord, y: yCoord), item: addItem(xCoord, yCoord))))
            .Aggregate(new Dictionary<Address, OneOf<Rock, Sand, None>>(),
                (allItems, currentItem) =>
                    allItems.Concat(currentItem
                            .Select(pair =>
                                new KeyValuePair<Address, OneOf<Rock, Sand, None>>(new Address(pair.coord.x, pair.coord.y),
                                    pair.item))
                            .ToDictionary(k => k.Key, p => p.Value))
                        .ToDictionary(k => k.Key, p => p.Value));
        _maxRows = _items.Keys.Max(k => k.Y);
    }

    public void AddSand()
    {
        if (IsFull) return;
        OccupyAddress(_entryPoint);

    }
    
    private static HashSet<Rock> BuildRocks(string input) =>
        input.Split(Environment.NewLine)
            .Select(line => line.Split(" -> ").Select(coord => (
                from: Int32.Parse(coord.Split(",")[0]),
                to: Int32.Parse(coord.Split(",")[1]))))
            .Aggregate(new HashSet<Rock>(), (rocks, lineCoords) =>
                rocks.Concat(lineCoords.Zip(lineCoords.Skip(1), BuildSequence)
                        .Aggregate(new HashSet<Rock>(), (lineRocks, seq) => lineRocks.Concat(seq).ToHashSet()))
                    .ToHashSet());
    
    private static IEnumerable<Rock> BuildSequence((int from, int to) x, (int from, int to) y) =>
        x.from == y.from
            ? Enumerable.Range(Math.Min(x.to, y.to), Math.Abs(y.to - x.to) + 1).Select(p => new Rock(x.from, p))
            : Enumerable.Range(Math.Min(x.from, y.from), Math.Abs(y.from - x.from) + 1).Select(p => new Rock(p, x.to));
    
    private void OccupyAddress(Address address)
    {
        var search = address;
        var added = false;
        while (!added && !IsFull && CanOccupyAddress(_entryPoint))
        {
            if (CanOccupyAddress(search.GetDown()))
            {
                search = search.GetDown();
            } else if (CanOccupyAddress(search.GetDownLeft()))
            {
                search = search.GetDownLeft();
            } else if (CanOccupyAddress(search.GetDownRight()))
            {
                search = search.GetDownRight();
            }
            else
            {
                _items[search] = OneOf<Rock, Sand, None>.FromT1(new Sand());
                added = true;
            }
            if (search.Y > _maxRows) IsFull = true;
        }

        if (!added) IsFull = true;
    }

    private bool IsAddressExists(Address point) => _items.ContainsKey(point);
    private bool CanOccupyAddress(Address address) => !IsAddressExists(address) || _items[address].IsT2;

    public string PrintCave() =>
        _items
            .OrderBy(pair => pair, Comparer<KeyValuePair<Address, OneOf<Rock, Sand, None>>>.Create((x, y) =>
                Comparer<int>.Default.Compare(x.Key.Y * 10 + x.Key.X, y.Key.Y * 10 + y.Key.X)))
            .Chunk(_items.Keys.Max(x => x.X) - _items.Keys.Min(x => x.X) + 1)
            .Select(pair =>
                pair.First().Key.Y.ToString("D3") + " |" + pair.Select(p => p.Value.Match(rock => " # ", sand => " o ", none => " . "))
                    .JoinAll(" | "))
            .Prepend("     " + Enumerable.Range(_items.Keys.Min(i => i.X), _items.Keys.Max(i => i.X) - _items.Keys.Min(i => i.X) + 1)
                .Select(idx => idx.ToString("D3")).JoinAll(" | "))
            .JoinAll(Environment.NewLine);
}


