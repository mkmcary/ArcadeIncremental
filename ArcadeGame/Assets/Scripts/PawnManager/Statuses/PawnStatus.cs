using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[Serializable]
public class PawnStatus
{
    [SerializeField]
    private BigIntWrapper money;
    [SerializeField]
    private long timeStamp;
    [SerializeField]
    private List<TicketPrinter> printers;

    private string[] initialPrinterPrices = { "100", "3000", "99999999999" };

    public BigInteger Money
    {
        get { return money.value; }
        set { money.value = value; }
    }

    public long TimeStamp
    {
        get { return timeStamp; }
        set { timeStamp = value; }
    }

    public List<TicketPrinter> Printers
    {
        get { return printers; }
        set { printers = value; }
    }

    public PawnStatus()
    {
        money = new BigIntWrapper();
        timeStamp = -1;
        printers = new List<TicketPrinter>();

        TicketPrinter printer = null;
        for (int i = 0; i < initialPrinterPrices.Length; i++)
        {
            printer = TicketPrinter.CreateReceiptPrinter();
            printer.PurchasePrice = BigInteger.Parse(initialPrinterPrices[i]);
            printers.Add(printer);
        }
    }
}
