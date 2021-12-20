using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SNKCabinetStatus : CabinetStatus
{
    [SerializeField]
    private ShopUpgrade robeUpgrade;

    public ShopUpgrade RobeUpgrade
    {
        get { return robeUpgrade; }
        set { robeUpgrade = value; }
    }

    public SNKCabinetStatus() : base()
    {
        Upgrades = new List<ShopUpgrade>();

        IsActive = true;

        // robe upgrade
        robeUpgrade = new ShopUpgrade();
        robeUpgrade.upgradeName = "Robes";
        robeUpgrade.price = new BigIntWrapper(1000);
        robeUpgrade.description = "Your robes now can handle more damage";
        robeUpgrade.currentLevel = 0;
        robeUpgrade.maxLevel = 5;
        robeUpgrade.priceScale = 2;
        robeUpgrade.sType = ShopUpgrade.ScaleType.MULT;
        robeUpgrade.sprite = "Sprites/ArcadeCabinets/BRD/Wizard";

        Upgrades.Add(robeUpgrade);
    }

    public override LayerZeroStatus ResetButPreserve()
    {
        SNKCabinetStatus newStat = new SNKCabinetStatus();
        newStat.CumulativeScore = this.CumulativeScore;
        newStat.HighScore = this.HighScore;
        return newStat;
    }
}
