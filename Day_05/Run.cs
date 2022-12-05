// Copyright (c) 2019 under MIT license.

using Core;

namespace Day_05;

public static class Run
{
    public static string FirstInput(string input) {

      
        var strings = input.Split(@"

");
        var allHeaders = strings[0].Split(Environment.NewLine);
        var init = new Dictionary<int, Stack<string>>();
        int numberOfColumns = (allHeaders[0].Length + 1)/4; // THAT'S DONE AFTER COMPLETION!
        Enumerable.Range(0, numberOfColumns).ToList().ForEach(n => init[n + 1] = new Stack<string>());
        for (var rowId = allHeaders.Length - 1 - 1; rowId >= 0; rowId--)
        {
            for (var columnId = 0; columnId < numberOfColumns; columnId++)
            {
                var chunk = allHeaders[rowId].Substring(columnId * 3 + columnId, 3);
                if (chunk[1] == ' ') continue; // all spaces
                init[columnId + 1].Push(chunk.Substring(1, 1));
            }
        }


        foreach (var row in strings[1].Split(Environment.NewLine))
        {
            var splitted = row.Split(" ");
            var rowAction = (what: Int32.Parse(splitted[1]), from: Int32.Parse(splitted[3]), to: Int32.Parse(splitted[5]));
            for (int i = 0; i < rowAction.what; i++)
            {
                var m = init[rowAction.from].Pop();
                init[rowAction.to].Push(m);
            } 
        }

        return string.Join("", init.Select(pair => pair.Value.Peek()));
    }
    public static string SecondInput(string input, int numberOfColumns)
    {
        var init = new Dictionary<int, Stack<string>>();
        Enumerable.Range(0, numberOfColumns).ToList().ForEach(n => init[n+1] = new Stack<string>());
        var strings = input.Split(@"

");
        
        var allRows = strings[0].Split(Environment.NewLine).Reverse().ToList();
        for (var rowId = 0; rowId < allRows.Count; rowId++)
        {
            for (var columnId = 0; columnId < numberOfColumns; columnId++)
            {
                var chunk = allRows[rowId].Substring(columnId * 3 + columnId, 3);
                if (chunk[1] == ' ') continue; // all spaces
                init[columnId + 1].Push(chunk.Substring(1, 1));
            }
        }

        strings[1].Split(Environment.NewLine).ToList().ForEach(row =>
        {
            var splitted = row.Split(" ");
            var rowAction = (what: Int32.Parse(splitted[1]), from: Int32.Parse(splitted[3]), to: Int32.Parse(splitted[5]));
            var toMove = new List<string>();
            for (int i = 0; i < rowAction.what; i++)
            {
                toMove.Add(init[rowAction.from].Pop());
            }

            toMove.Reverse();
            toMove.ForEach(item => init[rowAction.to].Push(item));
        });

        return string.Join("", init.Select(pair => pair.Value.Peek()));
    }
}