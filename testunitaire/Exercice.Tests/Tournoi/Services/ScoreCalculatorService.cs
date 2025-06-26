using System;
using System.Collections.Generic;
using Tournoi.Contracts;
using Tournoi.Models;

namespace Tournoi.Services
{
    public class ScoreCalculatorService : IScoreCalculatorService
    {
        public int CalculateScore(List<MatchResult> matches, bool isDisqualified = false, int penaltyPoints = 0)
        {
            ValidateInput(matches, penaltyPoints);

            if (isDisqualified)
            {
                return 0;
            }

            int baseScore = BaseScoreCalculator.CalculateBaseScore(matches);
            int bonus = BonusAndPenaltyCalculator.CalculateBonus(matches);
            int finalScore = baseScore + bonus;

            return BonusAndPenaltyCalculator.ApplyPenalties(finalScore, penaltyPoints);
        }

        
        
        
        
        private void ValidateInput(List<MatchResult> matches, int penaltyPoints)
        {
            if (matches == null)
            {
                throw new ArgumentNullException(nameof(matches), "The list of matches cannot be null.");
            }

            if (penaltyPoints < 0)
            {
                throw new ArgumentException("Penalty points cannot be negative.", nameof(penaltyPoints));
            }
        }
    }
}