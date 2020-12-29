using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugShopController : ShopController
{

    /**
     * Used to initialize the DebugShopController.
     */
    public override void initialize()
    {
        // read the file and initialize the CabinetStatus
        arcadeStatus = ArcadeManager.readArcadeStatus();
        status = arcadeStatus.DebugStatus;
        updateTicketText();

        currentSetIndex = 0;
        loadUpgrades(currentSetIndex);
    }
}
