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

    public void InitializePopUp(TicketPrinter printer)
    {
        this.printer = printer;
        popUp.SetActive(true);
        InitializeSliders();
        InitializeTicketDropDown();
    }

    private void InitializeSliders()
    {
        batchSizeSlider.LogarithmicPopulate(printer.BatchSize);
        batchTimeSlider.TimePopulate(printer.BatchTime);
        capacitySlider.Populate(printer.Capacity, printer.CapacityCurrentLevel, printer.CapacityMaxLevel);
        luckSlider.Populate(printer.Luck, printer.LuckCurrentLevel, printer.LuckMaxLevel);
    }

    private void InitializeTicketDropDown()
    {
        // come back to this
    }

    public void ChangeTicketType(int index)
    {
        // associate dropdown index with ticket type



        // collect all the tickets that were stored
    }

    public void ShowUpgradeCapacityPopUp()
    {
        // pop up to display how it would upgrade and how much it would cost
    }

    public void UpgradeCapacity()
    {
        if(printer.UpgradeCapacity())
        {
            // disable the button
        }

        // take money from player

        InitializeSliders();
    }

    public void ShowUpgradeLuckPopUp()
    {
        // pop up to display how it would upgrade and how much it would cost
    }

    public void UpgradeLuck()
    {
        if(printer.UpgradeLuck())
        {
            // disable the button
        }

        // take money from player

        InitializeSliders();
    }


}
