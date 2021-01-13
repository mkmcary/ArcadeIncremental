using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CabinetController : MonoBehaviour
{
    private ArcadeStatus arcadeStatus;

    public GameObject leftCabinet;
    public GameObject centerCabinet;
    public GameObject rightCabinet;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public Text machineLabel;

    private List<ArcadeCabinet> arcadeCabinets;

    private int currentCabinet;

    //Contains the menus for the arcade games
    public List<GameObject> cabinetMenus;

    public void Activate()
    {
        currentCabinet = 0;

        CreateCabinets();
        SetMachine();
        gameObject.SetActive(true);
    }

    public void RotateLeft()
    {
        // a - b * FLOOR(a/b) C# modulo formula
        currentCabinet = Modulo(currentCabinet + 1, arcadeCabinets.Count);
        SetMachine();
    }

    public void RotateRight()
    {
        // a - b * FLOOR(a/b) C# modulo formula
        currentCabinet = Modulo(currentCabinet - 1, arcadeCabinets.Count);
        SetMachine();
    }

    private void SetMachine()
    {
        centerCabinet.GetComponent<Image>().sprite = GameOperations.LoadSpriteFromPath(arcadeCabinets[currentCabinet].sprite);
        SetLabel();
        if (arcadeCabinets.Count > 1)
        {
            leftCabinet.GetComponent<Image>().sprite = GameOperations.LoadSpriteFromPath(arcadeCabinets[Modulo(currentCabinet - 1, arcadeCabinets.Count)].sprite);
            rightCabinet.GetComponent<Image>().sprite = GameOperations.LoadSpriteFromPath(arcadeCabinets[Modulo(currentCabinet + 1, arcadeCabinets.Count)].sprite);
            leftCabinet.SetActive(true);
            rightCabinet.SetActive(true);
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
        }
        else
        {
            leftCabinet.SetActive(false);
            rightCabinet.SetActive(false);
            leftArrow.SetActive(false);
            rightArrow.SetActive(false);
        }

    }

    private void SetLabel()
    {
        machineLabel.text = arcadeCabinets[currentCabinet].title;
    }

    private int Modulo(int a, int b)
    {
        return a - b * Mathf.FloorToInt(((float)a) / b);
    }

    public void LoadMachine()
    {
        //SceneManager.LoadScene(arcadeCabinets[currentCabinet].scene);

        cabinetMenus[currentCabinet].SetActive(true);
        gameObject.SetActive(false);
    }

    private void CreateCabinets()
    {
        arcadeStatus = ArcadeManager.ReadArcadeStatus();

        arcadeCabinets = new List<ArcadeCabinet>();

        if (arcadeStatus.DebugStatus.IsActive)
        {
            arcadeCabinets.Add(new ArcadeCabinet("The Debugger", "Sprites/CabinetScene/Placeholder/debugCabinet"));
        }
        if (arcadeStatus.QMGStatus.IsActive)
        {
            arcadeCabinets.Add(new ArcadeCabinet("???", "Sprites/CabinetScene/Placeholder/BlankCabinet"));
        }
        if(arcadeStatus.KNGStatus.IsActive)
        {
            arcadeCabinets.Add(new ArcadeCabinet("Banana Quest", "Sprites/CabinetScene/Placeholder/BlankCabinet"));
        }
    }

    private class ArcadeCabinet
    {
        public string title;
        public string sprite;
        
        public ArcadeCabinet(string title, string sprite)
        {
            this.title = title;
            this.sprite = sprite;
        }
    }
}
