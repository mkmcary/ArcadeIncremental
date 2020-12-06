using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static bool initialized = false;

    private long genericTickets;
    private long debugTickets;

    // Awake is called everytime a scene loads
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        if (!GameController.initialized)
        {
            initializeLayers();
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void initializeLayers()
    {
        genericTickets = 0;
        debugTickets = 0;
        GameController.initialized = true;
    }

    public long getGenericTickets()
    {
        return this.genericTickets;
    }

    public bool incrementGenericTickets(long increment)
    {
        if (genericTickets + increment < 0)
        {
            return false;
        }
        this.genericTickets += increment;
        return true;
    }

    public long getDebugTickets()
    {
        return this.debugTickets;
    }

    public bool incrementDebugTickets(long increment)
    {
        if (debugTickets + increment < 0)
        {
            return false;
        }
        this.debugTickets += increment;
        return true;
    }

}
