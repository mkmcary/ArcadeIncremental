using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrinterModifier : MonoBehaviour
{
    private TicketPrinter printer;

    [Header("Pop Up")]
    public GameObject popUp;

    [Header("Sliders")]
    public SliderHelper batchSizeSlider;
    public SliderHelper batchTimeSlider;
    public SliderHelper capacitySlider;
    public SliderHelper luckSlider;

    [Header("Dropdown")]
    public Dropdown ticketDropdown;

    public void initializePopUp(TicketPrinter printer)
    {
        this.printer = printer;
        popUp.SetActive(true);
        initializeSliders();
        initializeTicketDropDown();
    }

    private void initializeSliders()
    {
        batchSizeSlider.LogarithmicPopulate(printer.BatchSize);
        batchTimeSlider.timePopulate(printer.BatchTime);
        capacitySlider.Populate(printer.Capacity, printer.CapacityCurrentLevel, printer.CapacityMaxLevel);
        luckSlider.Populate(printer.Luck, printer.LuckCurrentLevel, printer.LuckMaxLevel);
    }

    private void initializeTicketDropDown()
    {
        // come back to this
    }

    public void changeTicketType(int index)
    {
        // associate dropdown index with ticket type



        // collect all the tickets that were stored
    }

    public void showUpgradeCapacityPopUp()
    {
        // pop up to display how it would upgrade and how much it would cost
    }

    public void upgradeCapacity()
    {
        if(printer.upgradeCapacity())
        {
            // disable the button
        }

        // take money from player

        initializeSliders();
    }

    public void showUpgradeLuckPopUp()
    {
        // pop up to display how it would upgrade and how much it would cost
    }

    public void upgradeLuck()
    {
        if(printer.upgradeLuck())
        {
            // disable the button
        }

        // take money from player

        initializeSliders();
    }


}
