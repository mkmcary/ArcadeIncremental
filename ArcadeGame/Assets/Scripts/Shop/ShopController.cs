using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopController : MonoBehaviour
{
    /** The CabinetStatus for this Shop. */
    public ArcadeStatus arcadeStatus;
    public LayerZeroStatus status;

    /** The PopUpPanel in the shop screen. */
    public GameObject popUpPanel;

    /** The currently viewed upgrade. */
    public ShopUpgrade activeUpgrade;
    public ShopUpgradeUI activeUpgradeUI;

    /** All the ShopUpgradeUIs. */
    public List<ShopUpgradeUI> upgradeUIs;

    /** The Upgrade UI. */
    public Image image;
    public Text nameText;
    public Text descriptionText;
    public Text priceText;
    public Button buyButton;

    // Scroll Data
    public Button scrollUpButton;
    public Button scrollDownButton;
    protected int currentSetIndex;

    /** The ticket text in the wallet. */
    public Text ticketText;

    // Start is called before the first frame update
    void Start()
    {
        popUpPanel.SetActive(false);
        initialize();
    }

    /**
     * Used to initialize the CabinetStatus.
     */
    public abstract void initialize();

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
        image.sprite = ArcadeManager.loadSprite(activeUpgrade.sprite);
        nameText.text = activeUpgrade.upgradeName;
        descriptionText.text = activeUpgrade.description;
        priceText.text = ArcadeManager.bigIntToString(activeUpgrade.price.value) + " Tickets";

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
        ticketText.text = ArcadeManager.bigIntToString(status.tickets.value);
    }

    /**
     * Called when the user presses the buy button.
     */
    public void buy()
    {
        if (activeUpgrade.price.value > status.tickets.value)
        {
            // ###########################################################
            // add error message for trying to buy with not enough tickets
            // ###########################################################
            return;
        }

        if (activeUpgrade.currentLevel < activeUpgrade.maxLevel)
        {
            status.tickets.value -= activeUpgrade.price.value;
            activeUpgrade.LevelUp();
        }
        else
        {
            // this upgrade is already at max level
        }

        // update ui
        activeUpgradeUI.populate();
        updateTicketText();
        closePopUp();
    }

    public void loadUpgrades(int min)
    {
        List<ShopUpgrade> upgrades = status.getUpgrades();

        int max = min + upgradeUIs.Count - 1;

        if(max - min + 1 != upgradeUIs.Count)
        {
            Debug.LogError("Not a valid upgrade range");
            return;
        }
        if(max >= upgrades.Count)
        {
            max = upgrades.Count - 1;
        }

        int i = 0;
        for (int j = min; j <= max; j++)
        {
            upgradeUIs[i].activeUpgrade = upgrades[j];
            upgradeUIs[i].populate();
            i++;
        }
        for(; i < upgradeUIs.Count; i++)
        {
            upgradeUIs[i].gameObject.SetActive(false);
        }

        // initialize the scroll buttons
        if (scrollDownButton != null && scrollUpButton != null)
        {
            if (min == 0)
            {
                scrollUpButton.gameObject.SetActive(false);
            }
            if (max == upgrades.Count - 1)
            {
                scrollDownButton.gameObject.SetActive(false);
            }
        }
    }

    public void scrollUp()
    {
        if (currentSetIndex == 0)
        {
            Debug.LogError("Should not be scrolling up.");
            return;
        }

        currentSetIndex -= upgradeUIs.Count;
        loadUpgrades(currentSetIndex);

        if (currentSetIndex == 0)
        {
            // if at top, disable up arrow
            scrollUpButton.gameObject.SetActive(false);
        }
        else
        {
            // else, make sure it is available
            scrollUpButton.gameObject.SetActive(true);
        }
        // always make sure scroll down is available
        scrollDownButton.gameObject.SetActive(true);
    }

    public void scrollDown()
    {

        List<ShopUpgrade> upgrades = status.getUpgrades();
        if (currentSetIndex + upgradeUIs.Count >= upgrades.Count)
        {
            Debug.LogError("Should not be scrolling down.");
            return;
        }

        currentSetIndex += upgradeUIs.Count;
        loadUpgrades(currentSetIndex);

        if (currentSetIndex + upgradeUIs.Count >= upgrades.Count)
        {
            // if at bottom, disable down arrow
            scrollDownButton.gameObject.SetActive(false);
        }
        else
        {
            // else, make sure it is available
            scrollDownButton.gameObject.SetActive(true);
        }
        // always make sure scroll up is available
        scrollUpButton.gameObject.SetActive(true);
    }

    public void OnApplicationQuit()
    {
        ArcadeManager.writeArcadeStatus();
    }

}
