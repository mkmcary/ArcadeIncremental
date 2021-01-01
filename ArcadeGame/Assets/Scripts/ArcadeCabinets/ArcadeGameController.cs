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
    protected BigInteger score;

    public abstract BigInteger InitalScore { get; }

    // Start is called before the first frame update
    public virtual void Start()
    {
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        score = InitalScore;
        UpdateScore();
    }
    
    /** The generic activity when a game is ended. */
    public virtual void EndGame()
    {
        ArcadeManager.WriteArcadeStatus();
    }

    /**
     * Updates the game score to the given score.
     * @param score the new score.
     */
    public virtual void UpdateScore()
    {
        scoreText.text = GameOperations.BigIntToString(score);
    }
}
