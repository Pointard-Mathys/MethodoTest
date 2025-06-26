// =====================================
//  Domain models
// =====================================
namespace Tournoi.Models;

/// <summary>
/// Simple DTO representing a tournament participant.
/// </summary>
public class Player
{
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Results of every match played by this player (in chronological order).
    /// </summary>
    public List<MatchResult> Matches { get; set; } = new();

    /// <summary>
    /// True if the player has been disqualified – their final score is forced to 0.
    /// </summary>
    public bool IsDisqualified { get; set; } = false;

    /// <summary>
    /// Penalty points that are deducted from the computed score (but never below 0).
    /// </summary>
    public int PenaltyPoints { get; set; } = 0;
}