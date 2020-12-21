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
        printers = new List<TicketPrinter>
        {
            TicketPrinter.createReceiptPrinter()
        };
    }
}
