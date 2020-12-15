using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugGameController : MonoBehaviour
{
    private ArcadeStatus arcadeStatus;

    /** The text where the user enters the number of points to receive. */
    public Text inputText;
    /** The text to display the number of active points the user has. */
    public Text pointsText;
    /** The text to display the number of tickets the user has. */
    public Text ticketsText;

    /** The active number of points for this game session. */
    public long points;


    /**
     * Called on the first frame of the scene execution.
     */
    void Start()
    {
        arcadeStatus = ArcadeManager.readArcadeStatus();

        points = 0;
        pointsText.text = "Current Points: " + ArcadeManager.convertToScientific(points);
        ticketsText.text = "Current Tickets: " + ArcadeManager.convertToScientific(arcadeStatus.debugStatus.tickets);

        //StartCoroutine(initialize());
    }

    /**
     * Used to update the points from the input field.
     */
    public void updatePoints()
    {
        string inputString = inputText.text;
        long pointsToAdd = long.Parse(inputString);

        points += pointsToAdd;

        pointsText.text = "Current Points: " + ArcadeManager.convertToScientific(points);
    }



    /**
     * Used to end the game.
     */
    public void endGame()
    {
        long pointsToAdd = points;

        // deal with upgrades
        pointsToAdd *= ((long) Mathf.Pow(2, arcadeStatus.debugStatus.doublePoints.currentLevel));
        pointsToAdd *= ((long) Mathf.Pow(2, arcadeStatus.prizeStatus.doublePoints.currentLevel));

        if (pointsToAdd > 0)
        {
            arcadeStatus.debugStatus.tickets += pointsToAdd;
        }

        //temporary functionality
        ArcadeManager.writeArcadeStatus(arcadeStatus);
        SceneManager.LoadScene("DebugTitleScene");
    }

    public void OnApplicationQuit()
    {
        ArcadeManager.writeArcadeStatus(arcadeStatus);
    }
}
