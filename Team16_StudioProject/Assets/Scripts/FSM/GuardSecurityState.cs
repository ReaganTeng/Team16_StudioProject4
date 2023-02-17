using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSecurityState : GuardStateBase
{

    float distfromPlayer;
    float stoppingdistance;
    private GameObject target;
    private float time;

    float detectiondistance;

    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        Debug.Log("ALERT STATE");
        time = 5.0f;

        detectiondistance = 5.0f;
        guard.navMeshAgent.speed = 5.0f;
        distfromPlayer = 10.0f;
    }

    public override void UpdateState(GuardStateManager guard)
    {
        guard.navMeshAgent.SetDestination(guard.gettargetpos());



        if(Vector3.Distance(guard.getgenemyPos().position, guard.gettargetpos()) < 5.0f)
        {
            time -= Time.deltaTime;
        }

        if (time < 0.0f)
        {
            guard.SwitchState(guard.SearchState);
        }


        //SWITCH TO CHASE STATE WHEN ENEMY SAW PLAYER
        for (int i = 0; i < 2; i++)
        {
            if (guard.returnObserver(i) == true
                && (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) < detectiondistance))
            {
                guard.SwitchState(guard.ChaseState);
            }
        }

    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
