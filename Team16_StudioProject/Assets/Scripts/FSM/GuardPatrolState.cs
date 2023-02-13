
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrolState : GuardStateBase
{
    float timer;

    //public Transform[] waypoints;
    //NavMeshAgent navMeshAgent;
    //int m_CurrentWaypointIndex;


    public override void EnterState(GuardStateManager guard)
    {
        Debug.Log("PATROL STATE");
        timer = 0;
        //navMeshAgent.SetDestination(waypoints[0].position);

    }

    public override void UpdateState(GuardStateManager guard)
    {
        timer += 1.0f * Time.deltaTime;

        if (timer > 3.0f)
        {
            guard.SwitchState(guard.StationState);
        }


        //if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        //{
        //    m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        //    navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        //}
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
