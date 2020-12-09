using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugCabinetController : MonoBehaviour
{
    public DebugStatus ds;
    public bool initialized = false;

    string appPath;

    // Start is called before the first frame update
    void Start()
    {
        appPath = Application.dataPath + "/SaveData/DebugCabinet.json";
        readFile();
    }

    private void OnApplicationQuit()
    {
        writeDebugFile();
    }

    public void writeDebugFile()
    {
        string serialized = JsonUtility.ToJson(ds);
        System.IO.File.WriteAllText(appPath, serialized);
    }


    void readFile()
    {
        if (System.IO.File.Exists(appPath))
        {
            string text = System.IO.File.ReadAllText(appPath);
            ds = JsonUtility.FromJson<DebugStatus>(text);
        } else
        {
            ds = new DebugStatus();
        }
        initialized = true;        
    }

    public class DebugStatus
    {
        public long tickets;
        public int doublePoints;
        public long doublePointsPrice;

        public DebugStatus()
        {
            this.tickets = 0;
            this.doublePoints = 0;
            this.doublePointsPrice = 1000; // default price for Double Points
        }

        /**
        public DebugStatus(long tickets, int doublePoints)
        {
            this.tickets = tickets;
            this.doublePoints = doublePoints;
            this.doublePointsPrice = 
        }
        */
    }
}
