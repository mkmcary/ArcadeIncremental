using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class PrizeStatus : LayerZeroStatus
{
    // upgrades
    public ShopUpgrade doublePoints;

    public PrizeStatus() : base()
    {
        List<ShopUpgrade> upgrades = getUpgrades();

        // double points
        doublePoints = new ShopUpgrade();
        doublePoints.upgradeName = "Double Points";
        doublePoints.price = new BigIntWrapper(1000);
        doublePoints.description = "Doubles the points received from playing this game.";
        doublePoints.currentLevel = 0;
        doublePoints.maxLevel = 1;
        doublePoints.priceScale = 2;
        doublePoints.sType = ShopUpgrade.scaleType.MULT;
        doublePoints.sprite = "Sprites/Shop/Placeholder/doubleMult";

        upgrades.Add(doublePoints);
    }
}
