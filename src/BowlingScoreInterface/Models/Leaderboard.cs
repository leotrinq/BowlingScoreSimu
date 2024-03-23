namespace BowlingScoreInterface.Models;

/// <summary>
/// Represents the model for the leaderboard page in a bowling game.
/// This model holds an array of player names and their scores, used for ranking and display purposes.
/// </summary>
public class Leaderboard
{
    /// <summary>
    /// An array of tuples containing player names and their corresponding scores.
    /// This array is used to display the leaderboard rankings of the players.
    /// Each tuple consists of a player's name and their total score in the game.
    /// </summary>
    public (string Name, int Score)[] PlayerLeaderboard { get; set; }

    /// <summary>
    /// Initializes a new instance of the Leaderboard class with a specific array of player names and scores.
    /// </summary>
    /// <param name="playerLeaderboard">An array of tuples with player names and scores.</param>
    public Leaderboard((string, int)[] playerLeaderboard)
    {
        PlayerLeaderboard = playerLeaderboard;
    }
}