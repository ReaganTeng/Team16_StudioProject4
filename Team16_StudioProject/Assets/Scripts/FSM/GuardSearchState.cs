using UnityEngine;
using UnityEngine.AI;

public class GuardSearchState : GuardStateBase
{
    private Transform[] wayp;
    int m_CurrentWaypointIndex;
    float detectiondistance;

    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        Debug.Log("SEARCH STATE");

        //set first destination
        guard.navMeshAgent.SetDestination(guard.waypoints[0].position);
        guard.navMeshAgent.speed = 5.0f;
        detectiondistance = 5.0f;
    }

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
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % guard.waypoints.Length;
            guard.navMeshAgent.SetDestination(guard.waypoints[m_CurrentWaypointIndex].position);
        }
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}