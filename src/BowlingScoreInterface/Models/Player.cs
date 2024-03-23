using System.Diagnostics;
using System.Text.Json.Serialization;

namespace BowlingScoreInterface.Models;

/// <summary>
/// Represents a player in the bowling game.
/// </summary>
public class Player
{
    /// <summary>
    /// The name of the player.
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// A list that stores tuples of each roll's scores and special roll indicators.
    /// </summary>
    public List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> Tab2DScores { get; internal set; }

    /// <summary>
    /// The list of rounds with their respective scores and special roll indicators.
    /// </summary>
    public List<Round> Rounds { get; private set; }

    /// <summary>
    /// Score for the first roll in a frame.
    /// </summary>
    public int Score_1 { get; set; }

    /// <summary>
    /// Score for the second roll in a frame, if applicable.
    /// </summary>
    public int Score_2 { get; set; }

    /// <summary>
    /// Total score accumulated by the player.
    /// </summary>
    public int TotalScore { get; set; }

    /// <summary>
    /// The type of special roll (strike, spare, etc.) achieved by the player.
    /// </summary>
    public SpecialRoll BonusRoll { get; set; }

    /// <summary>
    /// Boolean for a special case.
    /// </summary>
    public bool Problem { get; set; } = true;

    /// <summary>
    /// Constructor for initializing a player with a name and the number of rounds.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="NumberOfRounds">The number of rounds in the game.</param>
    public Player(string name, int NumberOfRounds)
    {
        Name = name;
        Score_1 = 0;
        Score_2 = 0;
        TotalScore = 0;
        Tab2DScores = new List<(int Roll1, int? Roll2, SpecialRoll specialRoll)>();
        Rounds = new List<Round> (NumberOfRounds);
        for (int i = 0; i < NumberOfRounds; i++)
        {
            Rounds.Add(new());
        }
    }
    /// <summary>
    /// Constructor for initializing a player with detailed scores and rolls.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="tab2DScores">A list of tuples representing the player's scores and special rolls.</param>
    /// <param name="rounds">A list of rounds for the player.</param>
    /// <param name="Score_1">Score of the first roll.</param>
    /// <param name="Score_2">Score of the second roll.</param>
    /// <param name="totalScore">Total score of the player.</param>
    public Player(string name, List<(int Roll1, int? Roll2, SpecialRoll specialRoll)> tab2DScores, List<Round> rounds, int Score_1, int Score_2, int totalScore)
    {
        Name = name;
        Tab2DScores = tab2DScores;
        Rounds = rounds;
        this.Score_1 = Score_1;
        this.Score_2 = Score_2;
        TotalScore = totalScore;
    }

    /// <summary>
    /// Constructor used for deserialization from JSON.
    /// </summary>
    /// <param name="name">The name of the player.</param>
    /// <param name="rounds">The rounds associated with the player.</param>
    [JsonConstructor]
    public Player(string name, List<Round> rounds)
    {
        Name = name;
        Rounds = rounds;

        Tab2DScores = new List<(int Roll1, int? Roll2, SpecialRoll specialRoll)>();
        Score_1 = 0;
        Score_2 = 0; 
        TotalScore = 0;
    }

    /// <summary>
    /// Updates the round's information for the player based on the current roll scores.
    /// </summary>
    /// <param name="NumberOfPins">The total number of pins for a strike.</param>
    /// <param name="CurrentRound">The current round number.</param>
    public void UpdateRounds(int NumberOfPins, int CurrentRound)
    {
        if (Score_1 < NumberOfPins)
        {
            Rounds[CurrentRound] = new() {FirstRound = Score_1.ToString(), SecondRound = String.Empty, RoundScore = String.Empty};
        }
        else if (Score_1 == NumberOfPins)
        {
            Rounds[CurrentRound] = new() { FirstRound = "X", SecondRound = String.Empty, RoundScore = String.Empty };
        }
        else
        {
            throw new Exception("Problem! Incorrect score!");
        }
    }
    /// <summary>
    /// Handles the logic for the first roll in a round, including scoring and special roll assessment.
    /// </summary>
    /// <param name="NumberOfPins">The total number of pins for a strike.</param>
    /// <param name="CurrentRound">The current round number.</param>
    public void Roll1(int NumberOfPins, int CurrentRound)
    {
        // Check if the score of the first roll is less than the total number of pins (not a strike).
        if (Score_1 < NumberOfPins)
        {
            // If so, proceed to the second roll.
            Roll2(NumberOfPins, CurrentRound);
        }
        else if (Score_1 == NumberOfPins) // Check if the player scores a strike.
        {
            // Add the strike to the player's score history.
            Tab2DScores.Add((Score_1, null, SpecialRoll.Strike));

            // Special handling for the last round.
            if (CurrentRound != Rounds.Count - 1)
            {
                // Reset the second roll score as the player won't roll again in this round.
                Score_2 = 0;
            }
            else if (Rounds[CurrentRound].RoundScore != String.Empty)
            {
                // If the round score is already recorded, reset the first roll score.
                Score_1 = 0;
            }

            // Update the total score and calculate the round score.
            TotalScore += Score_1 + Score_2;
            CalculateRoundScore(NumberOfPins, CurrentRound);
        }
        else
        {
            // Throw an exception if the score is incorrect.
            throw new Exception("Problem! Incorrect score!");
        }
    }

    /// <summary>
    /// Handles the logic for the second roll in a round, including scoring and special roll assessment.
    /// </summary>
    /// <param name="NumberOfPins">The total number of pins for a strike or spare.</param>
    /// <param name="CurrentRound">The current round number.</param>
    public void Roll2(int NumberOfPins, int CurrentRound)
    {
        // Update the total score by adding the scores from the first and second rolls.
        TotalScore += Score_1 + Score_2;

        // Check if the sum of Score_1 and Score_2 equals the total number of pins, indicating a spare.
        if (Score_1 + Score_2 == NumberOfPins)
        {
            // Add the spare to the player's score history.
            Tab2DScores.Add((Score_1, Score_2, SpecialRoll.Spare));

            // Update the current round's data to reflect the spare.
            Rounds[CurrentRound] = new() { FirstRound = Score_1.ToString(), SecondRound = "/", RoundScore = String.Empty };
        }
        else if (Score_1 + Score_2 < NumberOfPins) // Check if the total score is less than the number of pins (no strike or spare).
        {
            // Add a default roll (no strike or spare) to the score history.
            Tab2DScores.Add((Score_1, Score_2, SpecialRoll.Default));

            // Update the round's data with the scores and the total score.
            Rounds[CurrentRound] = new() { FirstRound = Score_1.ToString(), SecondRound = Score_2.ToString(), RoundScore = TotalScore.ToString() };
        }
        else
        {
            // Throw an exception if the total of Score_1 and Score_2 is greater than the number of pins, which should not happen.
            throw new Exception("Problem! Incorrect score!");
        }

        // Calculate the round score, considering strikes and spares from previous rounds.
        CalculateRoundScore(NumberOfPins, CurrentRound);
    }

    /// <summary>
    /// Calculates the round score for the player, taking into account the special rolls like strikes and spares from previous rounds.
    /// </summary>
    /// <param name="NumberOfPins">The total number of pins for a strike.</param>
    /// <param name="CurrentRound">The current round number.</param>
    public void CalculateRoundScore(int NumberOfPins, int CurrentRound)
    {
        // Check if the current round is not the first one.
        if (CurrentRound > 0)
        {
            // Check if the previous round was a strike.
            if (Rounds[CurrentRound - 1].FirstRound == "X")
            {
                // Handle the scenario of two consecutive strikes.
                if (CurrentRound > 1 && Rounds[CurrentRound - 2].FirstRound == "X")
                {
                    // Special handling for scoring when there are two consecutive strikes.
                    if (Problem) // 'Problem' indicates a special condition needing attention.
                    {
                        if (CurrentRound == Rounds.Count - 1)
                        {
                            Problem = false;
                        }
                        // Update the score of the round before the last strike.
                        Rounds[CurrentRound - 2].RoundScore = (int.Parse(Rounds[CurrentRound - 2].RoundScore) + Score_1).ToString();
                        TotalScore += Score_1;
                    }
                }
                // Update the score for the last strike.
                Rounds[CurrentRound - 1].RoundScore = TotalScore.ToString();
                Rounds[CurrentRound].RoundScore = (TotalScore + Score_1 + Score_2).ToString();
                TotalScore += Score_1 + Score_2;
            }
            else if (Rounds[CurrentRound - 1].SecondRound == "/") // Check if the previous round was a spare.
            {
                // Update the score of the spare round.
                Rounds[CurrentRound - 1].RoundScore = (TotalScore - Score_2).ToString();
                TotalScore += Score_1;
                Rounds[CurrentRound].RoundScore = TotalScore.ToString();
            }
        }

        // Special handling for the last round.
        if (CurrentRound == Rounds.Count - 1)
        {
            // Adjust the total score for the last round.
            TotalScore -= (Score_1 + Score_2);
            Rounds[CurrentRound].RoundScore = (int.Parse(Rounds[CurrentRound].RoundScore) - (Score_1 + Score_2)).ToString();
        }
    }

}