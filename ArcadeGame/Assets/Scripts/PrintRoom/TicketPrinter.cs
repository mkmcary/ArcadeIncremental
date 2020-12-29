using System;
using System.Numerics;
using UnityEngine;

[Serializable]
public class TicketPrinter
{
    // Interval and Batchsize are intrinsic value and can only be upgraded by going to the next printer type
    [SerializeField]
    private PrinterType printer;
    [SerializeField]
    private float batchTime;
    [SerializeField]
    private BigIntWrapper batchSize;

    // Capacity and Luck can be upgraded to a certain degree determined by printer type
    [SerializeField]
    private BigIntWrapper capacity;
    [SerializeField]
    private BigIntWrapper capacityIncrement;
    [SerializeField]
    private int capacityCurrentLevel;
    [SerializeField]
    private int capacityMaxLevel;
    [SerializeField]
    private int luck;
    [SerializeField]
    private int luckIncrement;
    [SerializeField]
    private int luckCurrentLevel;
    [SerializeField]
    private int luckMaxLevel;

    // Determines if the printer is unlocked and which ticket it prints
    [SerializeField]
    private bool isActive;
    [SerializeField]
    private BigIntWrapper purchasePrice;
    [SerializeField]
    private TicketType ticket;
    [SerializeField]
    private BigIntWrapper ticketsPrinted;
    [SerializeField]
    private float printTimer;

    //NYI - for later development date: TBD
    [SerializeField]
    private bool isAttended;

    public float BatchTime
    {
        get { return batchTime; }
        set { batchTime = value; }
    }

    public BigInteger BatchSize
    {
        get { return batchSize.value; }
        set { batchSize.value = value; }
    }

    public BigInteger Capacity
    {
        get { return capacity.value; }
        set { capacity.value = value; }
    }

    public BigInteger CapacityIncrement
    {
        get { return capacityIncrement.value; }
        set { capacityIncrement.value = value; }
    }

    public int CapacityCurrentLevel
    {
        get { return capacityCurrentLevel; }
        set { capacityCurrentLevel = value; }
    }

    public int CapacityMaxLevel
    {
        get { return capacityMaxLevel; }
        set { capacityMaxLevel = value; }
    }

    public int Luck
    {
        get { return luck; }
        set { luck = value; }
    }

    public int LuckIncrement
    {
        get { return luckIncrement; }
        set { luckIncrement = value; }
    }

    public int LuckCurrentLevel
    {
        get { return luckCurrentLevel; }
        set { luckCurrentLevel = value; }
    }

    public int LuckMaxLevel
    {
        get { return luckMaxLevel; }
        set { luckMaxLevel = value; }
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

    public BigInteger PurchasePrice
    {
        get { return purchasePrice.value; }
        set { purchasePrice.value = value; }
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

    public BigInteger TicketsPrinted
    {
        get { return ticketsPrinted.value; }
        set { ticketsPrinted.value = value; }
    }

    public float PrintTimer
    {
        get { return printTimer; }
        set { printTimer = value; }
    }

    [Serializable]
    public enum TicketType
    {
        None,
        DebugTicket,
        PrizeTicket
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

    

    /**
     * Upgrades the capacity of this printer.
     * @return true if the printer has reached max level,
     *         and upgrading should be disabled.
     */
    public bool UpgradeCapacity()
    {
        
        this.capacity.value += this.capacityIncrement.value;
        this.capacityCurrentLevel++;
        return this.capacityCurrentLevel == this.capacityMaxLevel;
    }

    /**
     * Upgrades the luck of this printer.
     * @return true if the printer has reached max level,
     *         and upgrading should be disabled.
     */
    public bool UpgradeLuck()
    {
        this.luck += this.luckIncrement;
        this.luckCurrentLevel++;
        return this.luckCurrentLevel == this.luckMaxLevel;
    }

    public bool UpdateTimer(float timeSinceLast)
    {
        if(TicketsPrinted == Capacity)
        {
            return false;
        }
        PrintTimer += timeSinceLast;
        if(PrintTimer >= batchTime)
        {
            // The timer has reached batchTime. Now we print.
            TicketsPrinted += BatchSize;
            PrintTimer = 0.0f;
            if(TicketsPrinted > Capacity)
            {
                TicketsPrinted = Capacity;
            }
            return true;
        }
        return false;
    }

    public TicketReturn CollectTickets()
    {
        TicketReturn ticketReturn = new TicketReturn(TicketsPrinted, Ticket);
        TicketsPrinted = 0;
        return ticketReturn;
    }

    public class TicketReturn
    {
        public BigInteger Number { get; set; }
        public TicketType Type { get; set; }

        public TicketReturn(BigInteger number, TicketType type)
        {
            Number = number;
            Type = type;
        }
    }
    
    private TicketPrinter()
    {
        batchTime = 10;
        batchSize = new BigIntWrapper(1);
        capacity = new BigIntWrapper(100);
        capacityCurrentLevel = 0;
        luck = 0;
        luckCurrentLevel = 0;
        isAttended = false;
        isActive = false;
        ticket = TicketType.PrizeTicket;
        printer = PrinterType.Receipt;
        purchasePrice = new BigIntWrapper();
        ticketsPrinted = new BigIntWrapper();
        capacityIncrement = new BigIntWrapper();
    }

    public static TicketPrinter CreateReceiptPrinter()
    {
        TicketPrinter printer = new TicketPrinter();

        printer.capacityIncrement = new BigIntWrapper(10);
        printer.capacityMaxLevel = 5;
        printer.luckIncrement = 1;
        printer.luckMaxLevel = 5;

        return printer;
    }
}