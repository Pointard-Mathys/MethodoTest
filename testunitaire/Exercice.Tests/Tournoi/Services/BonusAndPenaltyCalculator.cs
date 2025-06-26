using System;
using System.Collections.Generic;
using Tournoi.Models;

namespace Tournoi.Services
{
    public static class BonusAndPenaltyCalculator
    {
        public static int CalculateBonus(List<MatchResult> matches)
        {
            int bonus = 0;
            int consecutiveWins = 0;

            foreach (var match in matches)
            {
                if (match.Outcome == MatchResult.Result.Win)
                {
                    consecutiveWins++;
                    if (consecutiveWins % 3 == 0)
                    {
                        bonus += 5;
                    }
                }
                else
                {
                    consecutiveWins = 0;
                }
            }

            return bonus;
        }

        public static int ApplyPenalties(int score, int penaltyPoints)
        {
            return Math.Max(score - penaltyPoints, 0);
        }
    }
}