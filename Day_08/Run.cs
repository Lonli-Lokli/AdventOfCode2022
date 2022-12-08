// Copyright (c) 2019 under MIT license.

using System.Data;

namespace Day_08;

public class Run
{
    public static int FirstInput(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var columnNo = lines[0].Length;
        return Enumerable.Range(1, columnNo - 2).Aggregate(0, (total, curentColumnNo) =>
        {
            return total + Enumerable.Range(1, lines.Length - 2).Aggregate(0, (rowTotal, currentRowNo) =>
            {
                if (Enumerable.Range(0, curentColumnNo)
                        .All(toCheck =>
                            lines[currentRowNo][curentColumnNo] > lines[currentRowNo][toCheck] ||
                            curentColumnNo == toCheck) ||
                    Enumerable.Range(curentColumnNo + 1, columnNo - curentColumnNo - 1)
                        .All(toCheck => lines[currentRowNo][curentColumnNo] > lines[currentRowNo][toCheck]) ||
                    Enumerable.Range(0, currentRowNo)
                        .All(toCheck =>
                            lines[currentRowNo][curentColumnNo] > lines[toCheck][curentColumnNo] ||
                            currentRowNo == toCheck) ||
                    Enumerable.Range(currentRowNo + 1, lines.Length - currentRowNo - 1)
                        .All(toCheck => lines[currentRowNo][curentColumnNo] > lines[toCheck][curentColumnNo]))
                {
                    rowTotal += 1;
                }

                return rowTotal;
            });
        }) + columnNo * 2 + (lines.Length - 2) * 2;
    }

    public static int SecondInput(string input)
    {
        var lines = input.Split(Environment.NewLine);
        var columnCount = lines[0].Length;
    
        Func<int, int, int> limiter = (count, max) => max == count ? count : count + 1;
        return Enumerable.Range(1, columnCount - 2).Aggregate(0, (total, currentColumnNo) => Math.Max(total,
            Enumerable.Range(1, lines.Length - 2).Aggregate(0, (rowTotal, currentRowNo) => Math.Max(rowTotal,
                /*up*/ limiter(
                    Enumerable.Range(0, currentRowNo).Reverse().TakeWhile(row =>
                        lines[currentRowNo][currentColumnNo] > lines[row][currentColumnNo]).Count(),
                    currentRowNo) *
                /*down*/ limiter(
                    Enumerable.Range(currentRowNo + 1, lines.Length - currentRowNo - 1).TakeWhile(row =>
                        lines[currentRowNo][currentColumnNo] > lines[row][currentColumnNo]).Count(),
                    lines.Length - currentRowNo - 1) *
                /*left*/ limiter(
                    Enumerable.Range(0, currentColumnNo).Reverse().TakeWhile(col =>
                        lines[currentRowNo][currentColumnNo] > lines[currentRowNo][col]).Count(),
                    currentColumnNo) *
                /*right*/ limiter(
                    Enumerable.Range(currentColumnNo + 1, columnCount - currentColumnNo - 1)
                        .TakeWhile(col => lines[currentRowNo][currentColumnNo] > lines[currentRowNo][col]).Count(),
                    columnCount - currentColumnNo - 1)))));
    }
}