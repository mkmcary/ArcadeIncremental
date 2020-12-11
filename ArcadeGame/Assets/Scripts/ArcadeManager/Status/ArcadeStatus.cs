using System.Collections;
using System.Collections.Generic;

public class ArcadeStatus
{
    public PrizeStatus prizeStatus;
    // Here we will contain all of the Cabinet Statuses
    public DebugCabinetStatus debugStatus;

    public ArcadeStatus()
    {
        prizeStatus = new PrizeStatus();
        debugStatus = new DebugCabinetStatus();
    }
}
