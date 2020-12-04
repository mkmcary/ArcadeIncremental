using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CabinetController : MonoBehaviour
{
    public GameObject leftMachine;
    public GameObject centerMachine;
    public GameObject rightMachine;

    public Text machineTitle;

    public List<Sprite> sprites;
    public List<String> titles;

    private int currentMachine;

    // Start is called before the first frame update
    void Start()
    {
        currentMachine = 1;
        setMachine();
    }

    public void rotateLeft()
    {
        // a - b * FLOOR(a/b) C# modulo formula
        currentMachine = modulo(currentMachine + 1, sprites.Count);
        setMachine();
    }

    public void rotateRight()
    {
        // a - b * FLOOR(a/b) C# modulo formula
        currentMachine = modulo(currentMachine - 1, sprites.Count);
        setMachine();
    }

    private void setMachine()
    {
        leftMachine.GetComponent<Image>().sprite = sprites[modulo(currentMachine - 1, sprites.Count)];
        centerMachine.GetComponent<Image>().sprite = sprites[currentMachine];
        rightMachine.GetComponent<Image>().sprite = sprites[modulo(currentMachine + 1, sprites.Count)];
        setTitle();
    }

    private void setTitle()
    {
        machineTitle.text = titles[currentMachine];
    }

    private int modulo(int a, int b)
    {
        return a - b * Mathf.FloorToInt(((float)a) / b);
    }
}
