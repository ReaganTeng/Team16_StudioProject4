using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

class WaypointInfo
{
    public GameObject wp;
    public float Distance;
}

public class EventManager : MonoBehaviour
{
    public static EventManager Event;

    public event Action SetOffAlarm;
    public event Action NoEnemiesNearBy;
    public event Action StartCountDown;
    public bool isActive = false;
    private GameObject[] waypoints;

    // Start is called before the first frame update
    void Awake()
    {
        if (Event == null)
        {
            Event = this;
            waypoints = GameObject.FindGameObjectsWithTag("waypoint");
        }
        else
        {
            Destroy(gameObject);
        }


        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //AlarmEvent?.Invoke();
    }
    public void StartAlarm()
    {
        SetOffAlarm?.Invoke();
    }
    public void CheckForEnemies()
    {
        NoEnemiesNearBy?.Invoke();
    }
    public void AlarmCountDown()
    {
        StartCountDown?.Invoke();
    }
    public bool GetActiveBool()
    {
        return isActive;
    }
    public void SetActiveBool(bool boolean)
    {
        isActive = boolean;
    }
    public Transform[] CheckForNearestWP()
    {
        //Debug.Log(waypoints.Length);
        WaypointInfo[] wayPointArray = new WaypointInfo[waypoints.Length];
        float[] wayp = new float[waypoints.Length];
        for (int i = 0; i < waypoints.Length; ++i)
        {
            WaypointInfo wpInfo = new WaypointInfo();
            wpInfo.wp = waypoints[i];
            wpInfo.Distance = Vector3.Distance(waypoints[i].transform.position, transform.position);
            wayPointArray[i] = wpInfo;
        }
        // Sort the array
        for (int i = 0; i < waypoints.Length - 2; ++i)
        {
            if (wayPointArray[i].Distance > wayPointArray[i + 1].Distance)
            {
                var temp = wayPointArray[i].Distance;
                wayPointArray[i].Distance = wayPointArray[i + 1].Distance;
                wayPointArray[i + 1].Distance = temp;
            }
        }

        
        Random r = new Random();
        int bound = r.Next(0, 1);
        int startingPoint = (waypoints.Length) / 2;
        Transform[] storeWPTransform = new Transform[startingPoint];
        //int genRand = 0;
        //Debug.Log(bound);
        //Debug.Log(startingPoint);

        if (bound == 0)
        {
            for (int i = 0; i < startingPoint; ++i)
            {
                storeWPTransform[i] = wayPointArray[i].wp.transform;
                // Debug.Log(wayPointArray[i].wp.transform.position);
            }

        }
        else if (bound == 1)
        {
            //genRand = r.Next(startingPoint + 1, waypoints.Length);
            for (int i = startingPoint - 1; i < waypoints.Length; i++)
            {
               // Debug.Log(i - startingPoint);
                storeWPTransform[i - startingPoint] = wayPointArray[i].wp.transform;
                
                // Debug.Log(wayPointArray[i].wp.transform.position);
            }
        }
        //Debug.Log(startingPoint);
        //Debug.Log(genRand);

        

      //  Debug.Log(storeWPTransform.Length);
        return storeWPTransform;

    }
}
