
using UnityEngine;
using UnityEngine.AI;

public class GuardPatrolState : GuardStateBase
{
    float timer;
    private Transform[] wayp;
    NavMeshAgent navMeshAgent;
    int m_CurrentWaypointIndex;
    float detectiondistance;

    RaycastHit raycastHit;
    RaycastHit raycastHit_2;

    bool playerdetected = false;

    void OnTriggerEnter(GuardStateManager guard)
    {
        if (guard.pov.transform.GetComponent<Collider>() == guard.getplayer()
            || guard.pov2.transform.GetComponent<Collider>() == guard.getplayer())
        {
            playerdetected = true;
        }
    }

    void OnTriggerExit(GuardStateManager guard)
    {
        if (guard.pov.transform.GetComponent<Collider>() == guard.getplayer()
            || guard.pov2.transform.GetComponent<Collider>() == guard.getplayer())
        {
            playerdetected = false;
        }
    }

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
        //if player and enemy distance is within 5, and there's no obstacle along the way according to raycast
        if (
            (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) < detectiondistance)
            )
        {
            //if ((Physics.Raycast(ray_2, out raycastHit_2) && raycastHit_2.collider.transform == guard.getplayer())
            //|| (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.transform == guard.getplayer()))
            //{
            guard.SwitchState(guard.ChaseState);
            //}
        }


        //if (playerdetected == true)
        //{

        //    Vector3 direction = guard.getplayerPos().position - guard.pov.transform.position + Vector3.up;
        //    Ray ray = new Ray(guard.pov.transform.position, direction);

        //    Vector3 direction_2 = guard.getplayerPos().position - guard.pov2.transform.position + Vector3.up;
        //    Ray ray_2 = new Ray(guard.pov2.transform.position, direction_2);

        //    if ((Physics.Raycast(ray_2, out raycastHit_2) && raycastHit_2.collider.transform == guard.getplayer())
        //        || (Physics.Raycast(ray, out raycastHit) && raycastHit.collider.transform == guard.getplayer()))
        //    {
        //        Debug.Log("PLAYER DETECTED");
        //    }
        //}

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
