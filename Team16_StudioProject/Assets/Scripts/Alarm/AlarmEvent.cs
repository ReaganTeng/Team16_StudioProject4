using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlarmEvent : MonoBehaviour
{
    private GameObject[] Enemies;
    AudioSource m_AudioSource;
    [SerializeField] private float AlarmRadius = 20.0f;
    // public static event Action Alarm;
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
        m_AudioSource = GetComponent<AudioSource>();
    }
    public void OnDisable()
    {
        //EventManager.Event.SetOffAlarm -= CheckForNearbyEnemies;
        //EventManager.Event.NoEnemiesNearBy -= EnemiesWithinRadius;
    }

    public void FixedUpdate()
    {
        if (EventManager.Event.GetActiveBool() == true)
        {
            if (!m_AudioSource.isPlaying)
            {
                Debug.Log("Playing Sound");
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }
    }
    private void CheckForNearbyEnemies()
    {
        //Enemies = EnemyManager.enemyManager.GetNumberOfEnemies();
        // Debug.Log("SOMEBODY SET OFF THE ALARM!");
        foreach (GameObject nearbyEnemies in EnemyManager.enemyManager.GetNumberOfEnemies())
        {
            if (Vector3.Distance(nearbyEnemies.transform.position, transform.position) < AlarmRadius)
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
       // Enemies = EnemyManager.enemyManager.GetNumberOfEnemies();
        foreach (GameObject nearbyEnemies in EnemyManager.enemyManager.GetNumberOfEnemies())
        {
            Debug.Log(EnemyManager.enemyManager.GetNumberOfEnemies().Length);
            if (Vector3.Distance(nearbyEnemies.transform.position, transform.position) < AlarmRadius)
            {
                EventManager.Event.SetActiveBool(true);
                return;
            }
        }
        Debug.Log("False");
        EventManager.Event.SetActiveBool(false);
    }
}
