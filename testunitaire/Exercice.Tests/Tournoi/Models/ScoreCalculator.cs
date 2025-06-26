using System;
using System.Collections.Generic;
namespace Tournoi.Models;

public class ScoreCalculator
{
    public int CalculateScore(List<MatchResult> matches, bool isDisqualified = false, int penaltyPoints = 0)
    {
        if (matches == null)
        {
            throw new ArgumentNullException(nameof(matches), "The list of matches cannot be null.");
        }

        if (penaltyPoints < 0)
        {
            throw new ArgumentException("Penalty points cannot be negative.", nameof(penaltyPoints));
        }

        if (isDisqualified)
        {
            return 0;
        }

        int score = 0;
        int consecutiveWins = 0;

        foreach (var match in matches)
        {
            switch (match.Outcome)
            {
                case MatchResult.Result.Win:
                    score += 3;
                    consecutiveWins++;
                    if (consecutiveWins == 3)
                    {
                        score += 5;
                    }
                    break;
                case MatchResult.Result.Draw:
                    score += 1;
                    consecutiveWins = 0;
                    break;
                case MatchResult.Result.Loss:
                    consecutiveWins = 0;
                    break;
            }
        }

        score = Math.Max(score - penaltyPoints, 0);
        return score;
    }
}