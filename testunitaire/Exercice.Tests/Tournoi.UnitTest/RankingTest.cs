using FluentAssertions;
using Tournoi.Models;
using Tournoi.Services;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;

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
    /// Classement par score décroissant, aucun joueur à égalité.
    /// </summary>
    [Fact]
    public void Should_Rank_Players_By_Descending_Score()
    {
        var players = new List<Player>
        {
            NewPlayer("Charlie", wins: 2),
            NewPlayer("Alice", wins: 3),
            NewPlayer("Bob", 0, 3)
        };

        var ordered = Ranking.GetRanking(players).Select(p => p.Name).ToList();

        ordered.Should().Equal("Alice", "Charlie", "Bob");
    }
    
    /// <summary>
    /// Doit lever une exception si la liste de joueurs est null.
    /// </summary>
    [Fact]
    public void Should_Throw_If_Players_Null_In_GetRanking()
    {
        Action act = () => Ranking.GetRanking(null!);

        act.Should().Throw<ArgumentNullException>()
            .WithMessage("*players*");
    }

    /// <summary>
    /// Égalité de score : classement alphabétique.
    /// </summary>
    [Fact]
    public void Should_Order_Tied_Score_Alphabetically()
    {
        var players = new List<Player>
        {
            NewPlayer("Bob", wins: 3),
            NewPlayer("Alice", wins: 3),
            NewPlayer("Zed", wins: 1)
        };

        var ordered = Ranking.GetRanking(players).Select(p => p.Name).ToList();

        ordered.Should().Equal("Alice", "Bob", "Zed");
    }

    /// <summary>
    /// Retourne le champion avec le score le plus élevé.
    /// </summary>
    [Fact]
    public void Should_Return_Champion_With_Highest_Score()
    {
        var players = new List<Player>
        {
            NewPlayer("Eve", wins: 1, draws: 2),
            NewPlayer("Dave", wins: 4),
            NewPlayer("Mia", wins: 3, dq: true)
        };

        var champion = Ranking.GetChampion(players);

        champion.Should().NotBeNull();
        champion!.Name.Should().Be("Dave");
    }
    
    /// <summary>
    /// Doit retourner null si aucun joueur n'est fourni (liste vide).
    /// </summary>
    [Fact]
    public void Should_Return_Null_Champion_If_No_Players()
    {
        var result = Ranking.GetChampion(new List<Player>());

        result.Should().BeNull();
    }

    /// <summary>
    /// Tous les joueurs disqualifiés : champion null, mais classement retourné.
    /// </summary>
    [Fact]
    public void Should_Return_Null_Champion_When_All_Disqualified()
    {
        var players = new List<Player>
        {
            NewPlayer("Al", wins: 3, dq: true),
            NewPlayer("Bea", wins: 2, dq: true)
        };

        var champion = Ranking.GetChampion(players);
        var ranking = Ranking.GetRanking(players);

        champion.Should().BeNull();
        ranking.Should().HaveCount(2);
        ranking.Select(p => p.Name).Should().Equal("Al", "Bea");
    }

    /// <summary>
    /// Doit lever une exception si la liste de matchs est null.
    /// </summary>
    [Fact]
    public void Should_Throw_If_Matches_Null()
    {
        Action act = () => _calculator.CalculateScore(null!);

        act.Should().Throw<ArgumentNullException>()
           .WithMessage("*matches*");
    }

    /// <summary>
    /// Doit lever une exception si les pénalités sont négatives.
    /// </summary>
    [Fact]
    public void Should_Throw_If_Penalty_Is_Negative()
    {
        var matches = Repeat(MatchResult.Result.Win, 2);

        Action act = () => _calculator.CalculateScore(matches, false, -5);

        act.Should().Throw<ArgumentException>()
           .WithMessage("*Penalty points cannot be negative*");
    }

    /// <summary>
    /// Doit retourner 0 si le joueur est disqualifié (peu importe ses matchs).
    /// </summary>
    [Fact]
    public void Should_Return_Zero_If_Disqualified()
    {
        var matches = Repeat(MatchResult.Result.Win, 3);

        var score = _calculator.CalculateScore(matches, true, 0);

        score.Should().Be(0);
    }
}