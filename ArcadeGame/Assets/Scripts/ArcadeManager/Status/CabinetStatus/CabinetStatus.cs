using System.Collections;
using System.Collections.Generic;

public abstract class CabinetStatus : LayerZeroStatus
{
    public long highScore;
    public long cumulativeScore;
    public bool isActive;

    public CabinetStatus() : base()
    {
        highScore = 0;
        cumulativeScore = 0;
        isActive = false;
    }
}
