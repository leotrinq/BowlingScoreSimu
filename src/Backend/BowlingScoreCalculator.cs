using System;
using System.Collections.Generic;
class Program
{
    static void Main()
    {
        // GameManager gm1 = new GameManager(2,3);
        // Console.WriteLine($"New game started with {gm1.Players.Count} players and {gm1.NumberOfRounds} rounds.");
        // foreach (Player player in gm1.Players)
        // {
        //     Console.WriteLine(player.Name);
        // }
        // gm1.StartGame();
        
    }
}

class GameManager
{
    private int numberOfRounds;
    private List<List<int>> scores;
    public List<Player> Players {get;}
    //Getter - Setter for number of round
    
    public int NumberOfRounds
    {
        get => numberOfRounds; 
        set
        {
            if (value > 0)
            {
                return value;
            }
            else
            {
                throw new ArgumentException("Number of rounds must be greater than 0.", value)
            }
        }
    }

    //Constructeur
    public GameManager(int numberOfPlayers, int rounds = 10)
    {
        numberOfRounds = rounds;
        InitializePlayers(numberOfPlayers);
        InitializeScores(numberOfPlayers); 
        Console.WriteLine($"New game started with {numberOfPlayers} players and {numberOfRounds} rounds.");
    }

    private void InitializePlayers(int numberOfPlayers)
    {
        Players = new List<Player>();
        for (int i = 1; i <= numberOfPlayers; i++)
        {
            Players.Add(new Player($"Player {i}"));
        }
    }
    private void InitializeScores(int numberOfPlayers)
    {
        scores = new List<List<int>>();
        for (int i = 0; i < numberOfRounds; i++)
        {
            List<int> roundScores = new List<int>(numberOfPlayers);
            for (int j = 0; j < numberOfPlayers; j++)
            {
                roundScores.Add(0);
            }
            scores.Add(roundScores);
        }
    }


    public void StartGame()
    {
        Console.WriteLine("Game started!");
        for (int round = 1; round <= numberOfRounds; round++)
        {
            Console.WriteLine($"\nRound {round}:");

            for (int playerIndex = 0; playerIndex < Players.Count; playerIndex++)
            {
                Player currentPlayer = Players[playerIndex];
                Console.WriteLine($"{currentPlayer.Name}'s turn:");
                currentPlayer.Roll_1();
                currentPlayer.Roll_2();

                // Update the scores 2D list
                scores[round - 1][playerIndex] = currentPlayer.CurrentGame.CalculateRoundScore();
            }
            DisplayFinalScores();
        
        }

        Console.WriteLine("\nGame Over!");
    }

    public void DisplayScores()
    {
        Console.WriteLine("Round Scores:");
        for (int round = 0; round < numberOfRounds; round++)
        {
            Console.Write($"Round {round + 1}: ");
            for (int playerIndex = 0; playerIndex < Players.Count; playerIndex++)
            {
                Console.Write($"{Players[playerIndex].Name} - {scores[round][playerIndex]}   ");
            }
            Console.WriteLine();
        }
    }
    private void DisplayFinalScores()
    {
        Console.WriteLine("\nFinal Scores:");
        for (int playerIndex = 0; playerIndex < Players.Count; playerIndex++)
        {
            int totalScore = 0;
            for (int round = 0; round < numberOfRounds; round++)
            {
                totalScore += scores[round][playerIndex];
            }
            Console.WriteLine($"{Players[playerIndex].Name}: {totalScore}");
        }
    }
    public void DeletePlayer(Player player)
    {
        Players.Remove(player);
    }
    public void ResetGame()
    {
        foreach(Player player in Players)
        {
            Players.Remove(player);
        }
    }
    public void EndGame()
    {
        DisplayFinalScores();

    }

}

class Player
{
    public string Name { get; private set; }
    public BowlingGame CurrentGame { get; private set; }
    int score_1, score_2;

    public Player(string name)
    {
        Name = name;
        CurrentGame = new BowlingGame();
    }

    public void Roll_1()
    {
        Console.WriteLine("Roll 1: ");
        while (!int.TryParse(Console.ReadLine(), out score_1) || score_1 < 0 || score_1 > 10)
        {
            Console.WriteLine("Invalid input. Please enter a score between 0 and 10.");
            Console.Write("Enter the score for the first roll: ");

        }
        CurrentGame.Roll_1(score_1);
    }
    public void Roll_2()
    {
        if(score_1<10)
        {
        Console.WriteLine("Roll 2: ");
        while (!int.TryParse(Console.ReadLine(), out score_2) || score_2 < 0 || score_2+score_1 > 10)
        {
            Console.WriteLine("Invalid input. Please enter a score between 0 and 10.");
            Console.Write("Enter the score for the second roll: ");
        }

        CurrentGame.Roll_2(score_2);
        }
    }
    void RenamePlayer(string newName)
    {
        Name = newName;
    }
}

class BowlingGame
{
    private int pins_1, pins_2;

    public void Roll_1(int pins_1)
    {
        this.pins_1 = pins_1;
    }

    public void Roll_2(int pins_2)
    {
        this.pins_2 = pins_2;
    }

    public int CalculateRoundScore()
    {
        // Implement the logic to calculate the current round score
        return pins_1+pins_2;
    }
}
