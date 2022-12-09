// Copyright (c) 2019 under MIT license.

using System.Drawing;

namespace Day_09;

public static class Run
{
    public static int FirstInput(string input) =>
        input.Split(Environment.NewLine).Select(row =>
                new
                {
                    direction = Enum.Parse<Direction>(row.Split(" ")[0].ToString()),
                    steps = Int32.Parse(row.Split(" ")[1].ToString())
                })
            .Aggregate(new { points = new List<Point>(), head = new Point(0, 0), tail = new Point(0, 0) },
                (acc, movement) =>
                {
                    var newHead = acc.head;
                    var newTail = acc.tail;

                    Enumerable.Range(0, movement.steps).ToList().ForEach(_ =>
                    {
                        newHead = MoveHead(newHead, movement.direction);
                        newTail = MoveTail(newHead, newTail);
                        acc.points.Add(newTail);
                    });
                    return acc with { head = newHead, tail = newTail };
                }).points.Distinct().Count();

    public enum Direction { R, U, L, D }

    public static Point MoveHead(Point point, Direction direction) =>
        direction switch
        {
            Direction.R => point with { X = point.X + 1 },
            Direction.U => point with { Y = point.Y + 1 },
            Direction.L => point with { X = point.X - 1 },
            Direction.D => point with { Y = point.Y - 1 },
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };

    public static Point MoveTail(Point newHead, Point oldTail) =>
        AreTouching(newHead, oldTail) ? oldTail : GetClosestPoint(newHead, oldTail);

    public static bool AreTouching(Point head, Point tail) =>
        Math.Abs(head.X - tail.X) <= 1 && Math.Abs(head.Y - tail.Y) <= 1;


    public static Point GetClosestPoint(Point head, Point tail) =>
        (Math.Abs(head.X - tail.X) == 2 && head.Y == tail.Y)
            ? tail with { X = head.X > tail.X ? tail.X + 1 : tail.X - 1 } // move straight
            : Math.Abs(head.Y - tail.Y) == 2 && head.X == tail.X
                ? tail with { Y = head.Y > tail.Y ? tail.Y + 1 : tail.Y - 1 } // move straight
                : head.X > tail.X
                    ? new Point(tail.X + 1, head.Y > tail.Y ? tail.Y + 1 : tail.Y - 1) // move diagonal
                    : new Point(tail.X - 1, head.Y > tail.Y ? tail.Y + 1 : tail.Y - 1); // move diagonal

    public static List<Point> CreateRope(int noOfKnots) =>
        new(Enumerable.Range(0, noOfKnots).Select(x => new Point(0, 0)));

    public static int SecondInput(string input) =>
        input.Split(Environment.NewLine).Select(row =>
                new
                {
                    direction = Enum.Parse<Direction>(row.Split(" ")[0].ToString()),
                    steps = Int32.Parse(row.Split(" ")[1].ToString())
                })
            .Aggregate(new { points = new List<Point>(), rope = CreateRope(10) },
                (globalScope, movement) => globalScope with
                {
                    rope = Enumerable.Range(0, movement.steps).Aggregate(globalScope.rope,
                        (newRope, _) => newRope.Aggregate(new List<Point>(), (movingRope, knot) =>
                        {
                            if (!movingRope.Any()) // move head
                            {
                                movingRope.Add(MoveHead(knot, movement.direction));
                            }
                            else // move tail
                            {
                                movingRope.Add(MoveTail(movingRope.Last(), knot));
                                if (movingRope.Count == 10) // last knot moved
                                {
                                    globalScope.points.Add(movingRope.Last());
                                }
                            }

                            return movingRope;
                        }))
                }).points.Distinct().Count();
}