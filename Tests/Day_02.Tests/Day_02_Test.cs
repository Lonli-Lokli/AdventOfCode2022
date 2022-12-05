using FluentAssertions;
using Xunit;
using static Day_02.Run;

namespace Day_02.Tests;

public class UnitTest1
{
    [Theory]
    [InlineData(@"A Y
B X
C Z
B X")]
    public void InitialResultByChoice_Test(string input)
    {
        FollowStrategyResult(input).Should().Be(16);
    }
    
    [Theory]
    [InlineData(@"A Y
B X
C Z")]
    public void InitialResultByEnd_Test(string input)
    {
        FollowEndResult(input).Should().Be(12);
    }

    
    [Theory]
    [MemberData(nameof(Data))]
    public void Winners_Test(GameChoice x, GameChoice y, RoundResult z) 
    {
        GetWinnerByChoices(x, y).Should().Be(z);
    }

    public static TheoryData<GameChoice, GameChoice, RoundResult> Data =>
        new TheoryData<GameChoice, GameChoice, RoundResult>()
        {
            { GameChoice.Rock, GameChoice.Rock, RoundResult.Draw },
            { GameChoice.Rock, GameChoice.Paper, RoundResult.Loose },
            { GameChoice.Rock, GameChoice.Scissors, RoundResult.Win },

            { GameChoice.Paper, GameChoice.Paper, RoundResult.Draw },
            { GameChoice.Paper, GameChoice.Rock, RoundResult.Win },
            { GameChoice.Paper, GameChoice.Scissors, RoundResult.Loose },

            { GameChoice.Scissors, GameChoice.Paper, RoundResult.Win },
            { GameChoice.Scissors, GameChoice.Rock, RoundResult.Loose },
            { GameChoice.Scissors, GameChoice.Scissors, RoundResult.Draw }
        };
}