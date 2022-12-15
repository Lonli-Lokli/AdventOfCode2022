// Copyright (c) 2019 under MIT license.

using System.ComponentModel.DataAnnotations;

namespace Day_15;

public class Run
{
    public record Point(int X, int Y)
    {
        public Point((int X, int Y) parseAddress) : this(parseAddress.X, parseAddress.Y)
        {
        }

        public int GetDistance(Point other) => Math.Abs(X - other.X) + Math.Abs(Y - other.Y);
    }

    public record Sensor(Point Address);
    public record Beacon(Point Address);

    public record SensorBeacon(Sensor sensor, Beacon beacon)
    {
        public int Radius = sensor.Address.GetDistance(beacon.Address);

        public int GetDistanceTo(Point address) => sensor.Address.GetDistance(address);
        public bool In(Point address) => GetDistanceTo(address) <= Radius;
    }

    public record Area
    {
        public int Left { get; }
        public int Right { get; }
    
        public int Top { get; }
        public int Bottom { get; }
        public int Width { get; }
        public int Height { get; }
        
        public IEnumerable<Point> Edges { get; }
        public Area(int x, int y, int width, int height)
        {
            Left = x;
            Right = x + width - 1;
            Top = y;
            Bottom = y + height - 1;
            Width = width;
            Height = height;
            Edges = new Point[]
            {
                new(Left, Top),
                new(Right, Top),
                new(Right, Bottom),
                new(Left, Bottom),
            };
        }
        public IEnumerable<Area> Split() {
            var leftWidth = Width / 2;
            var rightWidth = Width - leftWidth;
            var upHeight = Height / 2;
            var downHeight = Height - upHeight;

            return new[]
            {
                new Area(Left, Top, leftWidth, upHeight),
                new Area(Left + leftWidth, Top, rightWidth, upHeight),
                new Area(Left, Top + upHeight, leftWidth, downHeight),
                new Area(Left + leftWidth, Top + upHeight, rightWidth, downHeight)
            };
        }
    }

    public static List<SensorBeacon> Parse(string input) =>
        input.Split(Environment.NewLine).Select(ParseRow).ToList();

    // Sensor at x=2, y=18: closest beacon is at x=-2, y=15
    public static SensorBeacon ParseRow(string input) =>
        new(ParseSensor(input.Split(": ")[0]), ParseBeacon(input.Split(": ")[1]));
    
    // Sensor at x=2, y=18
    public static Sensor ParseSensor(string input) =>
        new(new Point(ParseAddress(input.Substring("Sensor at ".Length))));
    
    // closest beacon is at x=-2, y=15
    public static Beacon ParseBeacon(string input) =>
        new(new Point(ParseAddress(input.Substring("closest beacon is at ".Length))));
    
    // x=2, y=18
    public static (int X, int Y) ParseAddress(string input) => (
        X: ParseCoordinate(input.Split(", ")[0]),
        Y: ParseCoordinate(input.Split(", ")[1]));
    
    // x=2
    public static int ParseCoordinate(string input) => Int32.Parse(input.Substring(2));

    public static bool CannotBeBeacon(
        Point toCheck, List<SensorBeacon> map, HashSet<Point> occupiedAddresses) =>
        !occupiedAddresses.Contains(toCheck) && map.Any(pair => CannotBeBeacon(toCheck, pair));

    public static bool CannotBeBeacon(Point toCheck, SensorBeacon pair) =>
        pair.GetDistanceTo(toCheck) <= pair.Radius;
    
    private static IEnumerable<Area> GetDistressedBeaconAreas(List<SensorBeacon> pairs, Area area)
    {
        if (area.Width == 0 || area.Height == 0) {
            yield break;
        }

        foreach (var pair in pairs)
        {
            if (area.Edges.All(edge => pair.In(edge)))
            {
                yield break;
            }
        }
        
        if (area.Width == 1 && area.Height == 1) {
            yield return area;
        }
        
        foreach (var splittedArea in area.Split()) {
            foreach (var subArea in GetDistressedBeaconAreas(pairs, splittedArea)) {
                yield return subArea;
            }
        }
    }
    
    public static object FirstInput(string input, int rowToCheck)
    {
        var assessment = Parse(input);
        var addAddresses = assessment
            .Select(a => a.sensor).Select(s => s.Address)
            .Concat(assessment.Select(a => a.beacon).Select(b => b.Address))
            .ToHashSet();
        var minX = addAddresses.Min(p => p.X);
        var maxX = addAddresses.Max(p => p.X);

        return Enumerable.Range(minX * 5, maxX * 5)
            .Count(x => CannotBeBeacon(new Point(x, rowToCheck), assessment, addAddresses));
    }

    public static object SecondInput(string input, int maxRange)
    {
        var asessment = Parse(input);
        var address = GetDistressedBeaconAreas(asessment, new Area(0, 0, maxRange + 1, maxRange + 1)).First();
        return address.Left * 4_000_000L + address.Top;
    }
}