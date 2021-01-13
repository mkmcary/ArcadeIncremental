using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRDShopController : ShopController
{
    public override void Initialize()
    {
        // read the file and initialize the CabinetStatus
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        status = arcadeStatus.BRDStatus;
        UpdateTicketText();

        currentSetIndex = 0;
        LoadUpgrades(currentSetIndex);
    }
}
