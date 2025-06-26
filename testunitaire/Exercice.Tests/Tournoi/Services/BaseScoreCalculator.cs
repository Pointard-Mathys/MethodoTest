using System.Collections.Generic;
using Tournoi.Models;

namespace Tournoi.Services
{
    public static class BaseScoreCalculator
    {
        public static int CalculateBaseScore(List<MatchResult> matches)
        {
            int score = 0;

            foreach (var match in matches)
            {
                switch (match.Outcome)
                {
                    case MatchResult.Result.Win:
                        score += 3;
                        break;
                    case MatchResult.Result.Draw:
                        score += 1;
                        break;
                    case MatchResult.Result.Loss:
                        break;
                }
            }

            return score;
        }
    }
}