using FluentAssertions;
using Xunit;

namespace Day_03.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData(@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
abcd
CrZsJsPPZsGzwwsLwLmpwMDw")]
    public void InitialDataOriginal_Test(string input)
    {
        Run.CalculateDuplicatesWeight(input).Should().Be(157);
    }

    [Fact]
    public void CharTest()
    {
        ((int)'a').Should().Be(97);
        ((int)'A').Should().Be(65);
    }
    
    [Fact]
    public void SplitTest()
    {
        "jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL".Length.Should().Be(32);
        var split = Run.SplitIntoCommpartment("jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL");
        split.left.Should().Be("jqHRNqRjqzjGDLGL");
        split.right.Should().Be("rsFMfFZSrLrFZsSL");
    }
    
    [Theory]
    [InlineData(@"vJrwpWtwJgWrhcsFMMfFFhFp
jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
PmmdzqPrVvPwwTWBwg
wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
ttgJtRGJQctTZtZT
CrZsJsPPZsGzwwsLwLmpwMDw")]
    public void InitialSecondDataOriginal_Test(string input)
    {
        Run.CalculateGroupBadgesWeight(input).Should().Be(70);
    }
 }