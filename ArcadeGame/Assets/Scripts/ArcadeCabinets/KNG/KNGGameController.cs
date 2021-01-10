using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class KNGGameController : ArcadeGameController
{
    [Header("Pop Up")]
    public GameObject popUp;

    public override BigInteger InitalScore => 0;

    // used to indicate whether the game should be running,
    // accessed by several other classes.
    public bool IsPlaying { get; set; }

    private GameObject player;
    private UnityEngine.Vector3 initPlayerPos;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        // initialize everything
        popUp.SetActive(false);

        // player
        if(player == null)
        {
            player = GameObject.FindObjectOfType<KNGPlayerController>().gameObject;
            initPlayerPos = player.transform.position;
        } 
        else
        {
            player.transform.position = initPlayerPos;
        }

        // collectibles
        KNGCollectible[] collectibles = GameObject.FindObjectsOfType<KNGCollectible>();
        for(int i = 0; i < collectibles.Length; i++)
        {
            collectibles[i].SetInteractiblity(true);
        }

        // start playing
        IsPlaying = true;
    }

    public override void EndGame()
    {
        IsPlaying = false;
        popUp.SetActive(true);
        base.EndGame();
    }

    private void ClearObstacles()
    {
        KNGObstacle[] obstacles = GameObject.FindObjectsOfType<KNGObstacle>();
        for(int i = 0; i < obstacles.Length; i++)
        {
            GameObject.Destroy(obstacles[i].gameObject);
        }
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
        ClearObstacles();

        // we could end game, or go to next level depending on upgrades
        EndGame();
    }

    public void HitObstacle()
    {
        // maybe end game, maybe extra life if this is an upgrade?
        ClearObstacles();
        EndGame();
    }
}
