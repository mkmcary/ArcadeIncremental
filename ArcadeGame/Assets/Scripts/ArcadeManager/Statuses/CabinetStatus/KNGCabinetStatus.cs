using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KNGCabinetStatus : CabinetStatus
{
    [SerializeField]
    private ShopUpgrade bananaUpgrade;

    public ShopUpgrade BananaUpgrade
    {
        get { return bananaUpgrade; }
        set { bananaUpgrade = value; }
    }

    public KNGCabinetStatus() : base()
    {
        Upgrades = new List<ShopUpgrade>();

        IsActive = true;

        // banana upgrade
        bananaUpgrade = new ShopUpgrade();
        bananaUpgrade.upgradeName = "Bananas";
        bananaUpgrade.price = new BigIntWrapper(1000);
        bananaUpgrade.description = "Bananas are now worth more points.";
        bananaUpgrade.currentLevel = 0;
        bananaUpgrade.maxLevel = 5;
        bananaUpgrade.priceScale = 2;
        bananaUpgrade.sType = ShopUpgrade.ScaleType.MULT;
        bananaUpgrade.sprite = "Sprites/ArcadeCabinets/KNG/JungleTheme/Collectibles/BananaBunch";

        Upgrades.Add(bananaUpgrade);
    }

    public override LayerZeroStatus ResetButPreserve()
    {
        KNGCabinetStatus newStat = new KNGCabinetStatus();
        newStat.CumulativeScore = this.CumulativeScore;
        newStat.HighScore = this.HighScore;
        return newStat;
    }
}
