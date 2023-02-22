using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = System.Random;

class WaypointInfo
{
    public Transform wp;
    public float Distance;
}
class ZoneInfo
{
    public float Distance; // Distance between the enemy and the zone
    public int ZoneIdx; // Zone Number
}

public class EventManager : MonoBehaviour
{
    public static EventManager Event;

    public event Action SetOffAlarm;
    public event Action NoEnemiesNearBy;
    public event Action StartCountDown;
    public bool isActive = false;
    private Transform[] waypoints;
    private GameObject[] ZoneArray;

    // Start is called before the first frame update
    void Awake()
    {
        if (Event == null)
        {
            Event = this;
            ZoneArray = GameObject.FindGameObjectsWithTag("Zone"); // Get All The Zones GameObject

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
    public Transform[] CheckForNearestZone(Transform enemypos)
    {
        //Debug.Log(waypoints.Length);
        int ZoneIndex = FindNearestZone(enemypos);
        //Debug.Log(ZoneIndex);
        GameObject Zone = GameObject.Find("Zone" + ZoneIndex);
        int WaypointLength = -1;
        foreach (Transform transform in Zone.GetComponentsInChildren<Transform>())
        {
            WaypointLength++;
           // Debug.Log(transform);
        }
        Transform[] storeWPTransform = new Transform[WaypointLength];
        for (int i = 0; i < WaypointLength; i++)
        { 
            storeWPTransform[i] = Zone.GetComponentsInChildren<Transform>()[i + 1];
        }
        return storeWPTransform;
    }

    public Transform[] SortWaypoints(Transform enemypos, Transform[] wpArray)
    {
        waypoints = wpArray;
        Debug.Log("WP Length:" + waypoints.Length);
        WaypointInfo[] wayPointArray = new WaypointInfo[waypoints.Length];
        for (int i = 0; i < waypoints.Length; ++i)
        {
            WaypointInfo wpInfo = new WaypointInfo();
            wpInfo.wp = waypoints[i];
            wpInfo.Distance = Vector3.Distance(waypoints[i].position, enemypos.position);
            wayPointArray[i] = wpInfo;
        }
        // Sort the array
        for (int j = 0; j < waypoints.Length; j++)
        {
            for (int i = 0; i < waypoints.Length - 2; ++i)
            {
                if (wayPointArray[i].Distance > wayPointArray[i + 1].Distance)
                {
                    var temp = wayPointArray[i];
                    wayPointArray[i] = wayPointArray[i + 1];
                    wayPointArray[i + 1] = temp;
                }
            }
        }
        Transform[] storeWPTransform = new Transform[waypoints.Length];
        for (int i = 0; i < waypoints.Length; ++i)
        {
            storeWPTransform[i] = wayPointArray[i].wp;
            // Debug.Log(wayPointArray[i].wp.transform.position);
        }
        return storeWPTransform;

    }
    private int FindNearestZone(Transform enemypos) // Finds the nearest zone
    {
        ZoneInfo[] ZoneInfoArray = new ZoneInfo[ZoneArray.Length];
        for (int i = 0; i < ZoneArray.Length; i++)
        {
            ZoneInfo zInfo = new ZoneInfo();
            zInfo.ZoneIdx = i + 1;
            zInfo.Distance = Vector3.Distance(ZoneArray[i].transform.position, enemypos.position);
            Debug.Log(zInfo.ZoneIdx + ", Distance:" + zInfo.Distance);
            ZoneInfoArray[i] = zInfo;             
        }

        // Sort the array
        for (int j = 0; j < ZoneInfoArray.Length; j++)
        {
            for (int i = 0; i < ZoneInfoArray.Length - 2; ++i)
            {
                if (ZoneInfoArray[i].Distance > ZoneInfoArray[i + 1].Distance)
                {
                    var temp = ZoneInfoArray[i];
                    ZoneInfoArray[i] = ZoneInfoArray[i + 1];
                    ZoneInfoArray[i + 1] = temp;
                }
            }
        }

        return ZoneInfoArray[0].ZoneIdx;
    }
    
}
