using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TicketPrinter
{
    // Interval and Batchsize are intrinsic value and can only be upgraded by going to the next printer type
    [SerializeField]
    private PrinterType printer;
    [SerializeField]
    private float interval;
    [SerializeField]
    private BigIntWrapper batchSize;

    // Capacity and Luck can be upgraded to a certain degree determined by printer type
    [SerializeField]
    private BigIntWrapper capacity;
    [SerializeField]
    private float luck;

    // Determines if the printer is unlocked and which ticket it prints
    [SerializeField]
    private bool isActive;
    [SerializeField]
    private BigIntWrapper purchasePrice;
    [SerializeField]
    private TicketType ticket;

    //NYI - for later development date: TBD
    [SerializeField]
    private bool isAttended;

    public float Interval
    {
        get { return interval; }
        set { interval = value; }
    }

    public BigIntWrapper BatchSize
    {
        get { return batchSize; }
        set { batchSize = value; }
    }

    public BigIntWrapper Capacity
    {
        get { return capacity; }
        set { capacity = value; }
    }

    public float Luck
    {
        get { return luck; }
        set { luck = value; }
    }

    public bool IsAttended
    {
        get { return isAttended; }
        set { isAttended = value; }
    }

    public bool IsActive
    {
        get { return isActive; }
        set { isActive = value; }
    }

    public BigIntWrapper PurchasePrice
    {
        get { return purchasePrice; }
        set { purchasePrice = value; }
    }

    public TicketType Ticket
    {
        get { return ticket; }
        set { ticket = value; }
    }

    public PrinterType Printer
    {
        get { return printer; }
        set { printer = value; }
    }

    [Serializable]
    public enum TicketType
    {
        None,
        DebugTicket,
        GenericTicket
    }

    [Serializable]
    public enum PrinterType
    {
        Receipt,
        InkJet,
        Laser,
        Office,
        Industrial,
        Compact3D,
        Industrial3D,
        Space
    }

    private TicketPrinter()
    {
        interval = 10;
        batchSize = new BigIntWrapper(1);
        capacity = new BigIntWrapper(100);
        luck = 0;
        isAttended = false;
        isActive = false;
        ticket = TicketType.None;
        printer = PrinterType.Receipt;
    }

    public static TicketPrinter createReceiptPrinter()
    {
        TicketPrinter printer = new TicketPrinter();

        return printer;
    }
}
