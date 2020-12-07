using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public GameObject popUpPanel;

    public Sprite sprite;
    public string description;
    public string upgradeName;
    public long price;

    public Image image;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;

    // Start is called before the first frame update
    void Start()
    {
        popUpPanel.SetActive(false);
    }

    public void buttonWasClicked()
    {
        popUpPanel.SetActive(true);
        initializePopUp();
    }

    private void initializePopUp()
    {
        image.sprite = sprite;
        nameText.text = upgradeName;
        descriptionText.text = description;
        priceText.text = price + " Tickets";
    }

    public void closePopUp()
    {
        popUpPanel.SetActive(false);
    }


}
