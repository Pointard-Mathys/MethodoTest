using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest;

public class BaseScoreCalculatorTests
{
    private static MatchResult W() => new() { Outcome = MatchResult.Result.Win };
    private static MatchResult D() => new() { Outcome = MatchResult.Result.Draw };
    private static MatchResult L() => new() { Outcome = MatchResult.Result.Loss };

    [Fact]
    public void CalculateBaseScore_AllWins_ReturnsThreePerWin()
    {
        var matches = new List<MatchResult> { W(), W(), W() };

        var score = BaseScoreCalculator.CalculateBaseScore(matches);

        Assert.Equal(9, score);
    }

    [Fact]
    public void CalculateBaseScore_MixedResults_ReturnsExpectedScore()
    {
        var matches = new List<MatchResult> { W(), D(), L(), W() };

        var score = BaseScoreCalculator.CalculateBaseScore(matches);

        // 3 + 1 + 0 + 3 = 7
        Assert.Equal(7, score);
    }

    [Fact]
    public void CalculateBaseScore_NoMatches_ReturnsZero()
    {
        var score = BaseScoreCalculator.CalculateBaseScore(new List<MatchResult>());

        Assert.Equal(0, score);
    }
}