using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrizeCounterController : MonoBehaviour
{
    /** The PopUpPanel in the shop screen. */
    public GameObject popUpPanel;

    /** The currently viewed upgrade. */
    public ShopUpgrade activeUpgrade;

    /** The PopUp UI. */
    public Image image;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;
    public Button buyButton;

    /** The ticket text in the wallet. */
    public Text ticketText;

    /** Upgrades. */
    public ShopUpgrade doublePoints;

    // Start is called before the first frame update
    void Start()
    {
        popUpPanel.SetActive(false);
        StartCoroutine(initialize());
    }

    /**
    * Used to initialize the DebugCabinetController.
    */
    IEnumerator initialize()
    {
        /*
        while (!dcc.initialized)
        {
            yield return null;
        }
        ds = dcc.ds;
        updateTicketText();
        updateAllUpgrades(); */
        yield return null;
    }

    /**
     * Used to initialize the PopUp when an upgrade is selected. 
     */
    public void buttonWasClicked()
    {
        popUpPanel.SetActive(true);
        //initializePopUp();
    }

    /**
     * Initializes the values of the PopUp.
     
    private void initializePopUp()
    {
        image.sprite = activeUpgrade.sprite;
        nameText.text = activeUpgrade.upgradeName;
        descriptionText.text = activeUpgrade.description;
        priceText.text = activeUpgrade.upgradePrice + " Tickets";

        if (activeUpgrade.currentLevel == activeUpgrade.maxLevel)
        {
            priceText.gameObject.SetActive(false);
            buyButton.interactable = false; // or message
        }
        else
        {
            priceText.gameObject.SetActive(true);
            buyButton.interactable = true;
        }
    }

    /**
     * Closes the PopUp.
     */
    public void closePopUp()
    {
        popUpPanel.SetActive(false);
        activeUpgrade = null;
    }

    /**
     * Updates the ticket text in the wallet.
     */
    public void updateTicketText()
    {
       // ticketText.text = "Tickets:\n" + ds.tickets;
    }

    /**
     * Called when the user presses the buy button.
     */
    public void buy()
    {
        /*
        if (activeUpgrade.upgradePrice > ds.tickets)
        {
            // ###########################################################
            // add error message for trying to buy with not enough tickets
            // ###########################################################
            return;
        }
        switch (activeUpgrade.upgradeName)
        {
            case "Double Points":
                if (doublePoints.currentLevel < doublePoints.maxLevel)
                {
                    // update the level
                    ds.doublePoints++;
                    // subtract tickets
                    ds.tickets -= activeUpgrade.upgradePrice;
                    // update the price
                    doublePoints.upgradePrice *= 2;
                    ds.doublePointsPrice = doublePoints.upgradePrice;
                    break;
                }
                else
                {
                    return;
                }
            default:
                Debug.LogError("Upgrade was not a valid name: " + activeUpgrade.upgradeName);
                return;
        }
        // update ui
        updateAllUpgrades();
        updateTicketText();
        closePopUp();*/
    }

    private void updateAllUpgrades()
    {
        // double points
        /*doublePoints.currentLevel = ds.doublePoints;
        doublePoints.upgradePrice = ds.doublePointsPrice; */
        //updateUpgradeUI(doublePoints);

        // upgrade 2
        // ...
        // upgrade 3
        // ...
    }
    /*
    private void updateUpgradeUI(ShopUpgrade upgrade)
    {
        upgrade.priceText.text = upgrade.upgradePrice + " Tickets";
        upgrade.levelText.text = upgrade.currentLevel + " / " + upgrade.maxLevel;
        if (upgrade.currentLevel == upgrade.maxLevel)
        {
            upgrade.priceText.gameObject.SetActive(false);
        }
    }*/
}
