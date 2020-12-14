﻿using System.Collections;
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
        status = arcadeStatus.debugStatus;
        updateTicketText();
        loadUpgrades();
        updateAllUpgrades();
    }

    /**
     * Called when the user presses the buy button.
     */
    public override void buy()
    {
        if (activeUpgrade.price > status.gameTickets)
        {
            // ###########################################################
            // add error message for trying to buy with not enough tickets
            // ###########################################################
            return;
        }

        if (activeUpgrade.currentLevel < activeUpgrade.maxLevel)
        {
            status.gameTickets -= activeUpgrade.price;
            activeUpgrade.LevelUp();
        } else
        {
            // this upgrade is already at max level
        }

        // update ui
        updateUpgradeUI(activeUpgradeUI);
        //updateAllUpgrades();
        updateTicketText();
        closePopUp();
    }

    private void updateAllUpgrades()
    {
        // double points
        //updateUpgradeUI();

        // upgrade 2
        // ...
        // upgrade 3
        // ...
    }

}