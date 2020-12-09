using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    public GameObject popUpPanel;

    public ShopUpgrade activeUpgrade;

    public Image image;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;

    public Text ticketText;

    public DebugCabinetController dcc;

    private DebugCabinetController.DebugStatus ds;

    // Start is called before the first frame update
    void Start()
    {
        popUpPanel.SetActive(false);
        StartCoroutine(initialize());
        updateTicketText();
    }

    IEnumerator initialize()
    {
        while (!dcc.initialized)
        {
            yield return null;
        }
        ds = dcc.ds;
    }

    public void buttonWasClicked()
    {
        popUpPanel.SetActive(true);
        initializePopUp();
    }

    private void initializePopUp()
    {
        image.sprite = activeUpgrade.sprite;
        nameText.text = activeUpgrade.upgradeName;
        descriptionText.text = activeUpgrade.description;
        priceText.text = activeUpgrade.upgradePrice + " Tickets";
    }

    public void closePopUp()
    {
        popUpPanel.SetActive(false);
    }

    public void updateTicketText()
    {
        ticketText.text = "Debug Tickets:\n" + ds.tickets;
    }

    public void buy()
    {
        if(activeUpgrade.upgradePrice > ds.tickets)
        {
            // add error message for trying to buy while broke
            return;
        }
        switch (activeUpgrade.upgradeName)
        {
            case "Double Points":
                ds.doublePoints++;
                break;
            default:
                Debug.LogError("Upgrade was not a valid name: " + activeUpgrade.upgradeName);
                return;
        }
        ds.tickets -= activeUpgrade.upgradePrice;
        updateTicketText();
        closePopUp();
    }
}
