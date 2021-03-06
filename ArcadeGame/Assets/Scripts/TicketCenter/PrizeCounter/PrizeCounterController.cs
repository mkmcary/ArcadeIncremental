﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCounterController : ShopController
{
    /**
     * Used to initialize the PrizeCounterController.
     */
    public override void Initialize()
    {
        // read the file and initialize the CabinetStatus
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        status = arcadeStatus.ArcadePrizeStatus;
        UpdateTicketText();

        currentSetIndex = 0;
        LoadUpgrades(currentSetIndex);
    }
}
