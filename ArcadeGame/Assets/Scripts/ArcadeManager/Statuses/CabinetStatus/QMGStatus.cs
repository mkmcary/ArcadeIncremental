using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QMGStatus : CabinetStatus
{
    // Add all the upgrades here
    [SerializeField]
    private ShopUpgrade doublePoints;
    [SerializeField]
    private ShopUpgrade redUpgrade;

    public ShopUpgrade DoublePoints
    {
        get { return doublePoints; }
        set { doublePoints = value; }
    }

    public ShopUpgrade RedUpgrade
    {
        get { return redUpgrade; }
        set { redUpgrade = value; }
    }

    public QMGStatus() : base()
    {
        Upgrades = new List<ShopUpgrade>();

        IsActive = true;

        // double points
        doublePoints = new ShopUpgrade();
        doublePoints.upgradeName = "Double Points";
        doublePoints.price = new BigIntWrapper(1000);
        doublePoints.description = "Doubles the points received from playing this game.";
        doublePoints.currentLevel = 0;
        doublePoints.maxLevel = 5;
        doublePoints.priceScale = 2;
        doublePoints.sType = ShopUpgrade.ScaleType.MULT;
        doublePoints.sprite = "Sprites/Shop/Placeholder/doubleMult";

        Upgrades.Add(doublePoints);

        // red upgrade
        redUpgrade = new ShopUpgrade();
        redUpgrade.upgradeName = "Red Upgrade";
        redUpgrade.price = new BigIntWrapper(99999999999);
        redUpgrade.description = "This upgrade is red.";
        redUpgrade.currentLevel = 0;
        redUpgrade.maxLevel = 3;
        redUpgrade.priceScale = 100;
        redUpgrade.sType = ShopUpgrade.ScaleType.ADD;
        redUpgrade.sprite = "Sprites/CabinetScene/Placeholder/redCabinet";

        Upgrades.Add(redUpgrade);
    }

    public override LayerZeroStatus ResetButPreserve()
    {
        DebugCabinetStatus newStat = new DebugCabinetStatus();
        newStat.CumulativeScore = this.CumulativeScore;
        newStat.HighScore = this.HighScore;
        return newStat;
    }
}
