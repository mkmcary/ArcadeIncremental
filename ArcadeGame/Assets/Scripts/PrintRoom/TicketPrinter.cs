﻿using System;
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

    //NYI - for later development date: TBD
    [SerializeField]
    private bool isAttended;

    public float BatchTime
    {
        get { return batchTime; }
        set { batchTime = value; }
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

    public BigIntWrapper CapacityIncrement
    {
        get { return capacityIncrement; }
        set { capacityIncrement = value; }
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
        batchTime = 30;
        batchSize = new BigIntWrapper(1);
        capacity = new BigIntWrapper(100);
        capacityCurrentLevel = 0;
        luck = 0;
        luckCurrentLevel = 0;
        isAttended = false;
        isActive = false;
        ticket = TicketType.None;
        printer = PrinterType.Receipt;
    }

    public static TicketPrinter createReceiptPrinter()
    {
        TicketPrinter printer = new TicketPrinter();

        printer.capacityIncrement = new BigIntWrapper(10);
        printer.capacityMaxLevel = 5;
        printer.luckIncrement = 1;
        printer.luckMaxLevel = 5;

        return printer;
    }

    /**
     * Upgrades the capacity of this printer.
     * @return true if the printer has reached max level,
     *         and upgrading should be disabled.
     */
    public bool upgradeCapacity()
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
    public bool upgradeLuck()
    {
        this.luck += this.luckIncrement;
        this.luckCurrentLevel++;
        return this.luckCurrentLevel == this.luckMaxLevel;
    }
}
