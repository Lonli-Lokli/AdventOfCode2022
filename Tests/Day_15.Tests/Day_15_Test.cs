using FluentAssertions;
using Xunit;

namespace Day_15.Tests;

public class UnitTest1
{
    [Fact]
    public void InitialFirstInput_Test()
    {
        Run.FirstInput(@"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3", 10).Should().Be(26);
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
        Run.SecondInput(@"Sensor at x=2, y=18: closest beacon is at x=-2, y=15
Sensor at x=9, y=16: closest beacon is at x=10, y=16
Sensor at x=13, y=2: closest beacon is at x=15, y=3
Sensor at x=12, y=14: closest beacon is at x=10, y=16
Sensor at x=10, y=20: closest beacon is at x=10, y=16
Sensor at x=14, y=17: closest beacon is at x=10, y=16
Sensor at x=8, y=7: closest beacon is at x=2, y=10
Sensor at x=2, y=0: closest beacon is at x=2, y=10
Sensor at x=0, y=11: closest beacon is at x=2, y=10
Sensor at x=20, y=14: closest beacon is at x=25, y=17
Sensor at x=17, y=20: closest beacon is at x=21, y=22
Sensor at x=16, y=7: closest beacon is at x=15, y=3
Sensor at x=14, y=3: closest beacon is at x=15, y=3
Sensor at x=20, y=1: closest beacon is at x=15, y=3", 20).Should().Be(56_000_011);
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