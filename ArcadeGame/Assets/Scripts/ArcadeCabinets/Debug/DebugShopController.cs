using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugShopController : ShopController
{

    /**
     * Used to initialize the DebugShopController.
     */
    public override void Initialize()
    {
        // read the file and initialize the CabinetStatus
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        status = arcadeStatus.DebugStatus;
        UpdateTicketText();

        currentSetIndex = 0;
        LoadUpgrades(currentSetIndex);
    }
}
