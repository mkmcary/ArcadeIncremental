using System.Numerics;
using UnityEngine;

public class BRDGameController : ArcadeGameController
{
    public BRDPlayerController playerController;

    public override BigInteger InitalScore => 0;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TapJump()
    {
        playerController.Jump();
    }

    public void TapFlip()
    {
        playerController.Flip();
    }

    public override void UpdateScore()
    {
        base.UpdateScore();
    }

    public override void EndGame()
    {
        base.EndGame();
    }
}
