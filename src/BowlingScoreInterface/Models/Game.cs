using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace BowlingScoreInterface.Models;

/// <summary>
/// Represents the state and behavior of a bowling game.
/// </summary>
public class Game
{
    /// <summary>
    /// The current round of the game.
    /// </summary>
    public int CurrentRound { get; set; }

    /// <summary>
    /// The list of players participating in the game.
    /// </summary>
    public List<Player> Players { get; set; }

    /// <summary>
    /// The total number of rounds in the game.
    /// </summary>
    public int NumberOfRounds { get; set; }

    /// <summary>
    /// The total number of pins set up at the start of each round.
    /// </summary>
    public int NumberOfPins { get; set; }

    /// <summary>
    /// The index of the current player who is rolling.
    /// </summary>
    public int actualplayer { get; set; }

    /// <summary>
    /// Indicates if the current roll is the first roll of the player's turn.
    /// </summary>
    public bool isRoll1 { get; set; }

    /// <summary>
    /// Constructor that initializes the game based on starting parameters.
    /// </summary>
    /// <param name="startingParameter">The initial configuration of the game derived from the home page.</param>
    public Game(Home startingParameter)
    {
        NumberOfRounds = startingParameter.NumberOfRounds + 1;
        Players = new List<Player>(startingParameter.Players.Count);
        for (int i = 0; i < startingParameter.Players.Count; i++)
        {
            Players.Add(new Player(startingParameter.Players[i], NumberOfRounds));
        }
        NumberOfPins = startingParameter.NumberOfPins;
        CurrentRound = 0;
        isRoll1 = true;
        actualplayer = 0;
    }

    /// <summary>
    /// Default constructor for the Game class.
    /// </summary>
    public Game() {}

    /// <summary>
    /// Constructor that initializes a new instance of the Game class with detailed game parameters.
    /// </summary>
    /// <param name="currentRound">The current round number.</param>
    /// <param name="players">The list of players in the game.</param>
    /// <param name="numberOfRounds">The total number of rounds in the game.</param>
    /// <param name="numberOfPins">The number of pins in a round.</param>
    /// <param name="actualplayer">The index of the current player.</param>
    /// <param name="isRoll1">Indicates if the current roll is the first roll of the player's turn.</param>
    public Game(int currentRound, List<Player> players, int numberOfRounds, int numberOfPins, int actualplayer, bool isRoll1)
    {
        CurrentRound = currentRound;
        Players = players;
        NumberOfRounds = numberOfRounds;
        NumberOfPins = numberOfPins;
        this.actualplayer = actualplayer;
        this.isRoll1 = isRoll1;
    }

    /// <summary>
    /// Updates the game state after a player rolls the ball.
    /// </summary>
    /// <param name="pinsScore">The number of pins knocked down in this roll.</param>
    /// <returns>The updated Game instance reflecting the new state after the roll.</returns>
    /// <remarks>
    /// The update handles the transition between rolls and players, as well as the transition to a new round.
    /// It also applies special rules for the final round of the game.
    /// </remarks>
    public Game Update(int pinsScore)
    {
        // Check if it's the last round and if the current player does not have a bonus roll.
        if (CurrentRound == NumberOfRounds - 1)
        {
            if (Players[actualplayer].BonusRoll == SpecialRoll.Default)
            {
                return this; // If so, no further action is taken.
            }
        }

        // Handle the logic for the first roll of a player's turn.
        if (isRoll1)
        {
            // Set the score for the first roll and update the round information.
            Players[actualplayer].Score_1 = pinsScore;
            Players[actualplayer].UpdateRounds(NumberOfPins, CurrentRound);

            // Check for spare in the previous round and reset the second roll score.
            if (Players[actualplayer].BonusRoll == SpecialRoll.Spare)
            {
                Players[actualplayer].Score_2 = 0;
                Players[actualplayer].Roll1(NumberOfPins, CurrentRound);

                // If it's the last player, increment the round. Otherwise, switch to the next player.
                if (actualplayer == Players.Count - 1)
                {
                    CurrentRound++;
                    return this;
                }
                else
                {
                    actualplayer = (actualplayer + 1) % Players.Count;
                    CurrentRound--;
                }
            }
            else if (pinsScore == NumberOfPins) // Check if the player scores a strike.
            {
                // Handle strike logic and set the bonus roll if necessary.
                Players[actualplayer].Roll1(NumberOfPins, CurrentRound);
                if (CurrentRound == NumberOfRounds - 2)
                {
                    Players[actualplayer].BonusRoll = SpecialRoll.Strike;
                }
                else if (CurrentRound == NumberOfRounds - 1)
                {
                    isRoll1 = false;
                }
                else
                {
                    isRoll1 = true;
                    actualplayer = (actualplayer + 1) % Players.Count;
                }
            }
            else
            {
                // If no strike, prepare for the second roll.
                isRoll1 = false;
            }
        }
        else
        {
            // Handle the logic for the second roll of a player's turn.
            Players[actualplayer].Score_2 = pinsScore;
            Players[actualplayer].Roll1(NumberOfPins, CurrentRound);

            // Check if the player scores a spare in the second-last round.
            if (CurrentRound == NumberOfRounds - 2 && Players[actualplayer].Score_1 + Players[actualplayer].Score_2 == NumberOfPins)
            {
                Players[actualplayer].BonusRoll = SpecialRoll.Spare;
                isRoll1 = true;
            }
            else
            {
                // Reset for the next player's turn.
                isRoll1 = true;
                actualplayer = (actualplayer + 1) % Players.Count;
            }
        }

        // Handle the transition to a new round in specific cases.
        if (CurrentRound == NumberOfRounds - 1 && isRoll1 && Players.Count != 1 && Players[^1].Rounds[^1].RoundScore == String.Empty)
        {
            CurrentRound--;
        }
        if ((actualplayer == 0 && isRoll1) || (CurrentRound == NumberOfRounds - 2 && Players[actualplayer].BonusRoll != SpecialRoll.Default && isRoll1))
        {
            CurrentRound++;
        }

        return this; // Return the updated game state.
    }
}