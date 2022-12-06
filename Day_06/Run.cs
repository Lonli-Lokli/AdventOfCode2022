// Copyright (c) 2019 under MIT license.

namespace Day_06;

public static class Run
{
    public static int FirstInput(string input)
    {
        var end = 4;
        do
        {
            if (input.Substring(end- 4, 4).ToCharArray().Distinct().Count() == 4) return end;
            end++;
        } while (end < input.Length);

        return -1;
    }

    public static int FirstInputFunctional(string input) => Enumerable.Range(4, input.Length)
        .Select(end => new { train = input.Substring(end - 4, 4).ToCharArray().Distinct(), end = end })
        .First(data => data.train.Count() == 4).end;
    
    
    public static int SecondInput(string input) {
        
        var end = 14;
        do
        {
            if (input.Substring(end- 14, 14).ToCharArray().Distinct().Count() == 14) return end;
            end++;
        } while (end < input.Length);

        return -1;
    }
}