using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ShopController : MonoBehaviour
{
    /** The CabinetStatus for this Shop. */
    public ArcadeStatus arcadeStatus;
    public CabinetStatus status;

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
        image.sprite = loadSprite(activeUpgrade.sprite);
        nameText.text = activeUpgrade.upgradeName;
        descriptionText.text = activeUpgrade.description;
        priceText.text = activeUpgrade.price + " Tickets";

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
        ticketText.text = "" + status.gameTickets;
    }

    /**
     * Called when the user presses the buy button.
     */
    public abstract void buy();

    public void loadUpgrades()
    {
        for (int i = 0; i < upgradeUIs.Count; i++)
        {
            upgradeUIs[i].activeUpgrade = status.upgrades[i];
            updateUpgradeUI(upgradeUIs[i]);
        }
    }

    public void updateUpgradeUI(ShopUpgradeUI ui)
    {
        //Debug.Log(Resources.Load<Sprite>("Sprites/Shop/Placeholder/doubleMult"));

        ui.image.sprite = loadSprite(ui.activeUpgrade.sprite);//ui.activeUpgrade.sprite;
        ui.nameText.text = ui.activeUpgrade.upgradeName;
        ui.priceText.text = ui.activeUpgrade.price + " Tickets";
        ui.levelText.text = ui.activeUpgrade.currentLevel + " / " + ui.activeUpgrade.maxLevel;
        if (ui.activeUpgrade.currentLevel == ui.activeUpgrade.maxLevel)
        {
            ui.priceText.gameObject.SetActive(false);
        }
    }

    public Sprite loadSprite(string path)
    {
        return Resources.Load<Sprite>(path);
    }

    public abstract void writeChanges();

    public void OnApplicationQuit()
    {
        writeChanges();
    }

}
