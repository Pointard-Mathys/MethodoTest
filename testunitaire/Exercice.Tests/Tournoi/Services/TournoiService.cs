// =====================================
//  Services
// =====================================

using Tournoi.Models;

namespace Tournoi.Services;

/// <summary>
/// Provides ranking utilities on top of <see cref="MatchResultService"/>.
/// </summary>
public class TournamentRanking
{
    private readonly MatchResultService _matchResult;

    public TournamentRanking(MatchResultService matchResult)
    {
        _matchResult = matchResult ?? throw new ArgumentNullException(nameof(matchResult));
    }

    /// <summary>
    /// Orders players by score (highest → lowest). If several players share exactly
    /// the same score, they are ordered alphabetically (ordinal‑ignore‑case) so that
    /// the ranking is deterministic.
    /// </summary>
    public List<Player> GetRanking(List<Player> players)
    {
        if (players is null) throw new ArgumentNullException(nameof(players));

        return players
            .Select(p => new
            {
                Player = p,
                Score = _matchResult.CalculateScore(p.Matches, p.IsDisqualified, p.PenaltyPoints)
            })
            .OrderByDescending(x => x.Score)
            .ThenBy(x => x.Player.Name, StringComparer.OrdinalIgnoreCase)
            .Select(x => x.Player)
            .ToList();
    }

    /// <summary>
    /// Returns the player who achieved the highest score, or <c>null</c> if no one
    /// has a strictly positive score (e.g. everyone disqualified).
    /// </summary>
    public Player? GetChampion(List<Player> players)
    {
        var ranking = GetRanking(players);
        if (!ranking.Any()) return null;

        var topPlayer = ranking.First();
        var topScore = _matchResult.CalculateScore(topPlayer.Matches, topPlayer.IsDisqualified, topPlayer.PenaltyPoints);
        return topScore > 0 ? topPlayer : null;
    }
}