using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintRoomController : MonoBehaviour
{
    private PawnStatus pawnStatus;
    private ArcadeStatus arcadeStatus;

    public Text walletText;

    public GameObject buyPopUp;
    public GameObject upgradePopUp;

    [Header("Ticket Printer UIs")]
    public List<TicketPrinterUI> ticketPrinterUIs;

    private List<TicketPrinter> printers;

    public void Activate()
    {
        pawnStatus = PawnManager.ReadPawnStatus();
        arcadeStatus = ArcadeManager.ReadArcadeStatus();
        printers = new List<TicketPrinter>();
        for (int i = 0; i < pawnStatus.Printers.Count; i++)
        {
            if (pawnStatus.Printers[i].IsActive)
            {
                printers.Add(pawnStatus.Printers[i]);
                ValidatePrinter(pawnStatus.Printers[i]);
            }
        }
        walletText.text = GameOperations.BigIntToString(pawnStatus.Money);

        UpdateFromReturn();
        // Temp until Scroll
        for (int i = 0; i < ticketPrinterUIs.Count; i++)
        {
            ticketPrinterUIs[i].ActivePrinter = pawnStatus.Printers[i];
            ticketPrinterUIs[i].Populate();
        }

        gameObject.SetActive(true);
        buyPopUp.SetActive(false);
        upgradePopUp.SetActive(false);

        GetComponent<PrinterTrader>().CloseTradeInPopUp();
    }

    /**
     * Validates that a printer is printing a valid type of tickets.
     */
    private void ValidatePrinter(TicketPrinter printer)
    {
        switch(printer.Ticket)
        {
            case TicketPrinter.TicketType.DebugTicket:
                if (!arcadeStatus.DebugStatus.IsActive)
                    ResetPrinterState(printer);
                break;
            case TicketPrinter.TicketType.QMGTicket:
                if (!arcadeStatus.QMGStatus.IsActive)
                    ResetPrinterState(printer);
                break;
            case TicketPrinter.TicketType.KNGTicket:
                if (!arcadeStatus.KNGStatus.IsActive)
                    ResetPrinterState(printer);
                break;
            case TicketPrinter.TicketType.BRDTicket:
                if (!arcadeStatus.BRDStatus.IsActive)
                    ResetPrinterState(printer);
                break;
            case TicketPrinter.TicketType.PrizeTicket:
                // check if we have the ability to print prize tickets???
                break;
            default:
                break;
        }
    }

    /**
     * Resets a printer to have no ticket type, base timer, and no tickets printed.
     */
    private void ResetPrinterState(TicketPrinter printer)
    {
        printer.Ticket = TicketPrinter.TicketType.None;
        printer.TicketsPrinted = 0;
        printer.PrintTimer = 0;
    }

    // This method is called every .02 seconds.
    public void FixedUpdate()
    {
        for(int i = 0; i < printers.Count; i++)
        {
            if (printers[i].Ticket != TicketPrinter.TicketType.None)
            {
                if (printers[i].UpdateTimer(Time.fixedDeltaTime))
                    Repopulate();
            } 
            else
            {
                printers[i].PrintTimer = 0f;
            }
        }
    }

    public void CollectTickets()
    {
        for(int i = 0; i < printers.Count; i++)
        {
            CollectTicketsFromPrinter(printers[i]);
        }
    }

    public void CollectTicketsFromPrinter(TicketPrinter printer)
    {
        TicketPrinter.TicketReturn ticketReturn = printer.CollectTickets();
        switch (ticketReturn.Type)
        {
            case TicketPrinter.TicketType.DebugTicket:
                arcadeStatus.DebugStatus.Tickets += ticketReturn.Number;
                break;
            case TicketPrinter.TicketType.QMGTicket:
                arcadeStatus.QMGStatus.Tickets += ticketReturn.Number;
                break;
            case TicketPrinter.TicketType.KNGTicket:
                arcadeStatus.KNGStatus.Tickets += ticketReturn.Number;
                break;
            case TicketPrinter.TicketType.BRDTicket:
                arcadeStatus.BRDStatus.Tickets += ticketReturn.Number;
                break;
            case TicketPrinter.TicketType.PrizeTicket:
                arcadeStatus.ArcadePrizeStatus.Tickets += ticketReturn.Number;
                break;
            default:
                break;
        }
        Repopulate();
    }

    public void Repopulate()
    {
        for (int i = 0; i < ticketPrinterUIs.Count; i++)
        {
            ticketPrinterUIs[i].Populate();
        }
        walletText.text = GameOperations.BigIntToString(pawnStatus.Money);
    }

    public void RecordTimeStamp()
    {
        PawnManager.RecordTimeStamp();
    }

    public void UpdateFromReturn()
    {
        if(pawnStatus.TimeStamp == -1)
        {
            return;
        }
        long timeAway = DateTimeOffset.Now.ToUnixTimeSeconds() - pawnStatus.TimeStamp;
        for(int i = 0; i < printers.Count; i++)
        {
            TicketPrinter printer = printers[i];
            if (printer.Ticket != TicketPrinter.TicketType.None)
            {
                float totalTime = (float)timeAway + printer.PrintTimer;
                // Sets the timer to the remaining time left
                printer.PrintTimer = totalTime % printer.BatchTime;
                int batches = (int)Mathf.Floor(totalTime / printer.BatchTime);
                printer.TicketsPrinted += printer.BatchSize * batches;
                if (printer.TicketsPrinted > printer.Capacity)
                {
                    printer.TicketsPrinted = printer.Capacity;
                }
            }
        }
    }
}
