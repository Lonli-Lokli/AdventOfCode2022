using FluentAssertions;
using Xunit;

namespace Day_04.Tests;

public class UnitTest1
{
    [Fact]
    public void InitialFirstInput_Test()
    {
        Run.FirstInput(@"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8").Should().Be(2);
    }
    
    [Fact]
    public void InitialSecondInput_Test()
    {
        Run.FirstInput(@"2-4,6-8
2-3,4-5
5-7,7-9
2-8,3-7
6-6,4-6
2-6,4-8").Should().Be(4);
    }
}