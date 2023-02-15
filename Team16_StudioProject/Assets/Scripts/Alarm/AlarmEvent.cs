using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlarmEvent : MonoBehaviour
{
    private GameObject[] Enemies;
    AudioSource m_AudioSource;

    // public static event Action Alarm;
    // Update is called once per frame
    public void Start()
    {
        Enemies = GameObject.FindGameObjectsWithTag("Enemy");
        EventManager.Event.SetOffAlarm += CheckForNearbyEnemies;
        EventManager.Event.NoEnemiesNearBy += EnemiesWithinRadius;
        m_AudioSource = GetComponent<AudioSource>();

        //EventManager.AlarmEvent.Check += EnemiesWithinRadius;
    }
    public void OnDisable()
    {
        EventManager.Event.SetOffAlarm -= CheckForNearbyEnemies;
        EventManager.Event.NoEnemiesNearBy -= EnemiesWithinRadius;
    }

    public void FixedUpdate()
    {
        if (EventManager.Event.isActive == true)
        {
            m_AudioSource.Play();
        }
        else
        {
            m_AudioSource.Stop();
        }
    }
    private void CheckForNearbyEnemies()
    {
       // Debug.Log("SOMEBODY SET OFF THE ALARM!");
        foreach (GameObject nearbyEnemies in Enemies)
        {
            if (Vector3.Distance(nearbyEnemies.transform.position, transform.position) < 20)
            {
                //Debug.Log("Alarm:Chase the player");
                nearbyEnemies.GetComponent<GuardStateManager>().SwitchState(nearbyEnemies.GetComponent<GuardStateManager>().ChaseState);
                //GSM.SwitchState(GSM.ChaseState);

            }
            else
            {
               // Debug.Log("Alarm:Search for the player");
                nearbyEnemies.GetComponent<GuardStateManager>().SwitchState(nearbyEnemies.GetComponent<GuardStateManager>().SearchState);
                //GSM.SwitchState(GSM.SearchState);
            }
        }
    }
    private void EnemiesWithinRadius()
    {
        foreach (GameObject nearbyEnemies in Enemies)
        {
            if (Vector3.Distance(nearbyEnemies.transform.position, transform.position) < 5)
            {
                return;
            }
        }

        EventManager.Event.SetActiveBool(false);
    }
}
