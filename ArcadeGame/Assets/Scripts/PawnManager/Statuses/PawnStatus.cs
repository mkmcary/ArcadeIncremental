using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PawnStatus
{
    [SerializeField]
    private BigIntWrapper money;
    [SerializeField]
    private List<TicketPrinter> printers;

    private string[] initialPrinterPrices = { "100", "3000", "99999999999" };

    public BigIntWrapper Money
    {
        get { return money; }
        set { money = value; }
    }

    public List<TicketPrinter> Printers
    {
        get { return printers; }
        set { printers = value; }
    }

    public PawnStatus()
    {
        money = new BigIntWrapper();
        printers = new List<TicketPrinter>();

        TicketPrinter printer = null;
        for (int i = 0; i < initialPrinterPrices.Length; i++)
        {
            printer = TicketPrinter.createReceiptPrinter();
            printer.PurchasePrice = new BigIntWrapper(initialPrinterPrices[i]);
            printers.Add(printer);
        }
    }
}
