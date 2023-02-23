using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlarmEvent : MonoBehaviour
{
    private GameObject[] Enemies;
    [SerializeField] private float AlarmRadius = 20.0f;
    [SerializeField] private float AlarmDuration = 20.0f;
    private GameObject[] AlarmPos;
    private Transform[] temp;

    // Update is called once per frame
    public void Start()
    {
        EventManager.Event.SetOffAlarm += CheckForNearbyEnemies;
        EventManager.Event.NoEnemiesNearBy += EnemiesWithinRadius;

       
        //Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        //EventManager.AlarmEvent.Check += EnemiesWithinRadius;
    }
    public void Awake()
    {
        AlarmPos = GameObject.FindGameObjectsWithTag("AlarmPosition");
        temp = new Transform[AlarmPos.Length];
        for (int i = 0; i < AlarmPos.Length; ++i)
        {
            temp[i] = AlarmPos[i].transform;
        }

    }
    public void OnDisable()
    {
        //EventManager.Event.SetOffAlarm -= CheckForNearbyEnemies;
        //EventManager.Event.NoEnemiesNearBy -= EnemiesWithinRadius;
    }

    public void FixedUpdate()
    {
        if (EventManager.Event.GetActiveBool() == true && AlarmDuration > 0.0f)
        {       
            //Debug.Log("Playing Sound");
            AlarmManager.alarmManager.PlayAlarm();
            AlarmManager.alarmManager.OnAlarmLights();           
        }
        else
        {
            if (EventManager.Event.GetActiveBool() == true)
            {
                if (BoolEnemiesWithinRadius())
                {
                    AlarmDuration = 20.0f;
                }
                else
                {
                    
                    EventManager.Event.SetActiveBool(false);
                    AlarmDuration = 20.0f;
                    AlarmManager.alarmManager.StopAlarm();
                    AlarmManager.alarmManager.OffAlarmLights();
                   // GlobalVolume.globalVolume.OffVignette();
                }
            }
        }
    }
    public void Update()
    {
        if (EventManager.Event.GetActiveBool() == true && AlarmDuration > 0.0f)
        {
            AlarmDuration -= Time.deltaTime;
           // Debug.Log(AlarmDuration);
        }
    }
    private void CheckForNearbyEnemies()
    {
        //Enemies = EnemyManager.enemyManager.GetNumberOfEnemies();
        // Debug.Log("SOMEBODY SET OFF THE ALARM!");
        foreach (GameObject nearbyEnemies in EnemyManager.enemyManager.GetNumberOfEnemies())
        {
            NearestAlarm(nearbyEnemies);
            Debug.Log("Alarm Pos:"+temp[0].position);
            if (Vector3.Distance(nearbyEnemies.transform.position, temp[0].position) < AlarmRadius)
            {
                // Debug.Log("Alarm:Chase the player");
                nearbyEnemies.GetComponent<GuardStateManager>().SwitchState(nearbyEnemies.GetComponent<GuardStateManager>().AlarmedState, temp);
                //GSM.SwitchState(GSM.ChaseState);
            }
            else
            {
                Debug.Log("Go Search State Due to Alarm");
                Transform[] nearestWP = EventManager.Event.SortWaypoints(nearbyEnemies.transform,
                    EventManager.Event.CheckForNearestZone(nearbyEnemies.transform));
                // Set the Enemy Search Waypoints , Sort the waypoints starting from the nearest way point
                nearbyEnemies.GetComponent<GuardStateManager>().SwitchState(nearbyEnemies.GetComponent<GuardStateManager>().SearchState, nearestWP);
            }
        }
    }
    private void EnemiesWithinRadius()
    {
       // Enemies = EnemyManager.enemyManager.GetNumberOfEnemies();
        foreach (GameObject nearbyEnemies in EnemyManager.enemyManager.GetNumberOfEnemies())
        {
            //Debug.Log(EnemyManager.enemyManager.GetNumberOfEnemies().Length);
            if (Vector3.Distance(nearbyEnemies.transform.position, transform.position) < AlarmRadius)
            {
                EventManager.Event.SetActiveBool(true);
                EventManager.Event.StartAlarm();
                return;
            }
        }
        Debug.Log("False");
    }
    private bool BoolEnemiesWithinRadius()
    {
        // Enemies = EnemyManager.enemyManager.GetNumberOfEnemies();
        foreach (GameObject nearbyEnemies in EnemyManager.enemyManager.GetNumberOfEnemies())
        {
            //Debug.Log(EnemyManager.enemyManager.GetNumberOfEnemies().Length);
            if (Vector3.Distance(nearbyEnemies.transform.position, transform.position) < AlarmRadius)
            {
                if (nearbyEnemies.GetComponent<GuardStateManager>().returnState() == nearbyEnemies.GetComponent<GuardStateManager>().ChaseState
                    || nearbyEnemies.GetComponent<GuardStateManager>().returnState() == nearbyEnemies.GetComponent<GuardStateManager>().AlarmedState)
                return true;
            }

        }
        return false;

    }
    private void NearestAlarm(GameObject enemy)
    {
        for (int i = 0; i <= temp.Length - 2; i++)
        {
            float dist1 = Vector3.Distance(temp[i].position, enemy.transform.position);
            float dist2 = Vector3.Distance(temp[i + 1].position, enemy.transform.position);
            if (dist1 > dist2)
            {
                var tempo = temp[i];
                temp[i] = temp[i + 1];
                temp[i + 1] = tempo;
            }

        }
    }
}
