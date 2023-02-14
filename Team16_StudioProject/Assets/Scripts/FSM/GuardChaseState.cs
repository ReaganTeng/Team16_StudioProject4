using UnityEngine;

public class GuardChaseState : GuardStateBase
{

    float distfromPlayer;
    float stoppingdistance;


    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        Debug.Log("CHASE STATE");

        guard.navMeshAgent.speed = 5.0f;
        distfromPlayer = 10.0f;
        stoppingdistance = 3.0f;
    }

    public override void UpdateState(GuardStateManager guard)
    {
        //if player and enemy distance is more than distance
        if (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) > distfromPlayer)
        {
            guard.SwitchState(guard.SearchState);
        }

        //STOPPING DISTANCE
       if (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) <= stoppingdistance)
        {
            guard.navMeshAgent.speed = 0.0f;
        }
        else if (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) > stoppingdistance)
        {
            guard.navMeshAgent.speed = 5.0f;
        }

        //CONTANTLY SET DESTINATION AS PLAYER'S CURRENT POSITION
        guard.navMeshAgent.SetDestination(guard.getplayerPos().position);
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
