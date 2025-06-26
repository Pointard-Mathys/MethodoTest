using System.Collections.Generic;
using Tournoi.Models;

namespace Tournoi.Contracts
{
    public interface IScoreCalculatorService
    {
        int CalculateScore(List<MatchResult> matches, bool isDisqualified = false, int penaltyPoints = 0);
    }
}