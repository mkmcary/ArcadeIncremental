using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class QMGGameController : ArcadeGameController
{
    [Header("Each of the Tasks Possible")]
    public List<QMGButtonTask> buttonTasks;

    [Header("UI Elements")]
    public Text timerText;

    [Header("PopUp")]
    public GameObject popUp;
    public Text popUpText;
    public Text gainText;

    private QMGButtonTask activeTask;
    private float timer;

    private bool isGameRunning;

    public override BigInteger InitalScore => 0;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        popUp.SetActive(false);

        ChooseTask();

        // apply base timer upgrades here
        timer = 15f;

        isGameRunning = true;
    }

    public void ChooseTask()
    {
        Debug.Log("Choosing new task.");

        if(activeTask != null)
            activeTask.Deactivate();

        int nextTask = Random.Range(0, buttonTasks.Count - 1);
        //while (activeGame != null && buttonGames[nextGame] != activeGame) {
        //nextGame = Random.Range(0, buttonGames.Count);
        //}

        activeTask = buttonTasks[nextTask];
        activeTask.Activate();

        Debug.Log("NextTask was: " + nextTask + " and active task is: " + activeTask.gameObject);
    }

    public void IncrementScore()
    {
        // here is where we can apply upgrades relating to score
        // ...

        score++;
        UpdateScore();
    }

    // Used to Handle Game Timer
    void FixedUpdate()
    {
        if (isGameRunning)
        {
            timer -= Time.deltaTime;
            timerText.text = timer.ToString("N1") + " s";

            if (timer <= 0)
            {
                EndGame();
            }
        }
        
    }

    public override void EndGame()
    {
        isGameRunning = false;

        // Calcluate new data
        BigInteger tickets = score;
        arcadeStatus.QMGStatus.CumulativeScore += score;
        arcadeStatus.QMGStatus.HighScore = BigInteger.Max(arcadeStatus.QMGStatus.HighScore, score);
        arcadeStatus.QMGStatus.Tickets += tickets;

        // Update popup text and display the popup
        popUpText.text = "Your Score: " + score
            + "\nCumulative Score: " + GameOperations.BigIntToString(arcadeStatus.QMGStatus.CumulativeScore)
            + "\nTicket Count: " + GameOperations.BigIntToString(arcadeStatus.QMGStatus.Tickets);
        gainText.text = "(+" + GameOperations.BigIntToString(tickets) + ")";
        popUp.SetActive(true);

        // Write to file
        base.EndGame();
    }

    public override void UpdateScore()
    {
        scoreText.text = "Score: " + GameOperations.BigIntToString(score);
    }
}
