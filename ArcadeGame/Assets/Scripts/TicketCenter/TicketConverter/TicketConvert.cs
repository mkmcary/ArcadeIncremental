using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class TicketConvert
{
    // Data for a ticket convert
    public string sprite;
    public string gameName;
    public int inputAmount;
    public int outputAmount;
    public CabinetStatus status;

    private long count;

    public TicketConvert()
    {
        sprite = null;
        gameName = null;
        inputAmount = 0;
        outputAmount = 0;
        status = null;
        count = 0;
    }

    public TicketConvert(string sprite, string gameName, int inputAmount, int outputAmount, CabinetStatus status)
    {
        this.sprite = sprite;
        this.gameName = gameName;
        this.inputAmount = inputAmount;
        this.outputAmount = outputAmount;
        this.status = status;
        count = 0;
    }

    public void incrementCount(long increment)
    {
        count += increment;
        if(count < 0)
        {
            count = 0;
        } else if(count > status.tickets)
        {
            count = status.tickets;
        }
    }

    public long getCount()
    {
        return count;
    }

    public void resetCount()
    {
        this.count = 0;
    }
}
