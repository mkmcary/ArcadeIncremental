using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DebugCabinetStatus : CabinetStatus
{
    // Add all the upgrades here
    public ShopUpgrade doublePoints;
    public ShopUpgrade redUpgrade;

    //public DebugShopUpgrade upgradeName;

    public DebugCabinetStatus() : base()
    {
        isActive = true;

        // double points
        doublePoints = new ShopUpgrade();
        doublePoints.upgradeName = "Double Points";
        doublePoints.price = 1000;
        doublePoints.description = "Doubles the points received from playing this game.";
        doublePoints.currentLevel = 0;
        doublePoints.maxLevel = 5;
        doublePoints.priceScale = 2;
        doublePoints.sType = ShopUpgrade.scaleType.MULT;
        doublePoints.sprite = "Sprites/Shop/Placeholder/doubleMult";

        upgrades.Add(doublePoints);

        redUpgrade = new ShopUpgrade();
        redUpgrade.upgradeName = "Red Upgrade";
        redUpgrade.price = 99999999999;
        redUpgrade.description = "This upgrade is red.";
        redUpgrade.currentLevel = 0;
        redUpgrade.maxLevel = 3;
        redUpgrade.priceScale = 100;
        redUpgrade.sType = ShopUpgrade.scaleType.ADD;
        redUpgrade.sprite = "Sprites/CabinetScene/Placeholder/redCabinet";

        upgrades.Add(redUpgrade);
    }
}
