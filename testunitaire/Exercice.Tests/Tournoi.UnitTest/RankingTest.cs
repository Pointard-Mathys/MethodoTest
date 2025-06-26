using FluentAssertions;
using Tournoi.Models;
using Tournoi.Services;


// =====================================
//  Unit tests (xUnit + FluentAssertions)
// =====================================
namespace Tournoi.UnitTest;

public class TournamentRankingTest
{
    private readonly ScoreCalculatorService _calculator = new();
    private TournamentRanking Ranking => new(_calculator);

    #region Helpers
    private static List<MatchResult> Repeat(MatchResult.Result r, int count) =>
        Enumerable.Range(0, count).Select(_ => new MatchResult(r)).ToList();

    private static Player NewPlayer(string name, int wins, int draws = 0, int losses = 0,
                                    bool dq = false, int penalty = 0)
    {
        var matches = new List<MatchResult>();
        matches.AddRange(Repeat(MatchResult.Result.Win, wins));
        matches.AddRange(Repeat(MatchResult.Result.Draw, draws));
        matches.AddRange(Repeat(MatchResult.Result.Loss, losses));

        return new Player
        {
            Name = name,
            Matches = matches,
            IsDisqualified = dq,
            PenaltyPoints = penalty
        };
    }
    #endregion

    /// <summary>
    /// Classement correct par score décroissant (aucune égalité).
    /// </summary>
    [Fact]
    public void Should_Rank_Players_By_Descending_Score()
    {
        // Arrange
        var players = new List<Player>
        {
            NewPlayer("Charlie", wins: 2),           // 6 pts
            NewPlayer("Alice", wins: 3),           // 14 pts
            NewPlayer("Bob", 0, 3)     // 3  pts
        };

        // Act
        var ordered = Ranking.GetRanking(players).Select(p => p.Name).ToList();

        // Assert
        ordered.Should().Equal("Alice", "Charlie", "Bob");
    }

    /// <summary>
    /// Gestion des égalités : même score → tri alphabétique.
    /// </summary>
    [Fact]
    public void Should_Order_Tied_Score_Alphabetically()
    {
        // Arrange → deux joueurs avec 3 victoires (14 pts chacun)
        var players = new List<Player>
        {
            NewPlayer("Bob",   wins: 3),
            NewPlayer("Alice", wins: 3),
            NewPlayer("Zed",   wins: 1) // 3 pts
        };

        // Act
        var ordered = Ranking.GetRanking(players).Select(p => p.Name).ToList();

        // Assert (Alice avant Bob car ordre alphabétique)
        ordered.Should().Equal("Alice", "Bob", "Zed");
    }

    /// <summary>
    /// Champion avec score maximum.
    /// </summary>
    [Fact]
    public void Should_Return_Champion_With_Highest_Score()
    {
        // Arrange
        var players = new List<Player>
        {
            NewPlayer("Eve",   wins: 1, draws: 2), // 5 pts
            NewPlayer("Dave",  wins: 4),           // 17 pts (bonus)
            NewPlayer("Mia",   wins: 3, dq: true)   // 0 pt (disqual.)
        };

        // Act
        var champion = Ranking.GetChampion(players);

        // Assert
        champion.Should().NotBeNull();
        champion!.Name.Should().Be("Dave");
    }

    /// <summary>
    /// Tous les joueurs disqualifiés → champion null + classement toujours retourné (mais scores 0).
    /// </summary>
    [Fact]
    public void Should_Return_Null_Champion_When_All_Disqualified()
    {
        // Arrange
        var players = new List<Player>
        {
            NewPlayer("Al", wins: 3, dq: true),
            NewPlayer("Bea", wins: 2, dq: true)
        };

        // Act
        var champion = Ranking.GetChampion(players);
        var ranking = Ranking.GetRanking(players);

        // Assert
        champion.Should().BeNull();
        ranking.Should().HaveCount(2);
        // Scores à 0 → ordre alphabétique
        ranking.Select(p => p.Name).Should().Equal("Al", "Bea");
    }
}
