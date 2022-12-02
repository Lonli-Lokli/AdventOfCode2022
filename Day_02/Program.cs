// See https://aka.ms/new-console-template for more information

using Day_02;
using static Day_02.Run;

Console.WriteLine(FollowStrategyResult(Input.Data));
Console.WriteLine(FollowEndResult(Input.Data));
Console.WriteLine(test(Input.Data.Split(Environment.NewLine)));

static int test(string[] input)
{
    var res = 0;
    foreach (var str in input)
    {
        var parts = str.Split(" ");
        var first = parts[0][0] - 'A';
        var diff = parts[1][0] - 'Y';
        var second = (first + diff + 3) % 3;
        res += (second + 1) + ((diff + 1) % 3) * 3;
    }

    return res;
}




