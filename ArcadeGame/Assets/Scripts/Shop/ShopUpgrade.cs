using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : MonoBehaviour
{
    /** The ShopController. */
    public DebugShopController sc;

    /** UI Elements for this upgrade. */
    public Image image;
    public Text nameText;
    public Text priceText;
    public Text levelText;

    /* Information about this Upgrade. */
    public string upgradeName;
    public string description;
    public long upgradePrice;
    public int currentLevel;
    public int maxLevel;
    public Sprite sprite;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = sprite;
        nameText.text = upgradeName;
        priceText.text = upgradePrice + " Tickets";
        levelText.text = currentLevel + " / " + maxLevel;
    }

    /**
     * Used to pass data to the ShopController on click.
     */
    public void onUpgradeClick()
    {
        sc.activeUpgrade = this;
        sc.buttonWasClicked();
    }
}
