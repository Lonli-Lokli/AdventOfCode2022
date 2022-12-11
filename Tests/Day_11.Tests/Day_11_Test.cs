using System;
using FluentAssertions;
using Xunit;

namespace Day_11.Tests;

public class UnitTest1
{
    [Fact]
    public void InitialFirstInput_Test()
    {
        Run.FirstInput(@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1").Should().Be(10605);
    }
    
    //[Theory]
    // [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    // [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 6)]
    // [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    //  [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    //  public void First_Input_Test(string input, int res)
    // {
    //     Run.FirstInput(input).Should().Be(res);
    //     
    // }
    
    // ------------------   SECOND INPUT    ----------------------------------------- //
    [Fact]
    public void InitialSecondInput_Test()
    {
        Run.SecondInput(@"Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1").Should().Be(2713310158);
    }
    
    // [Theory]
    // [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    // [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    // [InlineData("nppdvjthqldpwncqszvftbrmjlhg", 23)]
    // [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    // [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    // public void InitialDataOriginal2_Test(string input, int res)
    // {
    //     Run.SecondInput(input).Should().Be(res);
    // }
}