using System;
using System.Numerics;
using UnityEngine;

[Serializable]
public class TicketPrinter
{
    public static readonly int[] PriceScaler = { 1, 2, 5 };

    // Tracks the order of the printers for price scaling
    [SerializeField]
    private int printerIndex;

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
    private BigIntWrapper capacityUpgradeCost;
    [SerializeField]
    private int capacityCurrentLevel;
    [SerializeField]
    private int capacityMaxLevel;
    [SerializeField]
    private int luck;
    [SerializeField]
    private int luckIncrement;
    [SerializeField]
    private BigIntWrapper luckUpgradeCost;
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

    public int PrinterIndex
    {
        get { return printerIndex; }
        set { printerIndex = value; }
    }

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

    public BigInteger CapacityUpgradeCost
    {
        get { return capacityUpgradeCost.value * PriceScaler[PrinterIndex]; }
        set { capacityUpgradeCost.value = value;  }
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

    public BigInteger LuckUpgradeCost
    {
        get { return luckUpgradeCost.value * PriceScaler[PrinterIndex]; }
        set { luckUpgradeCost.value = value; }
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
        get { return purchasePrice.value * PriceScaler[PrinterIndex]; }
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
        QMGTicket,
        PrizeTicket
    }

    [Serializable]
    public enum PrinterType
    {
        Receipt,
        Inkjet,
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
    public void UpgradeCapacity()
    {
        Capacity += CapacityIncrement;
        CapacityCurrentLevel++;
        CapacityUpgradeCost *= 2;
    }

    /**
     * Upgrades the luck of this printer.
     * @return true if the printer has reached max level,
     *         and upgrading should be disabled.
     */
    public void UpgradeLuck()
    {
        Luck += LuckIncrement;
        LuckCurrentLevel++;
        LuckUpgradeCost *= 2;
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
            if (Ticket != TicketType.None)
            {
                ApplyLuck();

                PrintTimer = 0.0f;
                if (TicketsPrinted > Capacity)
                {
                    TicketsPrinted = Capacity;
                }
                return true;
            }
        }
        return false;
    }

    private void ApplyLuck()
    {
        BigInteger amtToAdd = 0;
        amtToAdd += BatchSize;
        // calculate how many to add, given luck (Better way to do this?????)
        if (BatchSize < 100)
        {
            // at low sizes
            int random = UnityEngine.Random.Range(0, 100);
            if (random < Luck)
            {
                amtToAdd *= 2;
            }
        }
        else
        {
            // at large sizes
            int percentage = UnityEngine.Random.Range(0, Luck) + 100;
            amtToAdd *= percentage;
            amtToAdd /= 100;
        }

        Debug.Log("Luck was applied, you should have gotten " + GameOperations.BigIntToString(amtToAdd));
        TicketsPrinted += amtToAdd;
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

    public Sprite GetPrinterSprite()
    {
        Sprite sprite = null;
        switch (Printer)
        {
            case TicketPrinter.PrinterType.Receipt:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Receipt");
                break;
            case TicketPrinter.PrinterType.Inkjet:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Inkjet");
                break;
            case TicketPrinter.PrinterType.Laser:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Laser");
                break;
            case TicketPrinter.PrinterType.Office:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Office");
                break;
            case TicketPrinter.PrinterType.Industrial:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Industrial");
                break;
            case TicketPrinter.PrinterType.Compact3D:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Compact3D");
                break;
            case TicketPrinter.PrinterType.Industrial3D:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Industrial3D");
                break;
            case TicketPrinter.PrinterType.Space:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Space");
                break;
            default:
                sprite = GameOperations.LoadSpriteFromPath("Sprites/Printers/Receipt");
                break;
        }
        return sprite;
    }

    public TicketPrinter GetNextPrinter()
    {
        switch(Printer)
        {
            case TicketPrinter.PrinterType.Receipt:
                return CreateTemporaryInkjet();
            case TicketPrinter.PrinterType.Inkjet:
                return CreateTemporaryLaser();
            case TicketPrinter.PrinterType.Laser:
                return null;
            case TicketPrinter.PrinterType.Office:
                return null;
            case TicketPrinter.PrinterType.Industrial:
                return null;
            case TicketPrinter.PrinterType.Compact3D:
                return null;
            case TicketPrinter.PrinterType.Industrial3D:
                return null;
            case TicketPrinter.PrinterType.Space:
                return null;
            default:
                return null;
        }
    }

    public void TradeIn()
    {
        switch (Printer)
        {
            case TicketPrinter.PrinterType.Receipt:
                UpgradeToInkjet();
                break;
            case TicketPrinter.PrinterType.Inkjet:
                break;
            case TicketPrinter.PrinterType.Laser:
                break;
            case TicketPrinter.PrinterType.Office:
                break;
            case TicketPrinter.PrinterType.Industrial:
                break;
            case TicketPrinter.PrinterType.Compact3D:
                break;
            case TicketPrinter.PrinterType.Industrial3D:
                break;
            case TicketPrinter.PrinterType.Space:
                break;
            default:
                break;
        }
    }

    private TicketPrinter()
    {
        ticket = TicketType.None;
        isActive = false;
        isAttended = false;
        ticketsPrinted = new BigIntWrapper();

        printer = PrinterType.Receipt;
        purchasePrice = new BigIntWrapper();

        batchTime = 0;
        batchSize = new BigIntWrapper();

        capacity = new BigIntWrapper();
        capacityCurrentLevel = 0;
        capacityMaxLevel = 1;
        capacityIncrement = new BigIntWrapper();
        capacityUpgradeCost = new BigIntWrapper();

        luck = 0;
        luckCurrentLevel = 0;
        luckMaxLevel = 1;
        luckIncrement = 1;
        luckUpgradeCost = new BigIntWrapper();
    }

    public static TicketPrinter CreateReceiptPrinter()
    {
        TicketPrinter printer = new TicketPrinter
        {
            Printer = PrinterType.Receipt,
            PurchasePrice = 100,

            BatchTime = 30,
            BatchSize = 1,

            Capacity = 50,
            CapacityIncrement = 10,
            CapacityMaxLevel = 5,
            CapacityUpgradeCost = 10,

            Luck = 0,
            LuckIncrement = 1,
            LuckMaxLevel = 5,
            LuckUpgradeCost = 1
        };

        return printer;
    }

    public static TicketPrinter CreateTemporaryInkjet()
    {
        TicketPrinter printer = new TicketPrinter
        {
            Printer = PrinterType.Inkjet,
            PurchasePrice = 500,

            BatchTime = 60,
            BatchSize = 25,
            Capacity = 250,
            Luck = 3,
        };

        return printer;
    }

    public void UpgradeToInkjet()
    {
        Printer = PrinterType.Inkjet;
        Ticket = TicketType.None;
        PurchasePrice = 500;

        BatchTime = 60;
        BatchSize = 25;

        Capacity = 250;
        CapacityIncrement = 50;
        CapacityCurrentLevel = 0;
        CapacityMaxLevel = 10;
        CapacityUpgradeCost = 100;

        Luck = 3;
        LuckIncrement = 1;
        LuckCurrentLevel = 0;
        LuckMaxLevel = 10;
        LuckUpgradeCost = 3;
    }

    public static TicketPrinter CreateTemporaryLaser()
    {
        TicketPrinter printer = new TicketPrinter
        {
            Printer = PrinterType.Laser,
            PurchasePrice = 1000000,

            BatchTime = 90,
            BatchSize = 100,
            Capacity = 400,
            Luck = 10,
        };

        return printer;
    }

    public void UpgradeToLaser()
    {
        Printer = PrinterType.Laser;
        Ticket = TicketType.None;
        PurchasePrice = 1000000;

        BatchTime = 90;
        BatchSize = 100;

        Capacity = 400;
        CapacityIncrement = 100;
        CapacityCurrentLevel = 0;
        CapacityMaxLevel = 10;
        CapacityUpgradeCost = 100;

        Luck = 10;
        LuckIncrement = 1;
        LuckCurrentLevel = 0;
        LuckMaxLevel = 10;
        LuckUpgradeCost = 5;
    }
}