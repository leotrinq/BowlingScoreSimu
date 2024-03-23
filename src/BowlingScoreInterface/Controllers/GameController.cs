using BowlingScoreInterface.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BowlingScoreInterface.Controllers
{
    public class GameController : Controller
    {

        /// <summary>
        /// Method to return the view of the game page.
        /// </summary>
        /// <returns>The main view.</returns>
        public IActionResult Index(string serializedGame)
        {
            Game? game = new();
            try
            {
                game = JsonSerializer.Deserialize<Game>(serializedGame);
            }
            catch (JsonException e)
            {
                throw new JsonException("Error while deserializing the game", e);
            }
            return View(game);
        }

        /// <summary>
        /// Method to add update the game with the number of pins taken.
        /// </summary>
        /// <param name="serializedGame">the serialized Model</param>
        /// <param name="pinsScore"> the amount of pins taken</param>
        /// <returns>Return a view with the updated model or the leaderboard view</returns>
        /// <exception cref="JsonException">If the deserialization had a problem</exception>
        public IActionResult PinsTaken(string serializedGame, int pinsScore)
        {
            Game? game = new();
            try
            {
                game = JsonSerializer.Deserialize<Game>(serializedGame);
            }
            catch (JsonException e)
            {
                throw new JsonException("Error while deserializing the game", e);
            }

            game = game.Update(pinsScore);

            // Check all cases for the end of the game
            if ((game.Players[^1].BonusRoll == SpecialRoll.Default && game.CurrentRound == game.NumberOfRounds - 1 && game.Players[^1].Rounds[game.NumberOfRounds-2].RoundScore != String.Empty) ||((game.Players[^1].BonusRoll != SpecialRoll.Default && game.CurrentRound == game.NumberOfRounds)))
            {

                string[] playerNames = new string[game.Players.Count];
                int[] playerScores = new int[game.Players.Count];

                for (int i = 0; i < game.Players.Count; i++)
                {
                    playerNames[i] = game.Players[i].Name;
                    playerScores[i] = game.Players[i].TotalScore;
                }

                for (int i = 0; i < playerScores.Length; i++)
                {
                    for (int j = i + 1; j < playerScores.Length; j++)
                    {
                        if (playerScores[i] < playerScores[j])
                        {
                            // Swap scores
                            int tempScore = playerScores[i];
                            playerScores[i] = playerScores[j];
                            playerScores[j] = tempScore;

                            // Swap names
                            string tempName = playerNames[i];
                            playerNames[i] = playerNames[j];
                            playerNames[j] = tempName;
                        }
                    }
                }
                return RedirectToAction(nameof(Index), nameof(Leaderboard), new { names = playerNames, scores = playerScores });
            }

            return View(nameof(Index), game);
        }
    }
}
