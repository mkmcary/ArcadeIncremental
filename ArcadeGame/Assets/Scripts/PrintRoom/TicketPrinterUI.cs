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

    private TicketPrinter activePrinter;

    public TicketPrinter ActivePrinter
    {
        get { return activePrinter; }
        set { activePrinter = value; }
    }

    public void Populate()
    {
        image.sprite = GetPrinterSprite();
        trayText.text = GameOperations.bigIntToString(activePrinter.TicketsPrinted) + "/" + GameOperations.bigIntToString(activePrinter.Capacity);
        if (!activePrinter.IsActive)
        {
            image.color = tintColor;
            trayText.gameObject.SetActive(false);
        } 
        else
        {
            image.color = Color.white;
            trayText.gameObject.SetActive(true);
        }
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

    private Sprite GetPrinterSprite()
    {
        Sprite sprite = null;
        switch (activePrinter.Printer)
        {
            case TicketPrinter.PrinterType.Receipt:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/Receipt");
                break;
            case TicketPrinter.PrinterType.InkJet:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/InkJet");
                break;
            case TicketPrinter.PrinterType.Laser:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/Laser");
                break;
            case TicketPrinter.PrinterType.Office:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/Office");
                break;
            case TicketPrinter.PrinterType.Industrial:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/Industrial");
                break;
            case TicketPrinter.PrinterType.Compact3D:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/Compact3D");
                break;
            case TicketPrinter.PrinterType.Industrial3D:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/Industrial3D");
                break;
            case TicketPrinter.PrinterType.Space:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/Space");
                break;
            default:
                sprite = GameOperations.loadSpriteFromPath("Sprites/Printers/Receipt");
                break;
        }
        return sprite;
    }
}
