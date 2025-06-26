using System;
using System.Collections.Generic;
using Tournoi.Contracts;
using Tournoi.Models;

namespace Tournoi.Services
{
    public class ScoreCalculatorService : IScoreCalculatorService
    {
        public int CalculateScore(
            List<MatchResult>? matches,
            bool isDisqualified = false,
            int penaltyPoints = 0)
        {
            if (matches is null)
                throw new ArgumentNullException(nameof(matches), "matches cannot be null");

            if (penaltyPoints < 0)
                throw new ArgumentException("Penalty points cannot be negative", nameof(penaltyPoints));

            if (isDisqualified)
                return 0;

            int score      = 0;
            int winStreak  = 0;
            bool bonusGiven = false;           // <-- NOUVEAU

            foreach (var match in matches)
            {
                switch (match.Outcome)
                {
                    case MatchResult.Result.Win:
                        score     += 3;
                        winStreak += 1;

                        // bonus si on atteint 3 victoires consécutives
                        if (winStreak == 3 && !bonusGiven)
                        {
                            score      += 5;
                            bonusGiven  = true;   // on n’accorde plus de bonus ensuite
                        }
                        break;

                    case MatchResult.Result.Draw:
                        score     += 1;
                        winStreak  = 0;
                        break;

                    case MatchResult.Result.Loss:
                        winStreak  = 0;
                        break;
                }
            }

            // application des pénalités, sans jamais descendre sous zéro
            score = Math.Max(0, score - penaltyPoints);

            return score;
        }
    }
}