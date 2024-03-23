using BowlingScoreInterface.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace BowlingScoreInterface.Controllers
{
    public class LeaderboardController : Controller
    {
        /// <summary>
        /// Method to return the view of the Leaderboard page.
        /// </summary>
        /// <returns>The main view.</returns>
        public IActionResult Index(string[] names,int[] scores)
        {
            (string, int)[] playerLeaderboard = new (string, int)[names.Length];
            for (int i = 0; i < names.Length; i++)
            {
                playerLeaderboard[i] = (names[i], scores[i]);
            }
            return View(new Leaderboard(playerLeaderboard));
        }
        /// <summary>
        /// Restart the game.
        /// </summary>
        /// <returns>Return the Initial home view</returns>
        public IActionResult Restart()
        {
            return RedirectToAction("Index", "Home");
        }
    }
}
