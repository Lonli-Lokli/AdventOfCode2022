using FluentAssertions;
using Xunit;

namespace Day_05.Tests;

public class UnitTest1
{
    [Fact]
    public void InitialFirstInput_Test()
    {
        Run.FirstInput(@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2", 3).Should().Be("CMZ");
    }
    
    [Fact]
    public void InitialSecondInput_Test()
    {
        Run.SecondInput(@"    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2", 3).Should().Be("MCD");
    }
}