using System;
using System.Collections.Generic;
using Tournoi.Models;
using Tournoi.Services;
using Tournoi.Contracts;

class Program
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    static void Main()
    {
        Console.WriteLine("=== Système de Tournoi ===\n");

        // 1. Création des joueurs avec leurs résultats
        var players = new List<Player>
        {
            new Player
            {
                Name = "Alice",
                Matches = new List<MatchResult>
                {
                    new(MatchResult.Result.Win),
                    new(MatchResult.Result.Win),
                    new(MatchResult.Result.Win)
                },
                IsDisqualified = false,
                PenaltyPoints = 1
            },
            new Player
            {
                Name = "Bob",
                Matches = new List<MatchResult>
                {
                    new(MatchResult.Result.Win),
                    new(MatchResult.Result.Draw),
                    new(MatchResult.Result.Loss)
                },
                IsDisqualified = false,
                PenaltyPoints = 0
            },
            new Player
            {
                Name = "Charlie",
                Matches = new List<MatchResult>
                {
                    new(MatchResult.Result.Draw),
                    new(MatchResult.Result.Draw),
                    new(MatchResult.Result.Draw)
                },
                IsDisqualified = true, // disqualifié
                PenaltyPoints = 0
            }
        };

        // 2. Création du service de calcul de score
        MatchResultService scoreCalculator = new MatchResultService();

        // 3. Création du système de classement
        var ranking = new TournamentRanking(scoreCalculator);

        // 4. Affichage du classement
        Console.WriteLine("Classement :");
        var rankedPlayers = ranking.GetRanking(players);
        foreach (var player in rankedPlayers)
        {
            int score = scoreCalculator.CalculateScore(player.Matches, player.IsDisqualified, player.PenaltyPoints);
            Console.WriteLine($"- {player.Name}: {score} pts {(player.IsDisqualified ? "(disqualifié)" : "")}");
        }

        // 5. Affichage du champion
        var champion = ranking.GetChampion(players);
        Console.WriteLine("\n🏆 Champion : " + (champion != null ? champion.Name : "Aucun"));

        Console.WriteLine("\nSimulation terminée.");
    }
}