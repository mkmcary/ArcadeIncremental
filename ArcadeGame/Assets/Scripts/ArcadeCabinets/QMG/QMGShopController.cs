using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QMGShopController : ShopController
{
    /**
      * Used to initialize the QMGShopController.
      */
    public override void Initialize()
    {
        // read the file and initialize the CabinetStatus
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        status = arcadeStatus.QMGStatus;
        UpdateTicketText();

        currentSetIndex = 0;
        LoadUpgrades(currentSetIndex);
    }
}
