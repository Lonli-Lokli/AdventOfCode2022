// Copyright (c) 2019 under MIT license.

namespace Day_02;
using static Core.Helpers;

public static class Run
{
    public static IEnumerable<string> SanitizeInput(string input) =>
        input.Split(Environment.NewLine).Select(row => row.Trim());

    public static IEnumerable<(GameChoice opponent, GameChoice me)> ParseInputByChoice(IEnumerable<string> sanitized) =>
        sanitized.Select(row => row.Split(" "))
            .Select(roundData =>
            (
                opponent: ParseAsGameChoice(roundData[0]),
                me: ParseAsGameChoice(roundData[1])
            ));
    public static IEnumerable<(GameChoice opponent, GameChoice me)> ParseInputByEnd(IEnumerable<string> sanitized) =>
        sanitized.Select(row => row.Split(" "))
            .Select(roundData =>
            (
                opponent: ParseAsGameChoice(roundData[0]),
                me: GetChoiceByExpectedResult(ParseAsGameChoice(roundData[0]), ParseAsRoundResult(roundData[1]))
            ));
    
    public static GameChoice ParseAsGameChoice(string input) => input.ToUpperInvariant() switch
    {
        "A" or "X" => GameChoice.Rock,
        "B" or "Y" => GameChoice.Paper,
        "C" or "Z" => GameChoice.Scissors,
        _ => throw new ArgumentOutOfRangeException(nameof(input), $"given value: '{input}'")
    };
    
    public static RoundResult ParseAsRoundResult(string input) => input.ToUpperInvariant() switch
    {
        "X" => RoundResult.Loose,
        "Y" => RoundResult.Draw,
        "Z" => RoundResult.Win,
        _ => throw new ArgumentOutOfRangeException(nameof(input), $"given value: '{input}'")
    };

    
    public static int GetRoundResult((GameChoice opponent, GameChoice me) round) =>
        Convert.ToInt32(round.me) + Convert.ToInt32(GetWinnerByChoices(round.me, round.opponent));

    public static RoundResult GetWinnerByChoices(GameChoice a, GameChoice b) =>
        a == b
            ? RoundResult.Draw
            : a switch
            {
                GameChoice.Rock => b == GameChoice.Scissors ? RoundResult.Win : RoundResult.Loose,
                GameChoice.Paper => b == GameChoice.Rock ? RoundResult.Win : RoundResult.Loose,
                GameChoice.Scissors => b == GameChoice.Paper ? RoundResult.Win : RoundResult.Loose,
                _ => throw new ArgumentOutOfRangeException(nameof(a), "Should not happen")
            };
    
    public static GameChoice GetChoiceByExpectedResult(GameChoice choice, RoundResult expectedResult) =>
            expectedResult switch
            {
                RoundResult.Draw => choice,
                RoundResult.Win => GetWinLoosePairOf(choice).looseTo,
                RoundResult.Loose => GetWinLoosePairOf(choice).winOver,
                _ => throw new ArgumentOutOfRangeException(nameof(choice), "Should not happen")
            };

    public static (GameChoice winOver, GameChoice looseTo) GetWinLoosePairOf(GameChoice a) => a switch
    {
        GameChoice.Rock => (winOver: GameChoice.Scissors, looseTo: GameChoice.Paper),
        GameChoice.Paper => (winOver: GameChoice.Rock, looseTo: GameChoice.Scissors),
        GameChoice.Scissors => (winOver: GameChoice.Paper, looseTo: GameChoice.Rock),
        _ => throw new ArgumentOutOfRangeException(nameof(a), "Should not happen")
    };

    public static int FollowStrategyResult(string input) => input.Pipe(SanitizeInput)
        .Pipe(ParseInputByChoice)
        .Pipe(round => round.Select(GetRoundResult))
        .Pipe(roundResults => roundResults.Sum());
    
    public static int FollowEndResult(string input) => input.Pipe(SanitizeInput)
        .Pipe(ParseInputByEnd)
        .Pipe(round => round.Select(GetRoundResult))
        .Pipe(roundResults => roundResults.Sum());
}

public enum GameChoice
{
    Rock = 1,
    Paper = 2,
    Scissors = 3
}

public enum RoundResult
{
    Loose = 0,
    Draw = 3,
    Win = 6
}