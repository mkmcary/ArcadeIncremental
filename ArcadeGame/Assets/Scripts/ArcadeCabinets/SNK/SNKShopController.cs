using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SNKShopController : ShopController
{
    public override void Initialize()
    {
        // read the file and initialize the CabinetStatus
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        status = arcadeStatus.SNKStatus;
        UpdateTicketText();

        currentSetIndex = 0;
        LoadUpgrades(currentSetIndex);
    }
}
