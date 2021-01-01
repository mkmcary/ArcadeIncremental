using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TicketPrinterUI : MonoBehaviour
{
    [Header("Controllers")]
    public PrinterBuyer printerBuyer;
    public PrinterModifier printerModifier;

    [Header("UI Elements")]
    public Color tintColor;
    public Image image;
    public Text trayText;
    public Slider slider;

    private TicketPrinter activePrinter;

    public TicketPrinter ActivePrinter
    {
        get { return activePrinter; }
        set { activePrinter = value; }
    }

    public void Populate()
    {
        image.sprite = activePrinter.GetPrinterSprite();
        trayText.text = GameOperations.BigIntToString(activePrinter.TicketsPrinted) + "/" + GameOperations.BigIntToString(activePrinter.Capacity);
        if (!activePrinter.IsActive)
        {
            image.color = tintColor;
            trayText.gameObject.SetActive(false);
            slider.gameObject.SetActive(false);
        } 
        else
        {
            image.color = Color.white;
            trayText.gameObject.SetActive(true);
            slider.gameObject.SetActive(true);
        }
        slider.maxValue = activePrinter.BatchTime;
    }

    private void FixedUpdate()
    {
        slider.value = activePrinter.PrintTimer;
    }

    public void OnPrinterClick()
    {
        if(activePrinter.IsActive)
        {
            // go to the upgrader
            printerModifier.InitializePopUp(activePrinter);
        } else
        {
            // go to the buyer
            printerBuyer.InitializePopUp(this);
        }
    }

    public static Sprite GetPrinterSprite(TicketPrinter printer)
    {
        Sprite sprite = null;
        switch (printer.Printer)
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
}
