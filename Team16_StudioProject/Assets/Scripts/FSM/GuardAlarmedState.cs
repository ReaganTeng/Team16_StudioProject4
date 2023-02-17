using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardAlarmedState : GuardStateBase
{
   // private float distFromPlayer = 10.0f;
   // private float stoppingdistance = 3.0f;
    private float iCurrentFsmPeriod = 15.0f; // Duration of the time spent in this state
    private Vector3 playerPos;
    // Start is called before the first frame update
    private GameObject[] AlarmObjectArray;
    private GuardStateManager GSM;
    int m_CurrentWaypointIndex;
    float detectiondistance;
    int numberOfWaypoints;
    private float gracePeriod = 5.0f; // Time before all guards within a specific radius will enter chase state


    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        GSM = guard;
        guard.navMeshAgent.speed = 5.0f;
        playerPos = guard.getplayerPos().position;
        AlarmObjectArray = GameObject.FindGameObjectsWithTag("Alarm");
        detectiondistance = 5.0f;
        numberOfWaypoints = wp.Length;

    }

    // Update is called once per frame
    public override void UpdateState(GuardStateManager guard)
    {
        if (gracePeriod < 0.0f)
        {
            gracePeriod = 5.0f;
            CheckForNearbyEnemies();            
        }
        else
        {
            gracePeriod -= Time.deltaTime;
            for (int i = 0; i < numberOfWaypoints; i++)
            {
                if (guard.returnObserver(i) == true
                    && (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) < detectiondistance))
                {
                    guard.SwitchState(guard.ChaseState);
                }
            }

            if (guard.navMeshAgent.remainingDistance < guard.navMeshAgent.stoppingDistance)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % guard.waypoints.Length;
                guard.navMeshAgent.SetDestination(guard.waypoints[m_CurrentWaypointIndex].position);
            }

        }

    }
    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
    void CheckForNearbyEnemies()
    {
        foreach (GameObject nearbyEnemies in AlarmObjectArray)
        {
            if (Vector3.Distance(nearbyEnemies.transform.position, GSM.getgenemyPos().position) < 10)
            {
                GSM.SwitchState(GSM.ChaseState);

            }
            else
            {
                GSM.SwitchState(GSM.SearchState);
            }
        }
    }
}
