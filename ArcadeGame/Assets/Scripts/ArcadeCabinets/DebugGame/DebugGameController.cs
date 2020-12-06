using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugGameController : MonoBehaviour
{
    private GameController gc;

    public Text inputText;
    public Text pointsText;
    public Text ticketsText;

    public long points;


    // Start is called before the first frame update
    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        points = 0;
        pointsText.text = "Current Points: " + points;
        ticketsText.text = "Current Tickets: " + gc.getDebugTickets();
    }

    public void updatePoints()
    {
        string inputString = inputText.text;
        long pointsToAdd = long.Parse(inputString);

        points += pointsToAdd;

        pointsText.text = "Current Points: " + points;
    }

    public void endGame()
    {
        if (points > 0)
        {
            gc.incrementDebugTickets(points);
        }

        //temporary functionality
        SceneManager.LoadScene("DebugCabinetTitleScene");
    }
}
