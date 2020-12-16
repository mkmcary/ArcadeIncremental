using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class ArcadeStatus
{
    private List<LayerZeroStatus> statuses;

    public PrizeStatus prizeStatus;
    // Here we will contain all of the Cabinet Statuses
    public DebugCabinetStatus debugStatus;

    public ArcadeStatus()
    {
        statuses = new List<LayerZeroStatus>();

        // PRIZE STATUS MUST BE FIRST IN THE LIST
        prizeStatus = new PrizeStatus();
        statuses.Add(prizeStatus);

        // OTHER STATUSES
        debugStatus = new DebugCabinetStatus();
        statuses.Add(debugStatus);
    }

    public List<LayerZeroStatus> getLayerZeroStatuses()
    {
        return statuses;
    }
}
