namespace BowlingScoreInterface.Models;

/// <summary>
/// Represents a single round in a bowling game, holding the score details for a player's turn.
/// </summary>
public class Round
{
    /// <summary>
    /// The score for the first roll of the round. Can be a number or a symbol representing a strike ('X').
    /// </summary>
    public string FirstRound { get; set; }

    /// <summary>
    /// The score for the second roll of the round, if applicable. Can be a number, a spare ('/'), or empty if not used.
    /// </summary>
    public string SecondRound { get; set; }

    /// <summary>
    /// The cumulative score up to this round. It may not be filled until a later frame due to strikes and spares requiring subsequent rolls' results.
    /// </summary>
    public string RoundScore { get; set; }

    /// <summary>
    /// Initializes a new instance of the Round class with specified scores for both rolls and the round's cumulative score.
    /// </summary>
    /// <param name="firstRound">The score for the first roll of the round.</param>
    /// <param name="secondRound">The score for the second roll of the round.</param>
    /// <param name="roundScore">The cumulative score up to this round.</param>
    public Round(string firstRound, string secondRound, string roundScore)
    {
        FirstRound = firstRound;
        SecondRound = secondRound;
        RoundScore = roundScore;
    }

    /// <summary>
    /// Initializes a new instance of the Round class with empty scores, to be filled during gameplay.
    /// </summary>
    public Round()
    {
        FirstRound = string.Empty;
        SecondRound = string.Empty;
        RoundScore = string.Empty;
    }
}