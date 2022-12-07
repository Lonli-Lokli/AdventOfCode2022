using FluentAssertions;
using Xunit;

namespace Day_07.Tests;

public class UnitTest1
{
    [Fact]
    public void InitialFirstInput_Test()
    {
        Run.FirstInput(@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k").Should().Be(95437);
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
        Run.SecondInput(@"$ cd /
$ ls
dir a
14848514 b.txt
8504156 c.dat
dir d
$ cd a
$ ls
dir e
29116 f
2557 g
62596 h.lst
$ cd e
$ ls
584 i
$ cd ..
$ cd ..
$ cd d
$ ls
4060174 j
8033020 d.log
5626152 d.ext
7214296 k").Should().Be(24933642);
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