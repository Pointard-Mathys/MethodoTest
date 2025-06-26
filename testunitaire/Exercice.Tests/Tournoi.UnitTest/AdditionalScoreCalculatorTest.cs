using FluentAssertions;
using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest;

public class ScoreCalculatorAdditionalTests
{
    private readonly ScoreCalculatorService _calculator = new();

    // 2. Two wins → 6 points
    [Fact]
    public void Should_Calculate_Two_Wins()
    {
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win)
        };

        var score = _calculator.CalculateScore(matches);

        score.Should().Be(6);
    }

    // 3. Three draws → 3 points
    [Fact]
    public void Should_Calculate_Three_Draws()
    {
        var matches = Enumerable.Repeat(new MatchResult(MatchResult.Result.Draw), 3).ToList();
        var score = _calculator.CalculateScore(matches);
        score.Should().Be(3);
    }

    // 4. Two losses → 0 point
    [Fact]
    public void Should_Calculate_Two_Losses()
    {
        var matches = Enumerable.Repeat(new MatchResult(MatchResult.Result.Loss), 2).ToList();
        var score = _calculator.CalculateScore(matches);
        score.Should().Be(0);
    }

    // 6. Four consecutive wins → 17 points (12 + 5 bonus once)
    [Fact]
    public void Should_Add_Bonus_For_Four_Consecutive_Wins()
    {
        var matches = Enumerable.Repeat(new MatchResult(MatchResult.Result.Win), 4).ToList();
        var score = _calculator.CalculateScore(matches);
        score.Should().Be(17);
    }

    // 7. Streak interrupted → no bonus
    [Fact]
    public void Should_Not_Add_Bonus_If_Streak_Broken()
    {
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Loss),
            new MatchResult(MatchResult.Result.Win)
        };

        var score = _calculator.CalculateScore(matches);
        score.Should().Be(6);
    }

    // 8. Multiple streaks, but only one bonus per tournoi → 26 points
    [Fact]
    public void Should_Add_Only_One_Bonus_Per_Tournament()
    {
        var matches = new List<MatchResult>();
        matches.AddRange(Enumerable.Repeat(new MatchResult(MatchResult.Result.Win), 3));
        matches.Add(new MatchResult(MatchResult.Result.Loss));
        matches.AddRange(Enumerable.Repeat(new MatchResult(MatchResult.Result.Win), 4));

        var score = _calculator.CalculateScore(matches);
        score.Should().Be(26);
    }

    // 9. Draw breaks the sequence before 3 wins → 10 points, no bonus
    [Fact]
    public void Should_Not_Add_Bonus_When_Sequence_Broken_By_Draw()
    {
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Draw),
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win)
        };

        var score = _calculator.CalculateScore(matches);
        score.Should().Be(10);
    }

    // 11. Disqualified without combat → 0 point
    [Fact]
    public void Should_Return_Zero_When_Disqualified_Without_Matches()
    {
        var score = _calculator.CalculateScore(new List<MatchResult>(), isDisqualified: true);
        score.Should().Be(0);
    }

    // 12. Score 10 – penalties 3 → 7 points
    [Fact]
    public void Should_Subtract_Penalties_When_Lower_Than_Score()
    {
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Draw)
        };

        var score = _calculator.CalculateScore(matches, penaltyPoints: 3);
        score.Should().Be(7);
    }

    // 13. Score 5 – penalties 8 → 0 point
    [Fact]
    public void Should_Zero_Score_When_Penalties_Exceed()
    {
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Draw),
            new MatchResult(MatchResult.Result.Draw)
        };

        var score = _calculator.CalculateScore(matches, penaltyPoints: 8);
        score.Should().Be(0);
    }

    // 14. Score 7 – penalties 7 → 0 point
    [Fact]
    public void Should_Zero_Score_When_Penalties_Equal()
    {
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Draw)
        };

        var score = _calculator.CalculateScore(matches, penaltyPoints: 7);
        score.Should().Be(0);
    }

    // 15. Empty list → 0 point
    [Fact]
    public void Should_Return_Zero_For_Empty_Match_List()
    {
        var score = _calculator.CalculateScore(new List<MatchResult>());
        score.Should().Be(0);
    }

    // 17. Negative penalties → ArgumentException
    [Fact]
    public void Should_Throw_When_Penalties_Negative()
    {
        var matches = new List<MatchResult> { new MatchResult(MatchResult.Result.Win) };
        Action act = () => _calculator.CalculateScore(matches, penaltyPoints: -1);
        act.Should().Throw<ArgumentException>()
           .WithParameterName("penaltyPoints");
    }

    // 18. Very long tournoi : 100 combats (pattern WWWDL) → 205 points
    [Fact]
    public void Should_Calculate_Score_For_Very_Long_Tournament()
    {
        var matches = new List<MatchResult>();
        for (int i = 0; i < 20; i++)
        {
            matches.AddRange(new[]
            {
                new MatchResult(MatchResult.Result.Win),
                new MatchResult(MatchResult.Result.Win),
                new MatchResult(MatchResult.Result.Win),
                new MatchResult(MatchResult.Result.Draw),
                new MatchResult(MatchResult.Result.Loss)
            });
        }

        // Base: 20×10 = 200, bonus: +5 (only once) → 205
        var score = _calculator.CalculateScore(matches);
        score.Should().Be(205);
    }

    // 20. Complex scenarios with MemberData
    public static IEnumerable<object[]> ComplexScenarios => new[]
    {
        new object[] { new[] { "W", "W", "W", "W" }, 17 },
        new object[] { new[] { "W", "W", "L", "W" }, 6 },
        new object[] { new[] { "W", "W", "W", "L", "W", "W", "W", "W" }, 26 },
        new object[] { new[] { "W", "D", "W", "W" }, 10 }
    };

    [Theory]
    [MemberData(nameof(ComplexScenarios))]
    public void Should_Calculate_Score_With_Complex_MemberData(string[] sequence, int expected)
    {
        var matches = sequence.Select(s => s switch
        {
            "W" => new MatchResult(MatchResult.Result.Win),
            "D" => new MatchResult(MatchResult.Result.Draw),
            _ => new MatchResult(MatchResult.Result.Loss)
        }).ToList();

        var score = _calculator.CalculateScore(matches);
        score.Should().Be(expected);
    }
}
