using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopUpController : MonoBehaviour
{
    public ShopController sc;

    public Image image;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;

    public void initializePopUp()
    {
        image.sprite = sc.sprite;
        nameText.text = sc.upgradeName;
        descriptionText.text = sc.description;
        priceText.text = sc.price + " Tickets";
    }
}
