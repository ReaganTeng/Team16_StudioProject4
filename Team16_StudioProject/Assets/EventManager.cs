using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager Event;

    public event Action SetOffAlarm;
    public event Action NoEnemiesNearBy;
    public bool isActive = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (Event == null)
        {
            Event = this;
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
    public bool GetActiveBool()
    {
        return isActive;
    }
    public void SetActiveBool(bool boolean)
    {
        isActive = boolean;
    }
}
