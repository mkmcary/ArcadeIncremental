using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterBuyer : MonoBehaviour
{
    private PawnStatus pawnStatus;

    public GameObject popUp;
    public Text purchaseText;

    public List<TicketPrinterUI> ticketPrinterUIs;

    private void Start()
    {
        pawnStatus = PawnManager.readPawnStatus();

        ticketPrinterUIs[0].ActivePrinter = pawnStatus.Printers[0];
        ticketPrinterUIs[0].Populate();
        ticketPrinterUIs[1].ActivePrinter = pawnStatus.Printers[1];
        ticketPrinterUIs[1].Populate();
        ticketPrinterUIs[2].ActivePrinter = pawnStatus.Printers[2];
        ticketPrinterUIs[2].Populate();
    }

    public void declinePurchase()
    {
        popUp.SetActive(false);
    }

    public void acceptPurchase()
    {
        ticketPrinterUIs[0].ActivePrinter.IsActive = true;
        ticketPrinterUIs[0].Populate();
        popUp.SetActive(false);
    }

    public void initializePopUp()
    {
        popUp.SetActive(true);
    }
}
