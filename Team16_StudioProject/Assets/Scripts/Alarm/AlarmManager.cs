using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmManager : MonoBehaviour
{
    public static AlarmManager alarmManager;
    private GameObject[] Alarms;
    private GameObject[] AlarmPos;
    private GameObject[] AlarmLight;
    // Start is called before the first frame update
    void Awake()
    {
        if (alarmManager == null)
        {
            alarmManager = this;
            Alarms = GameObject.FindGameObjectsWithTag("Alarm");
            AlarmPos = GameObject.FindGameObjectsWithTag("AlarmPosition");
            AlarmLight = GameObject.FindGameObjectsWithTag("AlarmLight");

        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

    }
    public void PlayAlarm()
    {
        foreach (GameObject Alarm in Alarms)
        {
            if (!Alarm.GetComponent<AudioSource>().isPlaying)
            {
                Alarm.GetComponent<AudioSource>().volume = GameObject.Find("SettingsStats").GetComponent<globalstats>().volumeslider;
                Alarm.GetComponent<AudioSource>().Play();
            }
        }
    }
    public void StopAlarm()
    {
        foreach (GameObject Alarm in Alarms)
        {
            if (Alarm.GetComponent<AudioSource>().isPlaying)
            {
                Alarm.GetComponent<AudioSource>().Stop();
            }
        }
    }
    public void OnAlarmLights()
    {
        foreach(GameObject alarmLight in AlarmLight)
        {
            alarmLight.GetComponent<Light>().enabled = true;
            alarmLight.GetComponent<Animator>().enabled = true;
        }
    }
    public void OffAlarmLights()
    {
        foreach (GameObject alarmLight in AlarmLight)
        {
            alarmLight.GetComponent<Light>().enabled = false;
            alarmLight.GetComponent<Animator>().enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
