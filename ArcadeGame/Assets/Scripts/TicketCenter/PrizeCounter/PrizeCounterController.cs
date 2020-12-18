using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCounterController : ShopController
{
    /**
     * Used to initialize the PrizeCounterController.
     */
    public override void initialize()
    {
        // read the file and initialize the CabinetStatus
        arcadeStatus = ArcadeManager.readArcadeStatus();
        status = arcadeStatus.prizeStatus;
        updateTicketText();

        currentSetIndex = 0;
        loadUpgrades(currentSetIndex);
    }
}
