using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AlarmEvent : MonoBehaviour
{
    private GameObject[] Enemies;
    AudioSource m_AudioSource;
    Animator m_Animator;
    [SerializeField] private float AlarmRadius = 20.0f;
    [SerializeField] private float AlarmDuration = 20.0f;
    private Transform AlarmPos;
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
        m_AudioSource = GetComponent<AudioSource>();
        m_Animator = GetComponentInChildren<Animator>();
        AlarmPos = GameObject.Find("AlarmPosition").transform;
        temp = new Transform[1];
        temp[0] = AlarmPos;

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
            if (!m_AudioSource.isPlaying)
            {
                Debug.Log("Playing Sound");
                m_AudioSource.Play();
                AlarmFlickering.LightSource.flickeringLight.enabled = true;
                m_Animator.enabled = true;
            }
        }
        else
        {
            m_AudioSource.Stop();
            m_Animator.enabled = false;
            AlarmFlickering.LightSource.flickeringLight.enabled = false;


        }
    }
    public void Update()
    {
        if (EventManager.Event.GetActiveBool() == true && AlarmDuration > 0.0f)
        {
            AlarmDuration -= Time.deltaTime;
           // Debug.Log(AlarmDuration);
        }
        else
        {
            AlarmDuration = 20.0f;
            EventManager.Event.SetActiveBool(false);
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
                nearbyEnemies.GetComponent<GuardStateManager>().SwitchState(nearbyEnemies.GetComponent<GuardStateManager>().AlarmedState, temp);
                //GSM.SwitchState(GSM.ChaseState);

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
    }
}
