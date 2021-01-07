﻿using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class BRDGameController : ArcadeGameController
{
    

    public bool isPlaying;

    [Header("UI")]
    public GameObject jumpButton;
    public GameObject popUp;

    // Player Data
    public BRDPlayerController playerController;

    private int jumpsCap;
    private int flipsCap;
    private int jumpsRemaining;
    private int flipsRemaining;

    //Score
    public override BigInteger InitalScore => 0;

    private BigInteger scoreIncrement;
    private BigInteger scoreBreakPoint;

    private int scoreCounter;
    private float scoreTimer;
    private float scoreInterval;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        isPlaying = true;

        if (playerController.GetComponent<Rigidbody2D>().gravityScale < 0)
        {
            playerController.Flip();
            RotateJumpButton();
        }
        jumpsCap = 3;
        flipsCap = 1;
        RestoreJumps();
        RestoreFlips();

        DestroyObstacles();

        popUp.SetActive(false);

        scoreCounter = 0;
        scoreTimer = 0;
        scoreInterval = 5f;
        scoreIncrement = 10;
        scoreBreakPoint = 10;
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
            score += scoreIncrement;
            scoreTimer = 0;
            CheckScore();
            base.UpdateScore();
        }
    }

    private void CheckScore()
    {
        scoreCounter++;
        if(score >= BigInteger.Pow(scoreBreakPoint, scoreCounter))
        {
            scoreIncrement *= scoreCounter;
        }
    }

    private void DestroyObstacles()
    {
        BRDObstacle[] obstacles = FindObjectsOfType<BRDObstacle>();
        for (int i = 0; i < obstacles.Length; i++){
            GameObject.Destroy(obstacles[i].gameObject);
        }
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
