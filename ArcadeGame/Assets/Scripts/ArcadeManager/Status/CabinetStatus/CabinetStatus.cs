using System.Collections;
using System.Collections.Generic;

public abstract class CabinetStatus
{
    public long gameTickets;
    public long highScore;
    public long cumulativeScore;
    public bool isActive;
    public List<ShopUpgrade> upgrades;

    public CabinetStatus()
    {
        gameTickets = 0;
        highScore = 0;
        cumulativeScore = 0;
        isActive = false;
        upgrades = new List<ShopUpgrade>();
    }
}
