using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArcadeStatus
{
    private List<LayerZeroStatus> statuses;

    [SerializeField]
    private PrizeStatus prizeStatus;

    // Here we will have all of the Cabinet Statuses
    [SerializeField]
    private DebugCabinetStatus debugStatus;
    [SerializeField]
    private QMGCabinetStatus qmgStatus;
    [SerializeField]
    private KNGCabinetStatus kngStatus;

    public List<LayerZeroStatus> Statuses
    {
        get { return statuses; }
        set { statuses = value; }
    }

    public PrizeStatus ArcadePrizeStatus
    {
        get { return prizeStatus; }
        set { prizeStatus = value; }
    }

    public DebugCabinetStatus DebugStatus
    {
        get { return debugStatus; }
        set { debugStatus = value; }
    }

    public QMGCabinetStatus QMGStatus
    {
        get { return qmgStatus; }
        set { qmgStatus = value; }
    }

    public KNGCabinetStatus KNGStatus
    {
        get { return kngStatus; }
        set { kngStatus = value; }
    }

    public ArcadeStatus()
    {
        statuses = new List<LayerZeroStatus>();

        // PRIZE STATUS MUST BE FIRST IN THE LIST
        prizeStatus = new PrizeStatus();
        statuses.Add(prizeStatus);

        // OTHER STATUSES
        debugStatus = new DebugCabinetStatus();
        statuses.Add(debugStatus);

        qmgStatus = new QMGCabinetStatus();
        statuses.Add(qmgStatus);

        kngStatus = new KNGCabinetStatus();
        statuses.Add(kngStatus);
    }

    public void ResetButPreserve()
    {
        statuses = new List<LayerZeroStatus>();

        // PRIZE STATUS MUST BE FIRST IN THE LIST
        prizeStatus = (PrizeStatus)prizeStatus.ResetButPreserve();
        statuses.Add(prizeStatus);

        // OTHER STATUSES
        debugStatus = (DebugCabinetStatus)debugStatus.ResetButPreserve();
        statuses.Add(debugStatus);

        qmgStatus = (QMGCabinetStatus)qmgStatus.ResetButPreserve();
        statuses.Add(qmgStatus);

        kngStatus = (KNGCabinetStatus)kngStatus.ResetButPreserve();
        statuses.Add(kngStatus);
    }
}
