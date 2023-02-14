
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrolState : GuardStateBase
{
    float timer;
    private Transform[] wayp;
    NavMeshAgent navMeshAgent;
    int m_CurrentWaypointIndex;
    float detectiondistance;

    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        Debug.Log("PATROL STATE");
        timer = 0;

        //wayp[0].position = guard.waypoints[0].position;
        //wayp[1].position = guard.waypoints[1].position;

        //set first destination
        guard.navMeshAgent.SetDestination(guard.waypoints[0].position);
        detectiondistance = 5.0f;

    }

    public override void UpdateState(GuardStateManager guard)
    {
        //if player and enemy diatance is within 5
        if (Vector3.Distance(guard.player.transform.position, guard.getgenemyPos().position) < detectiondistance)
        {
            guard.SwitchState(guard.ChaseState);
        }

        Debug.Log("DISTANCE POSITION " + Vector3.Distance(guard.getgenemyPos().position, guard.player.transform.position));


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
