using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuOverlayController : MonoBehaviour
{
    [Header("Categories of the Overlay")]
    public GameObject overlayWhenClosed;
    public GameObject overlayWhenOpen;

    [Header("Valid Landing Locations")]
    public GameObject ticketCenterMenu;
    public GameObject pawnShopMenu;

    // Start is called before the first frame update
    void Start()
    {
        CloseMenu();
    }

    public void OpenMenu()
    {
        overlayWhenClosed.SetActive(false);
        overlayWhenOpen.SetActive(true);
    }

    public void CloseMenu()
    {
        overlayWhenClosed.SetActive(true);
        overlayWhenOpen.SetActive(false);
    }

    public void GoToTicketCenter()
    {
        MenuManager.HideAllScreens();
        ticketCenterMenu.GetComponent<TicketMenuController>().Activate();
        CloseMenu();
    }

    public void GoToPawnShop()
    {
        MenuManager.HideAllScreens();
        pawnShopMenu.GetComponent<MoneyMenuController>().Activate();
        CloseMenu();
    }

}
