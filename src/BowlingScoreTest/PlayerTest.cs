using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Threading.Channels;
using BowlingScoreInterface.Models;


namespace BowlingScoreTest;
//score_1, score2, 10 remplace par nombre de tours, update rounds (parametres), pareil pr roll1
[TestClass]
public class PlayerTest
{
    [TestMethod]
    public void canTest()
    {
        Assert.AreEqual(1, 1);
    }
    [TestMethod]
    public void TestInitialScore()
    {
        Player player = new Player("name", 10);

        Assert.AreEqual(0, player.TotalScore, "Le score initial d'un nouveau joueur devrait être 0.");
    }
    [TestMethod]
    public void TestNameAssignment()
    {
        string expectedName = "name";
        Player player = new Player(expectedName, 10);

        Assert.AreEqual(expectedName, player.Name, "Le nom du joueur n'est pas correctement assigné.");
    }
    [TestMethod]
    public void TestSingleRollScoreCalculation()
    {
        Player player = new Player("name", 10);
        player.Score_1 = 5;
        player.Score_2 = 0;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);
        Assert.AreEqual(5, player.TotalScore, "Le score après un seul lancer n'est pas correct.");
    }
    [TestMethod]
    public void TestSpareScoreCalculation()
    {
        Player player = new Player("name", 10);
        player.Score_1 = 5;
        player.Score_2 = 5;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);
        Assert.AreEqual(10, player.TotalScore, "Le score après un spare n'est pas correct.");
    }
    [TestMethod]
    public void TestStrikeScoreCalculation()
    {
        Player player = new Player("name", 10);
        player.Score_1 = 10;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);
        Assert.AreEqual(10, player.TotalScore, "Le score après un strike n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame()
    {
        Player player = new Player("name", 10);
        int NumberOfRounds = 10;
        for (int i = 0; i < NumberOfRounds; i++)
        {
            player.Score_1 = 4;
            player.Score_2 = 5;
            player.UpdateRounds(NumberOfRounds, i);
            player.Roll1(NumberOfRounds, i);
        }
        
        int expectedTotalScore = 81;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGameWithSpare2()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 7;
                player.Score_2 = 3;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
        }

        int expectedTotalScore = 87;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpare3()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 0;
                player.Score_2 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
        }

        int expectedTotalScore = 87;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike1()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
        }

        int expectedTotalScore = 91;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike2()
    {
        Player player = new Player("name", 10);
        for (int i = 0; i < 10; i++)
        {
            if (i == 0)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);

            }
            else if (i == 1)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);

            }
            else
            {
                player.Score_1 = 5;
                player.Score_2 = 4;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);

            }
        }
        int expectedTotalScore = 107;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike3()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 5)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 6)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 7;
                player.Score_2 = 2;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
        }

        int expectedTotalScore = 109;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrike4()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 5)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 6)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 7)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 7;
                player.Score_2 = 2;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
        }

        int expectedTotalScore = 130;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrikeAndSpare()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            if (i == 5)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 6)
            {
                player.Score_1 = 5;
                player.Score_2 = 5;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else if (i == 7)
            {
                player.Score_1 = 10;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
            else
            {
                player.Score_1 = 7;
                player.Score_2 = 2;
                player.UpdateRounds(10, i);
                player.Roll1(10, i);
            }
        }

        int expectedTotalScore = 113;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithFullStrike()
    {
        Player player = new Player("name", 10);
        for (int i = 0; i < 10; i++)
        {
            player.Score_1 = 10;
            player.UpdateRounds(10, i);
            player.Roll1(10, i);
        }

        int expectedTotalScore = 260;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame2()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 7;
        player.Score_2 = 3;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 5;
        player.Score_2 = 4;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 1;
        player.Score_2 = 9;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);


        int expectedTotalScore = 34;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame3()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 4;
        player.Score_2 = 5;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 3;
        player.Score_2 = 6;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 2;
        player.Score_2 = 7;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 6;
        player.Score_2 = 3;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 5;
        player.Score_2 = 4;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);

        player.Score_1 = 0;
        player.Score_2 = 9;
        player.UpdateRounds(10, 7);
        player.Roll1(10, 7);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 8);
        player.Roll1(10, 8);

        player.Score_1 = 1;
        player.Score_2 = 8;
        player.UpdateRounds(10, 9);
        player.Roll1(10, 9);


        int expectedTotalScore = 81;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]

    public void TestCompleteGame4()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 6;
        player.Score_2 = 3;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 5;
        player.Score_2 = 4;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 0;
        player.Score_2 = 9;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 3;
        player.Score_2 = 6;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);

        player.Score_1 = 4;
        player.Score_2 = 5;
        player.UpdateRounds(10, 7);
        player.Roll1(10, 7);

        int expectedTotalScore = 72;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]

    public void TestCompleteGame5()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 4;
        player.Score_2 = 5;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 3;
        player.Score_2 = 6;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 0;
        player.Score_2 = 8;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);


        int expectedTotalScore = 44;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie incomplète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame6()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 2;
        player.Score_2 = 3;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 1;
        player.Score_2 = 4;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 0;
        player.Score_2 = 5;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 3;
        player.Score_2 = 2;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 2;
        player.Score_2 = 1;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 4;
        player.Score_2 = 0;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 1;
        player.Score_2 = 3;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);


        int expectedTotalScore = 31;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie incomplète de 7 tours n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame7()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 1;
        player.Score_2 = 2;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 2;
        player.Score_2 = 3;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 3;
        player.Score_2 = 4;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 4;
        player.Score_2 = 1;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 2;
        player.Score_2 = 2;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 3;
        player.Score_2 = 1;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 0;
        player.Score_2 = 5;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);

        int expectedTotalScore = 33;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie incomplète de 7 tours n'est pas correct.");
    }


    [TestMethod]
    public void TestCompleteGame8()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 4);
        player.Roll1(10, 4);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 5);
        player.Roll1(10, 5);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 6);
        player.Roll1(10, 6);

        player.Score_1 = 8;
        player.Score_2 = 1;
        player.UpdateRounds(10, 7);
        player.Roll1(10, 7);

        player.Score_1 = 7;
        player.Score_2 = 2;
        player.UpdateRounds(10, 8);
        player.Roll1(10, 8);

        player.Score_1 = 9;
        player.Score_2 = 0;
        player.UpdateRounds(10, 9);
        player.Roll1(10, 9);

        int expectedTotalScore = 81;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }


    [TestMethod]
    public void TestCompleteGame9()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 4;
        player.Score_2 = 2;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 0;
        player.Score_2 = 0;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 8;
        player.Score_2 = 0;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 4;
        player.Score_2 = 2;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        int expectedTotalScore = 20;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGame10()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 8;
        player.Score_2 = 0;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        player.Score_1 = 1;
        player.Score_2 = 6;
        player.UpdateRounds(10, 1);
        player.Roll1(10, 1);

        player.Score_1 = 5;
        player.Score_2 = 2;
        player.UpdateRounds(10, 2);
        player.Roll1(10, 2);

        player.Score_1 = 3;
        player.Score_2 = 0;
        player.UpdateRounds(10, 3);
        player.Roll1(10, 3);

        int expectedTotalScore = 25;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame11()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 8;
        player.Score_2 = 1;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        Assert.AreEqual("8", player.Rounds[0].FirstRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual("1", player.Rounds[0].SecondRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual("9", player.Rounds[0].RoundScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame12()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 8;
        player.Score_2 = 2;

        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        Assert.AreEqual("8", player.Rounds[0].FirstRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual("/", player.Rounds[0].SecondRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].RoundScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGame13()
    {
        
        Player player = new Player("name", 10);

        player.Score_1 = 10;
        player.UpdateRounds(10, 0);
        player.Roll1(10, 0);

        Assert.AreEqual("X", player.Rounds[0].FirstRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].SecondRound, "Le score total après une partie complète n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].RoundScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestWithSixPins()
    {
        Player player = new Player("name", 10);

        player.Score_1 = 6;
        player.UpdateRounds(6, 0);
        player.Roll1(6, 0);

        int expectedTotalScore = 6;
        Assert.AreEqual("X", player.Rounds[0].FirstRound, "Le score total après un lancé n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].SecondRound, "Le remplissage de rounds n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].RoundScore, "Le remplissage de rounds n'est pas correct.");
        Assert.AreEqual(player.TotalScore, expectedTotalScore, "Le calcul du score n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSixPins()
    {
        Player player = new Player("name", 10);

        for (int i = 0; i < 10; i++)
        {
            player.Score_1 = 2;
            player.Score_2 = 3;
            player.UpdateRounds(6, i);
            player.Roll1(6, i);
        }

        int expectedTotalScore = 45;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpareAndSixPins()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;
        for (int i = 0; i < 10; i++)
        {
            if (i == 3)
            {
                player.Score_1 = 3;
                player.Score_2 = 3;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
            else
            {
                player.Score_1 = 2;
                player.Score_2 = 3;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
        }

        int expectedTotalScore = 48;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }

    [TestMethod]
    public void TestCompleteGameWithSpareAndSixPins2()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;
        for (int i = 0; i < 10; i++)
        {
            if (i == 0 || i == 4)
            {
                player.Score_1 = 3;
                player.Score_2 = 3;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
            else
            {
                player.Score_1 = 2;
                player.Score_2 = 3;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
        }

        int expectedTotalScore = 51;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithSpareAndSixPins3()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;
        for (int i = 0; i < 10; i++)
        {
            if (i == 3 || i == 4)
            {
                player.Score_1 = 3;
                player.Score_2 = 3;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
            else
            {
                player.Score_1 = 2;
                player.Score_2 = 3;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
        }

        int expectedTotalScore = 52;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithFullSpareAndSixPins()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;
        for (int i = 0; i < 10; i++)
        {
            player.Score_1 = 3;
            player.Score_2 = 3;
            player.UpdateRounds(NumberOfPins, i);
            player.Roll1(NumberOfPins, i);
        }

        int expectedTotalScore = 81;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrikeAndSixPins()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;
        for (int i = 0; i < 10; i++)
        {
            if (i == 0 || i == 3)
            {
                player.Score_1 = 6;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
            else
            {
                player.Score_1 = 2;
                player.Score_2 = 3;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
        }

        int expectedTotalScore = 57;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithStrikeAndSixPins2()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;
        for (int i = 0; i < 10; i++)
        {
            if (i == 5 || i == 6)
            {
                player.Score_1 = 6;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
            else
            {
                player.Score_1 = 2;
                player.Score_2 = 3;
                player.UpdateRounds(NumberOfPins, i);
                player.Roll1(NumberOfPins, i);
            }
        }

        int expectedTotalScore = 60;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    [TestMethod]
    public void TestCompleteGameWithFullStrikeAndSixPins()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;
        for (int i = 0; i < 10; i++)
        {
            player.Score_1 = NumberOfPins;
            player.UpdateRounds(NumberOfPins, i);
            player.Roll1(NumberOfPins, i);
        }

        int expectedTotalScore = 156;
        Assert.AreEqual(expectedTotalScore, player.TotalScore, "Le score total après une partie complète n'est pas correct.");
    }
    // ----------------------------------------------------------------------------------------------

    [TestMethod]
    public void TestDisplayWithSixPins()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;

        player.Score_1 = NumberOfPins;
        player.UpdateRounds(NumberOfPins, 0);
        player.Roll1(NumberOfPins, 0);

        Assert.AreEqual("X", player.Rounds[0].FirstRound, "Le score total après un lancé n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].SecondRound, "Le remplissage de rounds n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].RoundScore, "Le remplissage de rounds n'est pas correct.");

        player.Score_1 = 2;
        player.Score_2 = 3;
        player.UpdateRounds(NumberOfPins, 1);
        player.Roll1(NumberOfPins, 1);

        Assert.AreEqual("11", player.Rounds[0].RoundScore, "Le remplissage de rounds n'est pas correct.");
        Assert.AreEqual("2", player.Rounds[1].FirstRound, "Le score total après un lancé n'est pas correct.");
        Assert.AreEqual("3", player.Rounds[1].SecondRound, "Le remplissage de rounds n'est pas correct.");
        Assert.AreEqual("16", player.Rounds[1].RoundScore, "Le remplissage de rounds n'est pas correct.");

    }
    [TestMethod]
    public void TestDisplayWithSixPins2()
    {
        Player player = new Player("name", 10);
        int NumberOfPins = 6;

        player.Score_1 = NumberOfPins;
        player.UpdateRounds(NumberOfPins, 0);
        player.Roll1(NumberOfPins, 0);

        Assert.AreEqual("X", player.Rounds[0].FirstRound, "Le score total après un lancé n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].SecondRound, "Le remplissage de rounds n'est pas correct.");
        Assert.AreEqual(String.Empty, player.Rounds[0].RoundScore, "Le remplissage de rounds n'est pas correct.");

        player.Score_1 = NumberOfPins;
        player.UpdateRounds(NumberOfPins, 1);
        player.Roll1(NumberOfPins, 1);

        Assert.AreEqual("12", player.Rounds[0].RoundScore, "Le remplissage de rounds n'est pas correct.");
        Assert.AreEqual("X", player.Rounds[1].FirstRound, "Le score total après un lancé n'est pas correct.");
        Assert.AreEqual(string.Empty, player.Rounds[1].SecondRound, "Le remplissage de rounds n'est pas correct.");
        Assert.AreEqual("18", player.Rounds[1].RoundScore, "Le remplissage de rounds n'est pas correct.");

    }
}