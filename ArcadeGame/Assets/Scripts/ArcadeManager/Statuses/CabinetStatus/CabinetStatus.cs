using System.Collections;
using System.Collections.Generic;
using System.Numerics;

public abstract class CabinetStatus : LayerZeroStatus
{
    public BigIntWrapper highScore;
    public BigIntWrapper cumulativeScore;
    public bool isActive;

    public CabinetStatus() : base()
    {
        highScore = new BigIntWrapper();
        cumulativeScore = new BigIntWrapper();
        isActive = false;
    }
}
