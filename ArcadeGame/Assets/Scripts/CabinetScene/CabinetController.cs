using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CabinetController : MonoBehaviour
{
    public GameObject leftCabinet;
    public GameObject centerCabinet;
    public GameObject rightCabinet;
    public GameObject leftArrow;
    public GameObject rightArrow;

    public Text machineLabel;

    public List<ArcadeCabinet> arcadeCabinets;

    private int currentCabinet;

    // Start is called before the first frame update
    void Start()
    {
        currentCabinet = 0;
        setMachine();
    }

    public void rotateLeft()
    {
        // a - b * FLOOR(a/b) C# modulo formula
        currentCabinet = modulo(currentCabinet + 1, arcadeCabinets.Count);
        setMachine();
    }

    public void rotateRight()
    {
        // a - b * FLOOR(a/b) C# modulo formula
        currentCabinet = modulo(currentCabinet - 1, arcadeCabinets.Count);
        setMachine();
    }

    private void setMachine()
    {
        centerCabinet.GetComponent<Image>().sprite = arcadeCabinets[currentCabinet].sprite;
        setLabel();
        if (arcadeCabinets.Count > 1)
        {
            leftCabinet.GetComponent<Image>().sprite = arcadeCabinets[modulo(currentCabinet - 1, arcadeCabinets.Count)].sprite;
            rightCabinet.GetComponent<Image>().sprite = arcadeCabinets[modulo(currentCabinet + 1, arcadeCabinets.Count)].sprite;
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

    private void setLabel()
    {
        machineLabel.text = arcadeCabinets[currentCabinet].title;
    }

    private int modulo(int a, int b)
    {
        return a - b * Mathf.FloorToInt(((float)a) / b);
    }

    public void loadMachine()
    {
        SceneManager.LoadScene(arcadeCabinets[currentCabinet].scene);
    }
}
