// Copyright (c) 2019 under MIT license.

using Core;

namespace Day_10;

public static class Run
{
    public static int FirstInput(string input)
    {
        var prog = input.Split(Environment.NewLine).Aggregate(
            new { registerX = 1, steps = new Dictionary<int, int>(), stepNo = 1 },
            (globalScope, line) =>
            {
                var command = line.Split(" ");
                switch (command[0])
                {
                    case "noop":
                        return globalScope with
                        {
                            stepNo = globalScope.stepNo + 1,
                            steps = AddOne(globalScope.steps, globalScope.stepNo + 1, globalScope.registerX)
                        };
                    case "addx":
                        var current = new Dictionary<int, int>(globalScope.steps);
                        current[globalScope.stepNo + 1] = globalScope.registerX;
                        current[globalScope.stepNo + 2] = globalScope.registerX + Int32.Parse(command[1]);

                        return new
                        {
                            registerX = current[globalScope.stepNo + 2],
                            steps = current,
                            stepNo = globalScope.stepNo + 2
                        };
                    default: throw new ArgumentException();
                }
            });

        return GetStrength(prog.steps, 20) +  GetStrength(prog.steps, 60) + GetStrength(prog.steps, 100) +
               GetStrength(prog.steps, 140) + GetStrength(prog.steps, 220) + GetStrength(prog.steps, 180);
    }


    public static int GetStrength(Dictionary<int, int> steps, int step) => step * steps[step];
    public static Dictionary<int, int> AddOne(Dictionary<int, int> x, int key, int value)
    {
        var newOne = new Dictionary<int, int>(x);
        newOne[key] = value;
        return newOne;
    }

    public enum Command  { noop, addx };

    public static string SecondInput(string input) => input.Split(Environment.NewLine)
        .Select(row => row.Split(" "))
        .Select(row => new { cmd = Enum.Parse<Command>(row[0]), args = row.Length > 1 ? Int32.Parse(row[1]) : -1 })
        .Aggregate(
            new { crt = new List<string>(Enumerable.Range(0, 240).Select(n => ".")), register = 1, cycle = 0 },
            (acc, exec) => exec.cmd switch
            {
                Command.noop => DrawNoop(acc.crt, acc.register, acc.cycle),
                Command.addx => DrawAddx(exec.args, acc.crt, acc.register, acc.cycle),
                _ => throw new ArgumentOutOfRangeException()
            }).crt.Chunk(40).Select(chunk => string.Join("", chunk))
        .JoinAll();


    private static dynamic DrawNoop(List<string> crt, int register, int cycle) => new
    {
        crt = DrawPixel(crt, register,cycle),
        register = register,
        cycle = cycle + 1
    };

    private static bool IsSpriteVisible(int cycle, int register) => Math.Abs(cycle % 40 - register) <= 1;

    private static List<string> DrawPixel(List<string> crt, int register, int cycle)
    {
        var clone = new List<string>(crt);
        clone[cycle] = IsSpriteVisible(cycle, register) ? "#" : clone[cycle];
        return clone;
    }
    private static dynamic DrawAddx(int argument, List<string> crt, int register, int cycle) => new
    {
        crt = DrawPixel(DrawPixel(crt, register,cycle), register, cycle + 1),
        register = register + argument,
        cycle = cycle + 2
    };
}