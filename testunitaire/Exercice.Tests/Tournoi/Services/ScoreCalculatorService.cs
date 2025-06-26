using System;
using System.Collections.Generic;
using System.Linq;
using Tournoi.Models;

namespace Tournoi.Services
{
    /// <summary>
    /// Contrat du service de calcul de score.
    /// </summary>
    public interface IScoreCalculatorService
    {
        int CalculateScore(IEnumerable<MatchResult> matches,
            bool isDisqualified = false,
            int penaltyPoints   = 0);
    }

    /// <summary>
    /// Implémentation du service.
    /// </summary>
    public sealed class ScoreCalculatorService : IScoreCalculatorService
    {
        private readonly ScoreCalculator _scoreCalculator;

        /// <summary>
        /// Injection du composant métier (facile à remplacer en test).
        /// </summary>
        public ScoreCalculatorService(ScoreCalculator scoreCalculator)
        {
            _scoreCalculator = scoreCalculator
                               ?? throw new ArgumentNullException(nameof(scoreCalculator));
        }

        public int CalculateScore(IEnumerable<MatchResult> matches,
            bool isDisqualified = false,
            int penaltyPoints   = 0)
        {
            // Convertit en List pour l’API attendue par ScoreCalculator
            var matchList = matches?.ToList();
            return _scoreCalculator.CalculateScore(matchList, isDisqualified, penaltyPoints);
        }
    }
}