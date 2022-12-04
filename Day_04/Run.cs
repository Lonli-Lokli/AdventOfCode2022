// Copyright (c) 2019 under MIT license.

using System.Runtime.InteropServices;

namespace Day_04;

public static class Run
{
    public static int FirstInput(string input) => input.Split(Environment.NewLine)
        .Select(row => (first:
            (from: Int32.Parse(row.Split(',')[0].Split('-')[0]),
                to: Int32.Parse(row.Split(',')[0].Split('-')[1])),
            second: (from: Int32.Parse(row.Split(',')[1].Split('-')[0]),
                to: Int32.Parse(row.Split(',')[1].Split('-')[1]))))
        .Count(row => (row.first.from <= row.second.from && row.first.to >= row.second.to) ||
                      (row.second.from <= row.first.from && row.second.to >= row.first.to));

    public static int SecondInput(string input) => input.Split(Environment.NewLine)
        .Select(row => (first:
            (from: Int32.Parse(row.Split(',')[0].Split('-')[0]),
                to: Int32.Parse(row.Split(',')[0].Split('-')[1])),
            second: (from: Int32.Parse(row.Split(',')[1].Split('-')[0]),
                to: Int32.Parse(row.Split(',')[1].Split('-')[1]))))
        .Count(row => !(row.first.to < row.second.from || row.first.from > row.second.to));

}