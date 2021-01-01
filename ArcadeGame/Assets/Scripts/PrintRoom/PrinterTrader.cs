using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterTrader : MonoBehaviour
{
    private TicketPrinter printer;
    private TicketPrinter nextPrinter;
    private PawnStatus pawnStatus;

    [Header("Pop Up")]
    public GameObject popUp;
    public Image printerImage;
    public Text priceText;
    public Button yesButton;

    [Header("Upgrade Bars")]
    public UpgradeBarHelper batchSizeBar;
    public UpgradeBarHelper batchTimeBar;
    public UpgradeBarHelper capacityBar;
    public UpgradeBarHelper luckBar;

    public void InitializePopUp(TicketPrinter printer)
    {
        pawnStatus = PawnManager.ReadPawnStatus();
        this.printer = printer;
        nextPrinter = printer.GetNextPrinter();
        nextPrinter.PrinterIndex = this.printer.PrinterIndex;

        popUp.SetActive(true);
        printerImage.sprite = nextPrinter.GetPrinterSprite();
        priceText.text = "This Will Cost:\n$" + GameOperations.BigIntToString(nextPrinter.PurchasePrice);

        if(pawnStatus.Money < nextPrinter.PurchasePrice)
        {
            yesButton.interactable = false;
        } 
        else
        {
            yesButton.interactable = true;
        }

        InitializeUpgradeBars();
    }
    private void InitializeUpgradeBars()
    {
        batchSizeBar.LogarithmicPopulate(nextPrinter.BatchSize);
        batchTimeBar.TimePopulate(nextPrinter.BatchTime);
        capacityBar.Populate(nextPrinter.Capacity, nextPrinter.CapacityCurrentLevel, nextPrinter.CapacityMaxLevel);
        luckBar.Populate(nextPrinter.Luck, nextPrinter.LuckCurrentLevel, nextPrinter.LuckMaxLevel);
    }

    public void TradeIn()
    {
        pawnStatus.Money -= nextPrinter.PurchasePrice;
        GetComponent<PrintRoomController>().CollectTicketsFromPrinter(printer);

        printer.TradeIn();

        GetComponent<PrintRoomController>().Repopulate();
        GetComponent<PrinterModifier>().InitializePopUp(printer);
        CloseTradeInPopUp();
    }

    public void CloseTradeInPopUp()
    {
        popUp.SetActive(false);
    }
}
