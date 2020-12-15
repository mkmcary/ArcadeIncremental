using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgradeUI : MonoBehaviour
{
    /** The ShopController. */
    public ShopController sc;

    /** UI Elements for this upgrade. */
    public Image image;
    public Text nameText;
    public Text priceText;
    public Text levelText;

    /** The upgrade we are displaying in this ShopUpgradeUI. */
    public ShopUpgrade activeUpgrade;

    /**
     * Used to pass data to the ShopController on click.
     */
    public void onUpgradeClick()
    {
        sc.activeUpgrade = activeUpgrade;
        sc.activeUpgradeUI = this;
        sc.buttonWasClicked();
    }

    public virtual void populate()
    {
        image.sprite = ArcadeManager.loadSprite(activeUpgrade.sprite);
        nameText.text = activeUpgrade.upgradeName;
        priceText.text = ArcadeManager.convertToScientific(activeUpgrade.price) + " Tickets";
        levelText.text = activeUpgrade.currentLevel + " / " + activeUpgrade.maxLevel;
        if (activeUpgrade.currentLevel == activeUpgrade.maxLevel)
        {
            priceText.gameObject.SetActive(false);
        }
    }
}
