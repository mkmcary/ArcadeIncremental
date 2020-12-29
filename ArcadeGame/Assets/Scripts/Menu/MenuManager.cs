using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public List<GameObject> allScreens;

    public GameObject cabinetScreen;
    public GameObject debugMenu;

    private void Start()
    {
        // set all to inactive
        foreach(GameObject go in allScreens) {
            go.SetActive(false);
        }

        // set the correct one to visible
        switch(ArcadeManager.activeScreen)
        {
            case ArcadeManager.menuScreen.cabinetScreen:
                cabinetScreen.SetActive(true);
                break;
            case ArcadeManager.menuScreen.debugGameMenu:
                debugMenu.SetActive(true);
                break;
        }
    }

    private void OnApplicationQuit()
    {
        ArcadeManager.WriteArcadeStatus();
        PawnManager.RecordTimeStamp();
        PawnManager.WritePawnStatus();
    }

    public void updateActiveScreen(int screen)
    {
        ArcadeManager.activeScreen = (ArcadeManager.menuScreen)screen;
    }
}
