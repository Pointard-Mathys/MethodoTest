using FluentAssertions;
using Tournoi.Models;
using Tournoi.Services;

namespace Tournoi.UnitTest;

public class ScoreCalculatorTest
{
    private readonly ScoreCalculatorService _calculator;

    public ScoreCalculatorTest()
    {
        _calculator = new ScoreCalculatorService();
    }

    [Fact]
    public void Should_Calculate_Basic_Score_Without_Bonus()
    {
        // Arrange
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Draw),
            new MatchResult(MatchResult.Result.Loss)
        };

        // Act
        var score = _calculator.CalculateScore(matches);

        // Assert
        score.Should().Be(4, "because 3+1+0 = 4 points without bonus");
    }

    [Fact]
    public void Should_Add_Bonus_For_Three_Consecutive_Wins()
    {
        // Arrange
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Draw)
        };

        // Act
        var score = _calculator.CalculateScore(matches);

        // Assert
        score.Should().Be(15, "because 3*3 + 1 + 5 bonus = 15 points");
    }

    [Fact]
    public void Should_Return_Zero_When_Disqualified()
    {
        // Arrange
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Win)
        };

        // Act
        var score = _calculator.CalculateScore(matches, isDisqualified: true);

        // Assert
        score.Should().Be(0, "because the player is disqualified");
    }

    [Fact]
    public void Should_Not_Allow_Negative_Final_Score()
    {
        // Arrange
        var matches = new List<MatchResult>
        {
            new MatchResult(MatchResult.Result.Win),
            new MatchResult(MatchResult.Result.Draw)
        };

        // Act
        var score = _calculator.CalculateScore(matches, penaltyPoints: 10);

        // Assert
        score.Should().Be(0, "because penalties cannot result in a negative score");
    }

    [Theory]
    [InlineData(3, 0, 0, 14)] // 3 wins → 9 + 5 bonus
    [InlineData(2, 1, 0, 7)]  // 2 wins, 1 draw → 7, no bonus
    [InlineData(0, 0, 3, 0)]  // 3 losses → 0 points
    public void Should_Calculate_Score_With_Different_Results(int wins, int draws, int losses, int expected)
    {
        // Arrange
        var matches = new List<MatchResult>();
        for (int i = 0; i < wins; i++) matches.Add(new MatchResult(MatchResult.Result.Win));
        for (int i = 0; i < draws; i++) matches.Add(new MatchResult(MatchResult.Result.Draw));
        for (int i = 0; i < losses; i++) matches.Add(new MatchResult(MatchResult.Result.Loss));

        // Act
        var score = _calculator.CalculateScore(matches);

        // Assert
        score.Should().Be(expected);
    }

    [Fact]
    public void Should_Throw_Exception_When_Matches_Is_Null()
    {
        // Act & Assert
        Action act = () => _calculator.CalculateScore(null);
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("matches")
            .WithMessage("*cannot be null*");
    }
}