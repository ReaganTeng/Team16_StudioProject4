using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAlarmedState : GuardStateBase
{
   // private float distFromPlayer = 10.0f;
   // private float stoppingdistance = 3.0f;

    private Vector3 playerPos;
    // Start is called before the first frame update
    private GuardStateManager GSM;
    int m_CurrentWaypointIndex;
    float detectiondistance;
    int numberOfWaypoints;
    private float gracePeriod = 5.0f; // Time before all guards within a specific radius will enter chase state
    bool alrCheck = false;
    Transform AlarmWP;


    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        GSM = guard;
        guard.navMeshAgent.speed = 5.0f;
        playerPos = guard.getplayerPos().position;
        detectiondistance = 5.0f;
        numberOfWaypoints = wp.Length;
        AlarmWP = wp[0];
        guard.navMeshAgent.SetDestination(wp[0].position);


    }

    // Update is called once per frame
    public override void UpdateState(GuardStateManager guard)
    {

        for (int i = 0; i < 2; i++)
        {
            if (guard.returnObserver(i) == true
                && (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) < detectiondistance))
            {
                guard.SwitchState(guard.ChaseState);
            }
        }

        if (guard.navMeshAgent.remainingDistance < guard.navMeshAgent.stoppingDistance)
        {
            //Debug.Log(AlarmWP.position);
            if (!alrCheck)
            {
                Debug.Log("CURRENTLY SEARCHING FOR THE PLAYER DUE TO ALARM");
                guard.SwitchState(guard.SearchState, EventManager.Event.CheckForNearestZone(guard.getgenemyPos()));
                alrCheck = true;
            }
        }



    }
    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
   
}
