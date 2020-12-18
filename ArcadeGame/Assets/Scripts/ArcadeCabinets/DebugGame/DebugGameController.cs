using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugGameController : ArcadeGameController
{
    /** The text where the user enters the number of points to receive. */
    public Text inputText;
    /** The text to display the number of tickets the user has. */
    public Text ticketsText;

    public override BigInteger initalScore
    {
        get { return 0; }
    }

    /**
     * Called on the first frame of the scene execution.
     */
    public override void Start()
    {
        base.Start();

        ticketsText.text = "Current Tickets: " + ArcadeManager.bigIntToString(arcadeStatus.debugStatus.tickets.value);
    }

    /**
     * Used to end the game.
     */
    public override void endGame()
    {
        BigInteger pointsToAdd = score;

        // deal with upgrades
        pointsToAdd *= (BigInteger.Pow(2, arcadeStatus.debugStatus.doublePoints.currentLevel));
        pointsToAdd *= (BigInteger.Pow(2, arcadeStatus.prizeStatus.doublePoints.currentLevel));

        if (pointsToAdd > 0)
        {
            arcadeStatus.debugStatus.tickets.value += pointsToAdd;
        }

        //temporary functionality
        base.endGame();
        SceneManager.LoadScene("MenuScene");
    }

    // Game specific methods
    
    /**
     * Used to update the points from the input field.
     */
    public void gainScore()
    {
        string inputString = inputText.text;
        long pointsToAdd = long.Parse(inputString);

        updateScore(score + pointsToAdd);
    }
}
