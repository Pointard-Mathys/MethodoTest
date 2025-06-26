using FluentAssertions;
using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest;

public class MatchResultTests
{
    private readonly MatchResultService _service = new();

    private static MatchResult W() => new() { Outcome = MatchResult.Result.Win };
    private static MatchResult D() => new() { Outcome = MatchResult.Result.Draw };
    private static MatchResult L() => new() { Outcome = MatchResult.Result.Loss };

    /// <summary>
    /// Un joueur disqualifié doit toujours obtenir un score de 0.
    /// </summary>
    [Fact]
    public void CalculateScore_PlayerDisqualified_ReturnsZero()
    {
        var matches = new List<MatchResult> { W(), W() };

        var score = _service.CalculateScore(matches, isDisqualified: true);

        score.Should().Be(0);
    }

    /// <summary>
    /// Applique correctement bonus et pénalité.
    /// </summary>
    [Fact]
    public void CalculateScore_WithBonusAndPenalty_AppliesAllModifiers()
    {
        // 3 victoires : 9 pts + bonus 5 = 14, –4 de pénalité = 10
        var matches = new List<MatchResult> { W(), W(), W() };

        var score = _service.CalculateScore(matches, penaltyPoints: 4);

        score.Should().Be(10);
    }

    /// <summary>
    /// Aucun match : score attendu = 0.
    /// </summary>
    [Fact]
    public void CalculateScore_WithNoMatches_ReturnsZero()
    {
        var score = _service.CalculateScore(new List<MatchResult>());

        score.Should().Be(0);
    }

    /// <summary>
    /// La méthode doit lever une exception si la liste des matchs est null.
    /// </summary>
    [Fact]
    public void CalculateScore_NullMatches_ThrowsArgumentNullException()
    {
        Action act = () => _service.CalculateScore(null!);

        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("matches");
    }

    /// <summary>
    /// La méthode doit lever une exception si les points de pénalité sont négatifs.
    /// </summary>
    [Fact]
    public void CalculateScore_NegativePenalty_ThrowsArgumentException()
    {
        var matches = new List<MatchResult>();

        Action act = () => _service.CalculateScore(matches, penaltyPoints: -1);

        act.Should().Throw<ArgumentException>()
           .WithParameterName("penaltyPoints");
    }
}