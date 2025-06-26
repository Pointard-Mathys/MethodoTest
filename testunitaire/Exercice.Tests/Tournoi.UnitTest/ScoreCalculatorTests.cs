using FluentAssertions;
using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest
{
    public class ScoreCalculatorServiceTests
    {
        private readonly IScoreCalculatorService _service;

        public ScoreCalculatorServiceTests()
        {
            _service = new ScoreCalculatorService(new ScoreCalculator());
        }

        /* ---------- Sécurité des arguments ---------- */

        /// <summary>
        /// Doit lever une exception si la liste des matchs est null.
        /// </summary>
        [Fact]
        public void CalculateScore_NullMatches_ThrowsArgumentNullException()
        {
            Action act = () => _service.CalculateScore(null!);

            act.Should().Throw<ArgumentNullException>()
               .WithParameterName("matches");
        }

        /// <summary>
        /// Doit lever une exception si les points de pénalité sont négatifs.
        /// </summary>
        [Fact]
        public void CalculateScore_NegativePenaltyPoints_ThrowsArgumentException()
        {
            var matches = BuildMatchList(MatchResult.Result.Win);

            Action act = () => _service.CalculateScore(matches, penaltyPoints: -1);

            act.Should().Throw<ArgumentException>()
               .WithParameterName("penaltyPoints");
        }

        /* ---------- Cas fonctionnels ---------- */

        /// <summary>
        /// Un joueur disqualifié obtient toujours 0 point.
        /// </summary>
        [Fact]
        public void CalculateScore_Disqualified_ReturnsZero()
        {
            var matches = BuildMatchList(MatchResult.Result.Win, MatchResult.Result.Draw);

            var score = _service.CalculateScore(matches, isDisqualified: true);

            score.Should().Be(0);
        }

        /// <summary>
        /// Trois victoires consécutives déclenchent un bonus de 5 pts.
        /// </summary>
        [Fact]
        public void CalculateScore_ThreeConsecutiveWins_AddsBonus()
        {
            // 3 victoires = 9 + 5 (bonus) = 14
            var matches = BuildMatchList(
                MatchResult.Result.Win,
                MatchResult.Result.Win,
                MatchResult.Result.Win);

            var score = _service.CalculateScore(matches);

            score.Should().Be(14);
        }

        /// <summary>
        /// Score combiné avec résultats variés et série de victoires.
        /// </summary>
        [Fact]
        public void CalculateScore_MixedResults_ReturnsExpectedScore()
        {
            // Win (3) + Draw (1) + Win (3) + Win (3) + Win + bonus (3+5) + Loss (0) = 18
            var matches = BuildMatchList(
                MatchResult.Result.Win,
                MatchResult.Result.Draw,
                MatchResult.Result.Win,
                MatchResult.Result.Win,
                MatchResult.Result.Win,
                MatchResult.Result.Loss);

            var score = _service.CalculateScore(matches);

            score.Should().Be(18);
        }

        /// <summary>
        /// Le score ne peut jamais être inférieur à 0 même avec de fortes pénalités.
        /// </summary>
        [Fact]
        public void CalculateScore_Penalties_CannotDropBelowZero()
        {
            var matches = BuildMatchList(MatchResult.Result.Draw); // score = 1

            var score = _service.CalculateScore(matches, penaltyPoints: 10);

            score.Should().Be(0);
        }

        /* ---------- Méthode auxiliaire ---------- */

        private static IList<MatchResult> BuildMatchList(params MatchResult.Result[] outcomes)
        {
            return outcomes.Select(o => new MatchResult { Outcome = o }).ToList();
        }
    }
}