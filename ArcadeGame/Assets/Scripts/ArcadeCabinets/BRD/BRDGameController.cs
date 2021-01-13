using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class BRDGameController : ArcadeGameController
{
    public bool isPlaying;
    public bool isSpawning;

    [Header("UI")]
    public GameObject jumpButton;
    public GameObject popUp;
    public Text endGameText;

    // Player Data
    public BRDPlayerController playerController;

    public int jumpsCap;
    public int flipsCap;
    private int jumpsRemaining;
    private int flipsRemaining;

    //Score
    public override BigInteger InitalScore => 0;

    private BigInteger scoreIncrement;
    private BigInteger scoreBreakPoint;

    private int scoreCounter;
    private float scoreTimer;
    private float scoreInterval;

    // Win conditions
    public bool winCondition;
    private BigInteger scoreToWin;
    private int multOnWin;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        isPlaying = true;

        // Orient player if last game ended upside-down
        if (playerController.GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            playerController.Flip();
            RotateJumpButton();
        }

        // Destroy all obstacles from previous game
        DestroyObstacles();

        // Set the players initial resources
        jumpsCap = 5;
        flipsCap = 3;
        RestoreJumps();
        RestoreFlips();

        // Deactivate pop up
        popUp.SetActive(false);

        // Initialize all of the score details
        scoreCounter = 0;
        scoreTimer = 0;
        scoreInterval = 5f;
        scoreIncrement = 10;
        scoreBreakPoint = 10;

        // Initalize win conditions
        isSpawning = true;
        winCondition = false;
        multOnWin = 2;
        scoreToWin = 500;
    }

    private void FixedUpdate()
    {
        if (!isPlaying)
        {
            return;
        }
        scoreTimer += Time.fixedDeltaTime;
        if(scoreTimer >= scoreInterval)
        {
            GainScore(scoreIncrement);
            scoreTimer = 0;
            CheckScore();
            base.UpdateScore();
        }
    }

    public void GainScore(BigInteger score)
    {
        this.score += score;
        CheckScore();
        base.UpdateScore();
    }

    private void CheckScore()
    {
        // If the score reaches the breakpoint, scale up passive gain.
        if(score >= BigInteger.Pow(scoreBreakPoint, scoreCounter))
        {
            scoreCounter++;
            scoreIncrement *= scoreCounter;
        }
        if(score >= scoreToWin)
        {
            winCondition = true;
        }
    }

    private void DestroyObstacles()
    {
        BRDObstacle[] obstacles = FindObjectsOfType<BRDObstacle>();
        for (int i = 0; i < obstacles.Length; i++){
            GameObject.Destroy(obstacles[i].gameObject);
        }
        BRDFinishLine finishLine = FindObjectOfType<BRDFinishLine>();
        if(finishLine != null)
        {
            GameObject.Destroy(finishLine.gameObject);
        }
    }

    public void Win()
    {
        score *= multOnWin;
        endGameText.text = "You Win!";
        EndGame();
    }

    private void RotateJumpButton()
    {
        UnityEngine.Quaternion temp = jumpButton.transform.rotation;
        if (playerController.isFlipped)
        {
            temp.z = 180f;
        }
        else
        {
            temp.z = 0;
        }
        jumpButton.transform.rotation = temp;
    }

    public void TapJump()
    {
        if(jumpsRemaining > 0)
        {
            jumpsRemaining--;
            playerController.Jump();
        }
        
    }

    public void TapFlip()
    {
        if(flipsRemaining > 0)
        {
            flipsRemaining--;

            playerController.Flip();

            RotateJumpButton();
        }
    }

    public void RestoreJumps()
    {
        jumpsRemaining = jumpsCap;
    }

    public void RestoreFlips()
    {
        flipsRemaining = flipsCap;
    }

    public override void UpdateScore()
    {
        base.UpdateScore();
    }

    public override void EndGame()
    {
        base.EndGame();
        isPlaying = false;

        popUp.SetActive(true);
    }
}
