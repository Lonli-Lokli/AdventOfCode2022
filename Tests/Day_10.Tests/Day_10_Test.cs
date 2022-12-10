using FluentAssertions;
using Xunit;

namespace Day_10.Tests;

public class Day10UnitTests
{
    [Fact]
    public void InitialFirstInput_Test()
    {
        Run.FirstInput(@"noop
noop
noop
addx 6
addx -1
addx 5
noop
noop
noop
addx 5
addx -8
addx 9
addx 3
addx 2
addx 4
addx 3
noop
addx 2
noop
addx 1
addx 6
noop
noop
noop
addx -39
noop
addx 5
addx 2
addx -2
addx 3
addx 2
addx 5
addx 2
addx 2
addx 13
addx -12
noop
addx 7
noop
addx 2
addx 3
noop
addx -25
addx 30
addx -10
addx 13
addx -40
noop
addx 5
addx 2
addx 3
noop
addx 2
addx 3
addx -2
addx 3
addx -1
addx 7
noop
noop
addx 5
addx -1
addx 6
noop
noop
noop
noop
addx 9
noop
addx -1
noop
addx -39
addx 2
addx 33
addx -29
addx 1
noop
addx 4
noop
noop
noop
addx 3
addx 2
noop
addx 3
noop
noop
addx 7
addx 2
addx 3
addx -2
noop
addx -30
noop
addx 40
addx -2
addx -38
noop
noop
noop
addx 5
addx 5
addx 2
addx -9
addx 5
addx 7
addx 2
addx 5
addx -18
addx 28
addx -7
addx 2
addx 5
addx -28
addx 34
addx -3
noop
addx 3
addx -38
addx 10
addx -3
addx 29
addx -28
addx 2
noop
noop
noop
addx 5
noop
addx 3
addx 2
addx 7
noop
addx -2
addx 5
addx 2
noop
addx 1
addx 5
noop
noop
addx -25
noop
noop
").Should().Be(14060);
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
        Run.SecondInput(@"noop
noop
noop
addx 6
addx -1
addx 5
noop
noop
noop
addx 5
addx -8
addx 9
addx 3
addx 2
addx 4
addx 3
noop
addx 2
noop
addx 1
addx 6
noop
noop
noop
addx -39
noop
addx 5
addx 2
addx -2
addx 3
addx 2
addx 5
addx 2
addx 2
addx 13
addx -12
noop
addx 7
noop
addx 2
addx 3
noop
addx -25
addx 30
addx -10
addx 13
addx -40
noop
addx 5
addx 2
addx 3
noop
addx 2
addx 3
addx -2
addx 3
addx -1
addx 7
noop
noop
addx 5
addx -1
addx 6
noop
noop
noop
noop
addx 9
noop
addx -1
noop
addx -39
addx 2
addx 33
addx -29
addx 1
noop
addx 4
noop
noop
noop
addx 3
addx 2
noop
addx 3
noop
noop
addx 7
addx 2
addx 3
addx -2
noop
addx -30
noop
addx 40
addx -2
addx -38
noop
noop
noop
addx 5
addx 5
addx 2
addx -9
addx 5
addx 7
addx 2
addx 5
addx -18
addx 28
addx -7
addx 2
addx 5
addx -28
addx 34
addx -3
noop
addx 3
addx -38
addx 10
addx -3
addx 29
addx -28
addx 2
noop
noop
noop
addx 5
noop
addx 3
addx 2
addx 7
noop
addx -2
addx 5
addx 2
noop
addx 1
addx 5
noop
noop
addx -25
noop
noop").Should().Be(@"###...##..###..#..#.####.#..#.####...##.
#..#.#..#.#..#.#.#..#....#.#..#.......#.
#..#.#..#.#..#.##...###..##...###.....#.
###..####.###..#.#..#....#.#..#.......#.
#....#..#.#....#.#..#....#.#..#....#..#.
#....#..#.#....#..#.#....#..#.####..##.."); // exclude new line from test for better readability
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