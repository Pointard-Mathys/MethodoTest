using FluentAssertions;
using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest;

public class BonusAndPenaltyCalculatorTests
{
    private static MatchResult W() => new() { Outcome = MatchResult.Result.Win };
    private static MatchResult D() => new() { Outcome = MatchResult.Result.Draw };
    private static MatchResult L() => new() { Outcome = MatchResult.Result.Loss };

    /// <summary>
    /// Ne doit pas accorder de bonus s’il n’y a pas 3 victoires consécutives.
    /// </summary>
    [Fact]
    public void CalculateBonus_NoConsecutiveWins_ReturnsZero()
    {
        var matches = new List<MatchResult> { W(), D(), W(), L() };

        var bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);

        bonus.Should().Be(0);
    }

    /// <summary>
    /// Doit donner un bonus de 5 points pour une série de 3 victoires consécutives.
    /// </summary>
    [Fact]
    public void CalculateBonus_ThreeConsecutiveWins_GivesSingleBonus()
    {
        var matches = new List<MatchResult> { W(), W(), W() };

        var bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);

        bonus.Should().Be(5);
    }

    /// <summary>
    /// Doit donner un double bonus pour 6 victoires consécutives.
    /// </summary>
    [Fact]
    public void CalculateBonus_SixConsecutiveWins_GivesDoubleBonus()
    {
        var matches = new List<MatchResult> { W(), W(), W(), W(), W(), W() };

        var bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);

        bonus.Should().Be(10);
    }

    /// <summary>
    /// Doit réinitialiser la série en cas de résultat autre qu’une victoire.
    /// </summary>
    [Fact]
    public void CalculateBonus_StreakBroken_ResetsCounter()
    {
        var matches = new List<MatchResult> { W(), W(), W(), D(), W(), W(), W() };

        var bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);

        bonus.Should().Be(10); // 2 séries de 3 victoires
    }

    /// <summary>
    /// Doit appliquer les pénalités correctement, sans descendre sous 0.
    /// </summary>
    [Theory]
    [InlineData(20, 5, 15)]
    [InlineData(4, 10, 0)]   // Jamais négatif
    public void ApplyPenalties_ReturnsExpectedValue(int score, int penalty, int expected)
    {
        var final = BonusAndPenaltyCalculator.ApplyPenalties(score, penalty);

        final.Should().Be(expected);
    }
}