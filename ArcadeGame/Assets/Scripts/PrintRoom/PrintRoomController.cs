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
        arcadeStatus = ArcadeManager.readArcadeStatus();
        printers = new List<TicketPrinter>();
        for (int i = 0; i < pawnStatus.Printers.Count; i++)
        {
            if (pawnStatus.Printers[i].IsActive)
            {
                printers.Add(pawnStatus.Printers[i]);
            }
        }
        walletText.text = GameOperations.bigIntToString(pawnStatus.Money);

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
    }

    // This method is called every .02 seconds.
    public void FixedUpdate()
    {
        for(int i = 0; i < printers.Count; i++)
        {
            if (printers[i].UpdateTimer(Time.fixedDeltaTime))
            {
                Repopulate();
            }
            
        }
    }

    public void CollectTickets()
    {
        for(int i = 0; i < printers.Count; i++)
        {
            TicketPrinter.TicketReturn ticketReturn = printers[i].CollectTickets();
            switch (ticketReturn.Type)
            {
                case TicketPrinter.TicketType.DebugTicket:
                    arcadeStatus.DebugStatus.Tickets += ticketReturn.Number;
                    break;
                case TicketPrinter.TicketType.PrizeTicket:
                    arcadeStatus.ArcadePrizeStatus.Tickets += ticketReturn.Number;
                    break;
                default:
                    break;
            }
        }
        Repopulate();
    }

    private void Repopulate()
    {
        for (int i = 0; i < ticketPrinterUIs.Count; i++)
        {
            ticketPrinterUIs[i].Populate();
        }
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
            float totalTime = (float) timeAway + printer.PrintTimer;
            // Sets the timer to the remaining time left
            printer.PrintTimer = totalTime % printer.BatchTime;
            int batches = (int) Mathf.Floor( totalTime / printer.BatchTime);
            printer.TicketsPrinted += printer.BatchSize * batches;
            if(printer.TicketsPrinted > printer.Capacity)
            {
                printer.TicketsPrinted = printer.Capacity;
            }
        }
    }
}
