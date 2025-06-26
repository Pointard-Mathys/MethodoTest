using FluentAssertions;
using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest;

public class BaseScoreCalculatorTests
{
    private static MatchResult W() => new() { Outcome = MatchResult.Result.Win };
    private static MatchResult D() => new() { Outcome = MatchResult.Result.Draw };
    private static MatchResult L() => new() { Outcome = MatchResult.Result.Loss };

    /// <summary>
    /// Doit retourner 3 points par victoire.
    /// </summary>
    [Fact]
    public void CalculateBaseScore_AllWins_ReturnsThreePerWin()
    {
        var matches = new List<MatchResult> { W(), W(), W() };

        var score = BaseScoreCalculator.CalculateBaseScore(matches);

        score.Should().Be(9);
    }

    /// <summary>
    /// Doit retourner la somme attendue pour un mélange de résultats.
    /// </summary>
    [Fact]
    public void CalculateBaseScore_MixedResults_ReturnsExpectedScore()
    {
        var matches = new List<MatchResult> { W(), D(), L(), W() };

        var score = BaseScoreCalculator.CalculateBaseScore(matches);

        // 3 + 1 + 0 + 3 = 7
        score.Should().Be(7);
    }

    /// <summary>
    /// Doit retourner 0 s’il n’y a aucun match.
    /// </summary>
    [Fact]
    public void CalculateBaseScore_NoMatches_ReturnsZero()
    {
        var score = BaseScoreCalculator.CalculateBaseScore(new List<MatchResult>());

        score.Should().Be(0);
    }

    /// <summary>
    /// Doit lever une exception si le scoreCalculator est null.
    /// </summary>
    [Fact]
    public void Should_Throw_If_MatchResultService_Is_Null()
    {
        Action act = () => new TournamentRanking(null!);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*matchResult*");
    }
}