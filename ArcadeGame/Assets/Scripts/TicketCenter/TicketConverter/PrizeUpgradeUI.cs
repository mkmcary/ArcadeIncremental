using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrizeUpgradeUI : ShopUpgradeUI
{

    public Color tintColor;

    /**
     * Update only the image: a PrizeUpgradeUI does not display the upgrade name,
     * price, or level.
     */
    public override void populate()
    {
        image.sprite = ArcadeManager.loadSprite(activeUpgrade.sprite);
        if(activeUpgrade.currentLevel == 0)
        {
            image.color = tintColor;
        } else
        {
            image.color = Color.white;
        }
        gameObject.SetActive(true);
    }
}
