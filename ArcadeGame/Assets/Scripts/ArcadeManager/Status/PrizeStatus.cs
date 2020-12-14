using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PrizeStatus
{
    public long prizeTickets;

    // Prize Upgrades

    // public PrizeShopUpgrade doublePoints;

    public PrizeStatus()
    {
        prizeTickets = 0;

        // Intialize upgrades to base form, after refactor
    }
}
