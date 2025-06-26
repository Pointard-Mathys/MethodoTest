using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest;

public class BonusAndPenaltyCalculatorTests
{
    private static MatchResult W() => new() { Outcome = MatchResult.Result.Win };
    private static MatchResult D() => new() { Outcome = MatchResult.Result.Draw };
    private static MatchResult L() => new() { Outcome = MatchResult.Result.Loss };

    [Fact]
    public void CalculateBonus_NoConsecutiveWins_ReturnsZero()
    {
        var matches = new List<MatchResult> { W(), D(), W(), L() };

        var bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);

        Assert.Equal(0, bonus);
    }

    [Fact]
    public void CalculateBonus_ThreeConsecutiveWins_GivesSingleBonus()
    {
        var matches = new List<MatchResult> { W(), W(), W() };

        var bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);

        Assert.Equal(5, bonus);
    }

    [Fact]
    public void CalculateBonus_SixConsecutiveWins_GivesDoubleBonus()
    {
        var matches = new List<MatchResult> { W(), W(), W(), W(), W(), W() };

        var bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);

        Assert.Equal(10, bonus);
    }

    [Fact]
    public void CalculateBonus_StreakBroken_ResetsCounter()
    {
        var matches = new List<MatchResult> { W(), W(), W(), D(), W(), W(), W() };

        var bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);

        // Deux séries de 3 victoires → 2×5 pts
        Assert.Equal(10, bonus);
    }

    [Theory]
    [InlineData(20, 5, 15)]
    [InlineData(4, 10, 0)]   // Jamais négatif
    public void ApplyPenalties_ReturnsExpectedValue(int score, int penalty, int expected)
    {
        var final = BonusAndPenaltyCalculator.ApplyPenalties(score, penalty);

        Assert.Equal(expected, final);
    }
}