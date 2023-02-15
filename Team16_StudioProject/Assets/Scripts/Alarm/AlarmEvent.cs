using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlarmEvent : MonoBehaviour
{
    private GameObject[] Enemies;
    
    // public static event Action Alarm;
    // Update is called once per frame
    public void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EventManager.AlarmEvent.SetOffAlarm += CheckForNearbyEnemies;
        //EventManager.AlarmEvent.Check += EnemiesWithinRadius;
    }
    public void Update()
    {

    }
    private void CheckForNearbyEnemies()
    {
        //Debug.Log("SOMEBODY SET OFF THE ALARM!");
        foreach (GameObject nearbyEnemies in Enemies)
        {
            if (Vector3.Distance(nearbyEnemies.transform.position, transform.position) < 20)
            {
                nearbyEnemies.GetComponent<GuardStateManager>().SwitchState(nearbyEnemies.GetComponent<GuardStateManager>().ChaseState);
                //GSM.SwitchState(GSM.ChaseState);

            }
            else
            {
                nearbyEnemies.GetComponent<GuardStateManager>().SwitchState(nearbyEnemies.GetComponent<GuardStateManager>().SearchState);
                //GSM.SwitchState(GSM.SearchState);
            }
        }
    }
    private bool EnemiesWithinRadius()
    {
        foreach (GameObject nearbyEnemies in Enemies)
        {
            if (Vector3.Distance(nearbyEnemies.transform.position, transform.position) < 20)
            {
                return true;
            }
        }
        return false;
    }
}
