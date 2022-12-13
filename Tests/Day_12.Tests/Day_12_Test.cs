using FluentAssertions;
using Xunit;

namespace Day_12.Tests;

public class UnitTest1
{
     [Fact]
    public void InitialFirstInput_Test()
    {
        Run.FirstInput(@"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi").Should().Be(31);
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
        Run.SecondInput(@"Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi").Should().Be(29);
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