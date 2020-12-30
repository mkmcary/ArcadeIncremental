using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterModifier : MonoBehaviour
{
    private TicketPrinter printer;
    private PawnStatus pawnStatus;

    [Header("Print Room Controller")]
    public PrintRoomController printRoomController;

    [Header("Pop Up")]
    public GameObject popUp;
    public Image printerImage;

    [Header("Upgrade Bars")]
    public UpgradeBarHelper batchSizeBar;
    public UpgradeBarHelper batchTimeBar;
    public UpgradeBarHelper capacityBar;
    public UpgradeBarHelper luckBar;

    [Header("Dropdown")]
    public Dropdown ticketDropdown;

    [Header("Upgrade Buttons")]
    public Button upgradeBatchSizeButton;
    public Text upgradeBatchSizeText;
    public Button upgradeCapacityButton;
    public Text upgradeCapacityText;
    public Button upgradeLuckButton;
    public Text upgradeLuckText;

    [Header("Pop Up Progress Bar")]
    public Slider popUpProgressBar;
    public Text popUpProgressText;

    [Header("Trade In")]
    public Image currentPrinterImage;
    public Image nextPrinterImage;

    private List<TicketAssociation> ticketAssociations;

    public void InitializePopUp(TicketPrinter printer)
    {
        pawnStatus = PawnManager.ReadPawnStatus();
        this.printer = printer;
        popUp.SetActive(true);
        InitializeUpgradeBars();

        printerImage.sprite = printer.GetPrinterSprite();
        popUpProgressBar.maxValue = printer.BatchTime;

        // set up upgrade buttons
        UpdateUpgradeButtons();

        InitializeTicketDropDown();
    }

    private void InitializeUpgradeBars()
    {
        batchSizeBar.LogarithmicPopulate(printer.BatchSize);
        batchTimeBar.TimePopulate(printer.BatchTime);
        capacityBar.Populate(printer.Capacity, printer.CapacityCurrentLevel, printer.CapacityMaxLevel);
        luckBar.Populate(printer.Luck, printer.LuckCurrentLevel, printer.LuckMaxLevel);
    }

    private void InitializeTicketDropDown()
    {
        ArcadeStatus arcadeStatus = ArcadeManager.ReadArcadeStatus();

        ticketAssociations = new List<TicketAssociation>();
        int index = 0;
        // add the none option
        TicketAssociation ticketAssociation = new TicketAssociation(index, TicketPrinter.TicketType.None, "None", "Sprites/Currency/Tickets/NoTicket");
        ticketAssociations.Add(ticketAssociation);
        index++;

        // cabinet tickets
        if (arcadeStatus.DebugStatus.IsActive)
        {
            ticketAssociation = new TicketAssociation(index, TicketPrinter.TicketType.DebugTicket, "Debug Ticket", "Sprites/Currency/Tickets/DebugTicket");
            ticketAssociations.Add(ticketAssociation);
            index++;
        }

        // ...
        // Copy the above for each cabinet ticket
        // ...

        // prize tickets
        if (true) // figure out condition upon which players can print prize tickets
        {
            ticketAssociation = new TicketAssociation(index, TicketPrinter.TicketType.PrizeTicket, "Prize Ticket", "Sprites/Currency/Tickets/PrizeTicket");
            ticketAssociations.Add(ticketAssociation);
            index++;
        }

        List<Dropdown.OptionData> options = new List<Dropdown.OptionData>();
        TicketAssociation selection = null;

        for(int i = 0; i < ticketAssociations.Count; i++)
        {
            // construct options
            TicketAssociation curr = ticketAssociations[i];
            options.Add(new Dropdown.OptionData(curr.label, GameOperations.loadSpriteFromPath(curr.imagePath)));

            // find the one that matches our ticket type
            if(curr.ticketType == printer.Ticket)
            {
                selection = curr;
            }
        }

        // set the list of options
        ticketDropdown.options = options;

        // set to the current type
        ticketDropdown.SetValueWithoutNotify(selection.listIndex);
        ticketDropdown.RefreshShownValue();
    }

    private void UpdateUpgradeButtons()
    {
        upgradeBatchSizeText.text = "N/A";
        upgradeLuckText.text = GameOperations.bigIntToString(printer.LuckUpgradeCost);
        upgradeCapacityText.text = GameOperations.bigIntToString(printer.CapacityUpgradeCost);

        // batch size
        upgradeBatchSizeButton.gameObject.SetActive(false);

        // luck
        if (printer.LuckCurrentLevel == printer.LuckMaxLevel && pawnStatus.Money >= printer.LuckUpgradeCost)
        {
            upgradeLuckButton.interactable = false;
            if(printer.LuckCurrentLevel == printer.LuckMaxLevel)
            {
                upgradeLuckText.text = "Max Level";
            }
        }
        else
        {
            upgradeLuckButton.interactable = true;
        }

        // capacity
        if (printer.CapacityCurrentLevel == printer.CapacityMaxLevel && pawnStatus.Money >= printer.CapacityUpgradeCost)
        {
            upgradeCapacityButton.interactable = false;
            if (printer.CapacityCurrentLevel == printer.CapacityMaxLevel)
            {
                upgradeCapacityText.text = "Max Level";
            }
        }
        else
        {
            upgradeCapacityButton.interactable = true;
        }

        // Trade In Updates
        currentPrinterImage.sprite = printer.GetPrinterSprite();
        nextPrinterImage.sprite = printer.GetNextPrinter().GetPrinterSprite();
    }

    public void ChangeTicketType(int index)
    {
        // associate dropdown index with ticket type
        TicketAssociation ticketAssociation = null;
        for (int i = 0; i < ticketAssociations.Count; i++)
        {
            if (ticketAssociations[i].listIndex == index)
            {
                ticketAssociation = ticketAssociations[i];
                break;
            }
        }

        if (ticketAssociation == null)
        {
            Debug.LogError("No Ticket Association was Found.");
        }

        // collect all the tickets that were stored
        printRoomController.CollectTicketsFromPrinter(printer);

        // update the ticket type
        printer.Ticket = ticketAssociation.ticketType;
    }

    public void UpgradeCapacity()
    {
        // transaction
        pawnStatus.Money -= printer.CapacityUpgradeCost;
        printer.UpgradeCapacity();

        // ui updates
        InitializeUpgradeBars();
        UpdateUpgradeButtons();
        printRoomController.Repopulate();
    }

    public void UpgradeLuck()
    {
        // transaction
        pawnStatus.Money -= printer.LuckUpgradeCost;
        printer.UpgradeLuck();

        // ui updates
        InitializeUpgradeBars();
        UpdateUpgradeButtons();
        printRoomController.Repopulate();
    }

    public void InitializeTradeInPopUp()
    {
        GetComponent<PrinterTrader>().InitializePopUp(printer);
    }

    public class TicketAssociation
    {
        // Association for index and ticket type
        public int listIndex;
        public TicketPrinter.TicketType ticketType;

        // Dropdown UI data
        public string label;
        public string imagePath;

        public TicketAssociation(int index, TicketPrinter.TicketType type, string label, string path)
        {
            this.listIndex = index;
            this.ticketType = type;
            this.label = label;
            this.imagePath = path;
        }
    }

    private void FixedUpdate()
    {
        if(printer != null)
        {
            popUpProgressBar.value = printer.PrintTimer;
            float timer = Mathf.Floor(printer.BatchTime - printer.PrintTimer);
            popUpProgressText.text = timer.ToString("N0") + " S";
        }
    }

}
