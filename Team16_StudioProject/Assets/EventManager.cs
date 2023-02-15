using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventManager : MonoBehaviour
{
    public static EventManager AlarmEvent;

    public event Action SetOffAlarm;
    public event Action Check;
    // Start is called before the first frame update
    void Start()
    {
        if (AlarmEvent == null)
        {
            AlarmEvent = this;
        }
        else
            Destroy(gameObject);


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
        Check?.Invoke();
    }
}
