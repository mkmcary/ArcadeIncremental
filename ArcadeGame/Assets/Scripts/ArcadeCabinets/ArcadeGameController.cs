using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public abstract class ArcadeGameController : MonoBehaviour
{
    /** Used to access the value of the user's current upgrades and currencies. */
    protected ArcadeStatus arcadeStatus;

    /** The text to display the number of active points the user has. */
    public Text scoreText;

    /** The active number of points for this game session. */
    public BigInteger score;

    public abstract BigInteger initalScore { get; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        arcadeStatus = ArcadeManager.ReadArcadeStatus();

        updateScore(initalScore);
    }
    
    /** The generic activity when a game is ended. */
    public virtual void endGame()
    {
        ArcadeManager.WriteArcadeStatus();
    }

    /**
     * Updates the game score to the given score.
     * @param score the new score.
     */
    public void updateScore(BigInteger score)
    {
        this.score = score;
        scoreText.text = "Current Points: " + GameOperations.bigIntToString(score);
    }
}
