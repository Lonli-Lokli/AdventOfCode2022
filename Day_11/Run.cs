// Copyright (c) 2019 under MIT license.

using Core;

namespace Day_11;

public static class Run
{
    public static long FirstInput(string input) =>
        Enumerable.Range(1, 20).Aggregate(input.Split(Environment.NewLine).Chunk(7).Select(ParseAsMonkey).ToList(),
                (globalMonkeys, roundNo) => globalMonkeys.Aggregate(globalMonkeys.ToList(), (copyAll, monkeyFrom) =>
                    copyAll[monkeyFrom.monkeyId].levels.ToList().Aggregate(copyAll, (acc, processingLevel) =>
                    {
                        var worry = Math.DivRem(ExecuteOperation(monkeyFrom.operation, processingLevel), 3).Quotient;
                        var monkeyTo = worry % monkeyFrom.divisibleBy == 0
                            ? acc[monkeyFrom.truthyMonkeyId]
                            : acc[monkeyFrom.falsyMonkeyId];

                        acc[monkeyTo.monkeyId] = acc[monkeyTo.monkeyId] with
                        {
                            levels = acc[monkeyTo.monkeyId].levels.Append(worry).ToList()
                        };
                        acc[monkeyFrom.monkeyId] = acc[monkeyFrom.monkeyId] with
                        {
                            levels = acc[monkeyFrom.monkeyId].levels.RemoveFirst(processingLevel).ToList(),
                            inspectionTimes = acc[monkeyFrom.monkeyId].inspectionTimes + 1
                        };

                        return acc;
                    })))
            .OrderByDescending(m => m.inspectionTimes)
            .Take(2).Aggregate(1L, (acc, curr) => acc * curr.inspectionTimes);
    
    public static Monkey ParseAsMonkey(string[] chunk) => new(
        Int32.Parse(chunk[0].Substring("Monkey ".Length, 1)),
        chunk[1].Substring("  Starting items: ".Length).Split(", ").Select(long.Parse).ToList(),
        chunk[2].Substring("  Operation:  new =".Length),
        Int32.Parse(chunk[3].Substring("  Test: divisible by ".Length)),
        Int32.Parse(chunk[4].Substring("    If true: throw to monkey ".Length)),
        Int32.Parse(chunk[5].Substring("    If false: throw to monkey ".Length)), 0L);
    
   

    public record Monkey(
        int monkeyId, List<long> levels, string operation, int divisibleBy, int truthyMonkeyId,
        int falsyMonkeyId, long inspectionTimes);
    
    public static long ExecuteOperation(string operation, long level)
    {
        checked
        {
            return operation switch
            {
                "old + 2" => level + 2,
                "old + 3" => level + 3,
                "old * 3" => level * 3,
                "old + 4" => level + 4,
                "old + 5" => level + 5,
                "old + 6" => level + 6,
                "old + 8" => level + 8,
                "old * 11" => level * 11,
                "old * 19" => level * 19,
                "old * old" => level * level,
                _ => throw new ArgumentOutOfRangeException(nameof(operation), $"Value {operation} should be handled")
            };
        }
    }

    public static long SecondInput(string input) => 
        Enumerable.Range(1, 10_000).Aggregate(input.Split(Environment.NewLine).Chunk(7).Select(ParseAsMonkey).ToList(),
                (globalMonkeys, roundNo) => globalMonkeys.Aggregate(globalMonkeys.ToList(), (copyAll, monkeyFrom) =>
                    copyAll[monkeyFrom.monkeyId].levels.ToList().Aggregate(copyAll, (acc, processingLevel) =>
                    {
                        var worry = ExecuteOperation(monkeyFrom.operation, processingLevel) % globalMonkeys
                            .Select(gm => gm.divisibleBy).Aggregate(1, (acc, agg) => acc * agg);
                        var monkeyTo = worry % monkeyFrom.divisibleBy == 0
                            ? acc[monkeyFrom.truthyMonkeyId]
                            : acc[monkeyFrom.falsyMonkeyId];

                        acc[monkeyTo.monkeyId] = acc[monkeyTo.monkeyId] with
                        {
                            levels = acc[monkeyTo.monkeyId].levels.Append(worry).ToList()
                        };
                        acc[monkeyFrom.monkeyId] = acc[monkeyFrom.monkeyId] with
                        {
                            levels = acc[monkeyFrom.monkeyId].levels.RemoveFirst(processingLevel).ToList(),
                            inspectionTimes = acc[monkeyFrom.monkeyId].inspectionTimes + 1
                        };

                        return acc;
                    })))
            .OrderByDescending(m => m.inspectionTimes)
            .Take(2).Aggregate(1L, (acc, curr) => acc * curr.inspectionTimes);


    public static BigMonkey ParseAsBigMonkey(string[] chunk) => new(
        new Queue<long>(chunk[1].Substring("  Starting items: ".Length).Split(", ").Select(long.Parse)),
        chunk[2].Substring("  Operation:  new =".Length),
        Int32.Parse(chunk[3].Substring("  Test: divisible by ".Length)),
        Int32.Parse(chunk[4].Substring("    If true: throw to monkey ".Length)),
        Int32.Parse(chunk[5].Substring("    If false: throw to monkey ".Length)), 0L);

    public static object SecondInputQueue(string input) => Enumerable.Range(1, 10_000).Aggregate(
            input.Split(Environment.NewLine).Chunk(7).Select(ParseAsBigMonkey).ToList(),
            (roundMonkeys, _) => roundMonkeys.Aggregate(roundMonkeys, (monkeys, monkey) =>
            {
                while (monkey.levels.Any())
                {
                    monkey.inspectionTimes++;
                    var worryLevel = monkey.levels.Dequeue();
                    worryLevel = ExecuteOperation(monkey.operation, worryLevel);
                    worryLevel %= roundMonkeys.Select(gm => gm.divisibleBy).Aggregate(1, (acc, agg) => acc * agg);
                    monkeys[worryLevel % monkey.divisibleBy == 0 ? monkey.truthyMonkeyId : monkey.falsyMonkeyId].levels
                        .Enqueue(worryLevel);
                }

                return monkeys;
            })).OrderByDescending(m => m.inspectionTimes)
        .Take(2).Aggregate(1L, (acc, curr) => acc * curr.inspectionTimes);
    
    public class BigMonkey
    {
        public BigMonkey(Queue<long> levels, string operation, int divisibleBy, int truthyMonkeyId,
            int falsyMonkeyId, long inspectionTimes)
        {
            this.levels = levels;
            this.operation = operation;
            this.divisibleBy = divisibleBy;
            this.truthyMonkeyId = truthyMonkeyId;
            this.falsyMonkeyId = falsyMonkeyId;
            this.inspectionTimes = inspectionTimes;
        }

        public Queue<long> levels;
        public string operation;
        public int divisibleBy;
        public int truthyMonkeyId;
        public int falsyMonkeyId;
        public long inspectionTimes;
    }

}