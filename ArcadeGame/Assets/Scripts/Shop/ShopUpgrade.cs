using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUpgrade : MonoBehaviour
{
    public ShopController sc;

    public Image image;

    public Text nameText;
    public Text priceText;

    public string upgradeName;
    public string description;
    public long upgradePrice;

    public Sprite sprite;
    

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = sprite;
        nameText.text = upgradeName;
        priceText.text = "" + upgradePrice;
    }

    public void onUpgradeClick()
    {
        sc.activeUpgrade = this;
        sc.buttonWasClicked();
    }
}
