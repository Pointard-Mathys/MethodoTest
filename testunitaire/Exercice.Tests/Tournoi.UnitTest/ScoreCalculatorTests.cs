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

        [Fact]
        public void CalculateScore_NullMatches_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _service.CalculateScore(null));
        }

        [Fact]
        public void CalculateScore_NegativePenaltyPoints_ThrowsArgumentException()
        {
            var matches = BuildMatchList(MatchResult.Result.Win);
            Assert.Throws<ArgumentException>(() => _service.CalculateScore(matches, penaltyPoints: -1));
        }

        /* ---------- Cas fonctionnels ---------- */

        [Fact]
        public void CalculateScore_Disqualified_ReturnsZero()
        {
            var matches = BuildMatchList(MatchResult.Result.Win, MatchResult.Result.Draw);
            int score = _service.CalculateScore(matches, isDisqualified: true);

            Assert.Equal(0, score);
        }

        [Fact]
        public void CalculateScore_ThreeConsecutiveWins_AddsBonus()
        {
            // 3 victoires : 3×3 pts + bonus 5 pts = 14
            var matches = BuildMatchList(
                MatchResult.Result.Win,
                MatchResult.Result.Win,
                MatchResult.Result.Win);

            int score = _service.CalculateScore(matches);

            Assert.Equal(14, score);
        }

        [Fact]
        public void CalculateScore_MixedResults_ReturnsExpectedScore()
        {
            // Win (3) + Draw (1) + Win (3) + Win (3) + Win (3+bonus 5) + Loss (0) = 18
            var matches = BuildMatchList(
                MatchResult.Result.Win,
                MatchResult.Result.Draw,
                MatchResult.Result.Win,
                MatchResult.Result.Win,
                MatchResult.Result.Win,
                MatchResult.Result.Loss);

            int score = _service.CalculateScore(matches);

            Assert.Equal(18, score);
        }

        [Fact]
        public void CalculateScore_Penalties_CannotDropBelowZero()
        {
            var matches = BuildMatchList(MatchResult.Result.Draw); // score = 1
            int score = _service.CalculateScore(matches, penaltyPoints: 10);

            Assert.Equal(0, score);
        }

        /* ---------- Méthode auxiliaire ---------- */

        private static IList<MatchResult> BuildMatchList(params MatchResult.Result[] outcomes)
        {
            var list = new List<MatchResult>();
            foreach (var outcome in outcomes)
            {
                list.Add(new MatchResult { Outcome = outcome });
            }
            return list;
        }
    }
}
