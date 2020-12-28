using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterBuyer : MonoBehaviour
{
    private PawnStatus pawnStatus;

    [Header("Print Room Controller")]
    public PrintRoomController printRoomController;

    [Header("PopUp")]
    public GameObject popUp;
    public Text purchaseText;
    public Button buyButton;

    private TicketPrinterUI activePrinterUI;

    private void Start()
    {
        pawnStatus = PawnManager.readPawnStatus();
    }

    public void declinePurchase()
    {
        // just close this popup menu
        closePopUp();
    }

    public void acceptPurchase()
    {
        // take away the money
        pawnStatus.Money -= activePrinterUI.ActivePrinter.PurchasePrice;

        // set to active and update our UI to reflect this
        activePrinterUI.ActivePrinter.IsActive = true;
        activePrinterUI.Populate();
        closePopUp();

        // update money UI
        printRoomController.Activate();
    }

    private void closePopUp()
    {
        popUp.SetActive(false);
    }

    public void initializePopUp(TicketPrinterUI printerUI)
    {
        this.activePrinterUI = printerUI;

        // display price text
        purchaseText.text = "This Purchase Will Cost:\n$" + GameOperations.bigIntToString(activePrinterUI.ActivePrinter.PurchasePrice);

        // check if they have enough money to actually buy it
        if(pawnStatus.Money >= activePrinterUI.ActivePrinter.PurchasePrice)
        {
            buyButton.interactable = true;
        }
        else
        {
            buyButton.interactable = false;
        }

        popUp.SetActive(true);
    }
}
