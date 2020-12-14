using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketConversionController : MonoBehaviour
{

    private ArcadeStatus arcadeStatus;

    public Text prizeTicketText;
    public Text debugTicketText;

    public Color defaultColor;
    public Color selectedColor;

    public Button incrementOneValue;
    public Button incrementTenValue;
    public Button incrementHundredValue;
    public Button incrementFivePercent;
    public Button incrementTwentyFivePercent;
    public Button incrementMax;

    private Button currentIncrementSelection;

    public List<TicketConvertUI> ticketConvertUIs;

    private List<TicketConvert> ticketConverts;

    // Start is called before the first frame update
    void Start()
    {
        arcadeStatus = ArcadeManager.readArcadeStatus();

        prizeTicketText.text = "" + arcadeStatus.prizeStatus.prizeTickets;
        debugTicketText.text = "" + arcadeStatus.debugStatus.gameTickets;

        incrementOneValue.image.color = defaultColor;
        incrementTenValue.image.color = defaultColor;
        incrementHundredValue.image.color = defaultColor;
        incrementFivePercent.image.color = defaultColor;
        incrementTwentyFivePercent.image.color = defaultColor;
        incrementMax.image.color = defaultColor;
        selectButton(incrementOneValue);

        initializeConversions();
    }

    private void initializeConversions()
    {
        ticketConverts = new List<TicketConvert>();
        if (arcadeStatus.debugStatus.isActive)
        {
            ticketConverts.Add(new TicketConvert("Sprites/Tickets/DebugTicket", "The Debugger", 1, 1, arcadeStatus.debugStatus));
        }
        // copy the previous if statement for future games
        // ...

        // Initalize the UIs
        initializeConvertUI(0, 2);
    }

    private void initializeConvertUI(int min, int max)
    {
        if(max - min + 1 != ticketConvertUIs.Count)
        {
            Debug.LogError("Not a valid range");
            return;
        }
        if(max >= ticketConverts.Count)
        {
            max = ticketConverts.Count - 1;
        }
        int i = 0;
        for(int j = min; j <= max; j++)
        {
            ticketConvertUIs[i].activeConvert = ticketConverts[j];
            ticketConvertUIs[i].populate();
            i++;
        }
        for(; i < ticketConvertUIs.Count; i++)
        {
            ticketConvertUIs[i].gameObject.SetActive(false);
        }

    }

    public void selectButton(Button button)
    {
        if(currentIncrementSelection != null)
        {
            currentIncrementSelection.image.color = defaultColor;
        }
        currentIncrementSelection = button;
        button.image.color = selectedColor;
    }

    public void incrementButton(TicketConvertUI ui)
    {
        increment(ui, true);
    }

    public void decrementButton(TicketConvertUI ui)
    {
        increment(ui, false);
    }

    private void increment(TicketConvertUI ui, bool increase)
    {
        int scalar = 1;
        if (!increase)
        {
            scalar = -1;
        }
        long incrementValue = 0;

        if (currentIncrementSelection == incrementOneValue)
        {
            incrementValue = scalar * 1;
        } 
        else if (currentIncrementSelection == incrementTenValue)
        {
            incrementValue = scalar * 10;
        }
        else if (currentIncrementSelection == incrementHundredValue)
        {
            incrementValue = scalar * 100;
        }
        else if (currentIncrementSelection == incrementFivePercent)
        {
            incrementValue = (long) (scalar * ((double) ui.activeConvert.status.gameTickets) * .05);
        }
        else if (currentIncrementSelection == incrementTwentyFivePercent)
        {
            incrementValue = (long)(scalar * ((double)ui.activeConvert.status.gameTickets) * .25);
        }
        else if (currentIncrementSelection == incrementMax)
        {
            incrementValue = scalar * ui.activeConvert.status.gameTickets;
        }

        ui.activeConvert.incrementCount(incrementValue);
        ui.populate();
    }
}
