using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest;

public class MatchResultTests
{
    private readonly MatchResultService _service = new();

    private static MatchResult W() => new() { Outcome = MatchResult.Result.Win };
    private static MatchResult D() => new() { Outcome = MatchResult.Result.Draw };
    private static MatchResult L() => new() { Outcome = MatchResult.Result.Loss };

    [Fact]
    public void CalculateScore_PlayerDisqualified_ReturnsZero()
    {
        var matches = new List<MatchResult> { W(), W() };

        var score = _service.CalculateScore(matches, isDisqualified: true);

        Assert.Equal(0, score);
    }

    [Fact]
    public void CalculateScore_WithBonusAndPenalty_AppliesAllModifiers()
    {
        // 3 Victoires consécutives = 9 pts + bonus 5 = 14, –4 de pénalité = 10
        var matches = new List<MatchResult> { W(), W(), W() };

        var score = _service.CalculateScore(matches, penaltyPoints: 4);

        Assert.Equal(10, score);
    }

    [Fact]
    public void CalculateScore_WithNoMatches_ReturnsZero()
    {
        var score = _service.CalculateScore(new List<MatchResult>());

        Assert.Equal(0, score);
    }

    [Fact]
    public void CalculateScore_NullMatches_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => _service.CalculateScore(null!));
    }

    [Fact]
    public void CalculateScore_NegativePenalty_ThrowsArgumentException()
    {
        var matches = new List<MatchResult>();

        Assert.Throws<ArgumentException>(() => _service.CalculateScore(matches, penaltyPoints: -1));
    }
}