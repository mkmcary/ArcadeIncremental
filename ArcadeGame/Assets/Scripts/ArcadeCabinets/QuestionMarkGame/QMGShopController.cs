using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QMGShopController : ShopController
{
    /**
      * Used to initialize the QMGShopController.
      */
    public override void initialize()
    {
        // read the file and initialize the CabinetStatus
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        status = arcadeStatus.QMGStatus;
        updateTicketText();

        currentSetIndex = 0;
        loadUpgrades(currentSetIndex);
    }
}
