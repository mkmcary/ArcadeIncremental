using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public abstract class CabinetStatus : LayerZeroStatus
{
    [SerializeField]
    private BigIntWrapper highScore;
    [SerializeField]
    private BigIntWrapper cumulativeScore;
    [SerializeField]
    private bool isActive;

    public BigInteger HighScore
    {
        get { return highScore.value; }
        set { highScore.value = value; }
    }

    public BigInteger CumulativeScore
    {
        get { return cumulativeScore.value; }
        set { cumulativeScore.value = value; }
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public CabinetStatus() : base()
    {
        highScore = new BigIntWrapper();
        cumulativeScore = new BigIntWrapper();
        isActive = false;
    }
}
