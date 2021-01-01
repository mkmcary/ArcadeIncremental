﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject cabinetScreen;
    public GameObject debugMenu;
    public GameObject QMGMenu;

    private void Start()
    {
        HideAllScreens();

        // set the correct one to visible
        switch(ArcadeManager.activeScreen)
        {
            case ArcadeManager.menuScreen.cabinetScreen:
                cabinetScreen.SetActive(true);
                break;
            case ArcadeManager.menuScreen.debugGameMenu:
                debugMenu.SetActive(true);
                break;
            case ArcadeManager.menuScreen.QMGMenu:
                QMGMenu.SetActive(true);
                break;
        }
    }

    private void HideAllScreens()
    {
        List<GameObject> allScreens = new List<GameObject>();
        // set all to inactive
        GameObject[] menuScreens = GameObject.FindGameObjectsWithTag("MenuScreen");
        GameObject[] cabinetScreens = GameObject.FindGameObjectsWithTag("CabinetMenuScreen");
        allScreens.AddRange(menuScreens);
        allScreens.AddRange(cabinetScreens);

        foreach (GameObject go in allScreens)
        {
            go.SetActive(false);
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
