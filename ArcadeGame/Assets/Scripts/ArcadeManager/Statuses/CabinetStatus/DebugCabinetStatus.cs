using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DebugCabinetStatus : CabinetStatus
{
    // Add all the upgrades here
    [SerializeField]
    private ShopUpgrade doublePoints;
    [SerializeField]
    private ShopUpgrade redUpgrade;
    
    //public ShopUpgrade blueUpgrade;
    //public ShopUpgrade greenUpgrade;
    //public ShopUpgrade yellowUpgrade;
    //public ShopUpgrade redUpgrade2;
    //public ShopUpgrade blueUpgrade2;

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

    public DebugCabinetStatus() : base()
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

        /*
        // blue upgrade
        blueUpgrade = new ShopUpgrade();
        blueUpgrade.upgradeName = "Blue Upgrade";
        blueUpgrade.price = new BigIntWrapper(100);
        blueUpgrade.description = "This upgrade is blue.";
        blueUpgrade.currentLevel = 0;
        blueUpgrade.maxLevel = 4;
        blueUpgrade.priceScale = 100;
        blueUpgrade.sType = ShopUpgrade.ScaleType.ADD;
        blueUpgrade.sprite = "Sprites/CabinetScene/Placeholder/blueCabinet";

        Upgrades.Add(blueUpgrade);
        
        // green upgrade
        greenUpgrade = new ShopUpgrade();
        greenUpgrade.upgradeName = "Green Upgrade";
        greenUpgrade.price = new BigIntWrapper(3000);
        greenUpgrade.description = "This upgrade is green.";
        greenUpgrade.currentLevel = 0;
        greenUpgrade.maxLevel = 2;
        greenUpgrade.priceScale = 100;
        greenUpgrade.sType = ShopUpgrade.ScaleType.ADD;
        greenUpgrade.sprite = "Sprites/CabinetScene/Placeholder/greenCabinet";

        Upgrades.Add(greenUpgrade);

        // yellow upgrade
        yellowUpgrade = new ShopUpgrade();
        yellowUpgrade.upgradeName = "Yellow Upgrade";
        yellowUpgrade.price = new BigIntWrapper(7000);
        yellowUpgrade.description = "This upgrade is yellow.";
        yellowUpgrade.currentLevel = 0;
        yellowUpgrade.maxLevel = 10;
        yellowUpgrade.priceScale = 100;
        yellowUpgrade.sType = ShopUpgrade.ScaleType.ADD;
        yellowUpgrade.sprite = "Sprites/CabinetScene/Placeholder/yellowCabinet";

        Upgrades.Add(yellowUpgrade);

        // red upgrade 2
        redUpgrade2 = new ShopUpgrade();
        redUpgrade2.upgradeName = "Red Upgrade 2";
        redUpgrade2.price = new BigIntWrapper(5);
        redUpgrade2.description = "This upgrade is red, but a second time.";
        redUpgrade2.currentLevel = 0;
        redUpgrade2.maxLevel = 5;
        redUpgrade2.priceScale = 2;
        redUpgrade2.sType = ShopUpgrade.ScaleType.EXP;
        redUpgrade2.sprite = "Sprites/CabinetScene/Placeholder/redCabinet";

        Upgrades.Add(redUpgrade2);

        // blue upgrade 2
        blueUpgrade2 = new ShopUpgrade();
        blueUpgrade2.upgradeName = "Blue Upgrade 2";
        blueUpgrade2.price = new BigIntWrapper(30);
        blueUpgrade2.description = "This upgrade is blue, but a second time.";
        blueUpgrade2.currentLevel = 0;
        blueUpgrade2.maxLevel = 3;
        blueUpgrade2.priceScale = 100;
        blueUpgrade2.sType = ShopUpgrade.ScaleType.MULT;
        blueUpgrade2.sprite = "Sprites/CabinetScene/Placeholder/blueCabinet";

        Upgrades.Add(blueUpgrade2);
        */
    }

    public override LayerZeroStatus ResetButPreserve()
    {
        DebugCabinetStatus newStat = new DebugCabinetStatus();
        newStat.CumulativeScore = this.CumulativeScore;
        newStat.HighScore = this.HighScore;
        return newStat;
    }
}
