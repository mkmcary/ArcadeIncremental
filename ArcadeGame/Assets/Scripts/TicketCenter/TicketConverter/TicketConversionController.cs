using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;

public class TicketConversionController : MonoBehaviour
{
    // ArcadeStatus
    private ArcadeStatus arcadeStatus;

    // Ticket Texts
    public Text prizeTicketText;
    public Text debugTicketText;

    // Colors for the increment buttons
    public Color defaultColor;
    public Color selectedColor;

    // Increment Buttons
    public Button incrementOneValue;
    public Button incrementTenValue;
    public Button incrementHundredValue;
    public Button incrementFivePercent;
    public Button incrementTwentyFivePercent;
    public Button incrementMax;

    // Currently Selected Increment
    private Button currentIncrementSelection;

    // Ticket Conversion UIs
    public List<TicketConvertUI> ticketConvertUIs;

    // All the ticket conversions
    private List<TicketConvert> ticketConverts;

    // Conversion Pop Up Data.
    public GameObject convertPopUp;
    public Text conversionText;
    private BigInteger prizeTicketsToReceive;

    // Scroll Data
    public Button scrollUpButton;
    public Button scrollDownButton;
    private int currentSetIndex;

    // Start is called before the first frame update
    void Start()
    {
        arcadeStatus = ArcadeManager.readArcadeStatus();

        setWalletText();

        incrementOneValue.image.color = defaultColor;
        incrementTenValue.image.color = defaultColor;
        incrementHundredValue.image.color = defaultColor;
        incrementFivePercent.image.color = defaultColor;
        incrementTwentyFivePercent.image.color = defaultColor;
        incrementMax.image.color = defaultColor;
        selectButton(incrementOneValue);

        initializeConversions();

        convertPopUp.SetActive(false);
        prizeTicketsToReceive = 0;
    }

    private void setWalletText()
    {
        prizeTicketText.text = ArcadeManager.bigIntToString(arcadeStatus.prizeStatus.tickets.value);
        debugTicketText.text = ArcadeManager.bigIntToString(arcadeStatus.debugStatus.tickets.value);
    }

    /**
     * Used to create the ticket conversions and load UI.
     */
    private void initializeConversions()
    {
        ticketConverts = new List<TicketConvert>();
        if (arcadeStatus.debugStatus.isActive)
        {
            ticketConverts.Add(new TicketConvert("Sprites/Tickets/DebugTicket", "The Debugger", 6, 1, arcadeStatus.debugStatus));
        }
        // copy the previous if statement for future games
        // ...

        // Initalize the UIs
        currentSetIndex = 0;
        initializeConvertUI(0);
    }

    /**
     * Loads a new set of Ticket Conversions into the UI between indices min and max inclusive.
     * @param min the minimum index.
     */
    private void initializeConvertUI(int min)
    {
        int max = min + ticketConvertUIs.Count - 1;

        if (max - min + 1 != ticketConvertUIs.Count)
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

        // initialize the scroll buttons
        if(min == 0)
        {
            scrollUpButton.gameObject.SetActive(false);
        }
        if(max == ticketConverts.Count - 1)
        {
            scrollDownButton.gameObject.SetActive(false);
        }

    }

    /**
     * Selects an increment button.
     * @param button the button to select.
     */
    public void selectButton(Button button)
    {
        if(currentIncrementSelection != null)
        {
            currentIncrementSelection.image.color = defaultColor;
        }
        currentIncrementSelection = button;
        button.image.color = selectedColor;
    }

    /**
     * Called by a ticket conversion increment button.
     * @param ui the TicketConvertUI that should be incremented.
     */
    public void incrementButton(TicketConvertUI ui)
    {
        increment(ui, true);
    }


    /**
     * Called by a ticket conversion decrement button.
     * @param ui the TicketConvertUI that should be decremented.
     */
    public void decrementButton(TicketConvertUI ui)
    {
        increment(ui, false);
    }


    /**
     * Helper method for increment/decrement.
     * @param ui the TicketConvertUI that should be altered.
     * @param increase should be true to increase the value, or false to decrease it.
     */
    private void increment(TicketConvertUI ui, bool increase)
    {
        int scalar = 1;
        if (!increase)
        {
            scalar = -1;
        }
        BigInteger incrementValue = 0;

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
            incrementValue = (scalar * (ui.activeConvert.status.tickets.value) / 20);
        }
        else if (currentIncrementSelection == incrementTwentyFivePercent)
        {
            incrementValue = (scalar * (ui.activeConvert.status.tickets.value) / 4);
        }
        else if (currentIncrementSelection == incrementMax)
        {
            incrementValue = scalar * ui.activeConvert.status.tickets.value;
        }

        // actually increment the value
        ui.activeConvert.incrementCount(incrementValue);
        ui.populate();
    }

    public void initializePopUp()
    {
        convertPopUp.SetActive(true);

        // calculate amount to receive
        prizeTicketsToReceive = 0;
        for(int i = 0; i < ticketConverts.Count; i++)
        {
            BigInteger amountToTurnIn = ticketConverts[i].getCount();

            amountToTurnIn -= amountToTurnIn % ticketConverts[i].inputAmount;
            BigInteger numberOfConversions = amountToTurnIn / ticketConverts[i].inputAmount;
            prizeTicketsToReceive += numberOfConversions * ticketConverts[i].outputAmount;
        }

        conversionText.text = "You Will Receive:\n" + ArcadeManager.bigIntToString(prizeTicketsToReceive) + "\nPrize Tickets";
    }

    public void convert()
    {
        for(int i = 0; i < ticketConverts.Count; i++)
        {
            BigInteger amountToTurnIn = ticketConverts[i].getCount();
            ticketConverts[i].status.tickets.value -= (amountToTurnIn - (amountToTurnIn % ticketConverts[i].inputAmount));

            ticketConverts[i].resetCount();
        }
        initializeConvertUI(currentSetIndex);

        arcadeStatus.prizeStatus.tickets.value += prizeTicketsToReceive;
        prizeTicketsToReceive = 0;
        setWalletText();
        closePopUp();
    }

    public void closePopUp()
    {
        convertPopUp.SetActive(false);
    }

    public void OnApplicationQuit()
    {
        ArcadeManager.writeArcadeStatus(arcadeStatus);
    }

    public void scrollUp()
    {
        if (currentSetIndex == 0)
        {
            Debug.LogError("Should not be scrolling up.");
            return;
        }

        currentSetIndex -= ticketConvertUIs.Count;
        initializeConvertUI(currentSetIndex);

        if (currentSetIndex == 0)
        {
            // if at top, disable up arrow
            scrollUpButton.gameObject.SetActive(false);
        } else { 
            // else, make sure it is available
            scrollUpButton.gameObject.SetActive(true);
        }
        // always make sure scroll down is available
        scrollDownButton.gameObject.SetActive(true);
    }

    public void scrollDown()
    {
        if (currentSetIndex + ticketConvertUIs.Count >= ticketConverts.Count)
        {
            Debug.LogError("Should not be scrolling down.");
            return;
        }

        currentSetIndex += ticketConvertUIs.Count;
        initializeConvertUI(currentSetIndex);

        if (currentSetIndex + ticketConvertUIs.Count >= ticketConverts.Count)
        {
            // if at bottom, disable down arrow
            scrollDownButton.gameObject.SetActive(false);
        }
        else
        {
            // else, make sure it is available
            scrollDownButton.gameObject.SetActive(true);
        }
        // always make sure scroll up is available
        scrollUpButton.gameObject.SetActive(true);
    }
}
