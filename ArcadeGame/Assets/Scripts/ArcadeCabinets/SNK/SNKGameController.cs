using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class SNKGameController : ArcadeGameController
{
    [Header("Pop Up")]
    public GameObject popUp;
    public Text popUpText;
    public Text gainText;

    public override BigInteger InitalScore => 0;

    // used to indicate whether the game should be running,
    // accessed by several other classes.
    public bool IsPlaying { get; set; }

    private GameObject player;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        // initialize everything
        popUp.SetActive(false);

        // start playing
        IsPlaying = true;
    }

    public override void EndGame()
    {
        IsPlaying = false;

        // calculate new data
        BigInteger tickets = score; // decide how many tickets to give
        //SNKCabinetStatus snkStatus = arcadeStatus.SNKStatus;
        //snkStatus.CumulativeScore += score;
        //snkStatus.HighScore = BigInteger.Max(snkStatus.HighScore, score);
        //snkStatus.Tickets += tickets;

        // update popup
        //popUpText.text = "Your Score: " + score
        //    + "\nCumulative Score: " + GameOperations.BigIntToString(snkStatus.CumulativeScore)
        //    + "\nTicket Count: " + GameOperations.BigIntToString(snkStatus.Tickets);
        //gainText.text = "(+" + GameOperations.BigIntToString(tickets) + ")";
        //popUp.SetActive(true);

        // write to file
        base.EndGame();
    }

    public void CollectPoints(int basePoints)
    {
        BigInteger pointsToGive = basePoints;
        // factor in any upgrade scaling here.

        score += pointsToGive;
        UpdateScore();
    }

    public void ReachedObjective()
    {
        // we could end game, or go to next level depending on upgrades
        EndGame();
    }
}
