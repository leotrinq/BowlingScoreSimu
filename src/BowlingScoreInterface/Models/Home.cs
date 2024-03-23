using System.Numerics;

namespace BowlingScoreInterface.Models;

/// <summary>
/// Class to represent the model of the home page.
/// </summary>
public class Home
{
    /// <summary>
    /// amount of pins.
    /// </summary>
    public int NumberOfPins { get; set; } = 10;

    /// <summary>
    /// List of players.
    /// </summary>
    public List<string> Players { get; set; } = [];

    /// <summary>
    /// amount of rounds.
    /// </summary>
    public int NumberOfRounds { get; set; } = 10;    

    /// <summary>
    /// Limit of players.
    /// </summary>
    public int MaxPlayers { get; } = 10;


}
