﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    /** The PopUpPanel in the shop screen. */
    public GameObject popUpPanel;

    /** The currently viewed upgrade. */
    public ShopUpgrade activeUpgrade;

    /** The Upgrade UI. */
    public Image image;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;

    /** The ticket text in the wallet. */
    public Text ticketText;

    /** The DebugCabinetController and its status. */
    public DebugCabinetController dcc;
    private DebugCabinetController.DebugStatus ds;

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
        while (!dcc.initialized)
        {
            yield return null;
        }
        ds = dcc.ds;
        updateTicketText();
    }

    /**
     * Used to initialize the PopUp when an upgrade is selected. 
     */
    public void buttonWasClicked()
    {
        popUpPanel.SetActive(true);
        initializePopUp();
    }

    /**
     * Initializes the values of the PopUp.
     */
    private void initializePopUp()
    {
        image.sprite = activeUpgrade.sprite;
        nameText.text = activeUpgrade.upgradeName;
        descriptionText.text = activeUpgrade.description;
        priceText.text = activeUpgrade.upgradePrice + " Tickets";
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
        ticketText.text = "Debug Tickets:\n" + ds.tickets;
    }

    /**
     * Called when the user presses the buy button.
     */
    public void buy()
    {
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
