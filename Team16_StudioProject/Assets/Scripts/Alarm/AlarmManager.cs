using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    public static AlarmManager alarmManager;
    public GameObject[] Alarms;
    // Start is called before the first frame update
    void Awake()
    {
        if (alarmManager == null)
        {
            alarmManager = this;
            Alarms = GameObject.FindGameObjectsWithTag("Alarm");

        }
        else
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
