using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardSecurityState : GuardStateBase
{

     float distfromPlayer;
    float stoppingdistance;
    private GameObject target;
    private float time;


    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        time = 5.0f;


        guard.navMeshAgent.speed = 5.0f;
        distfromPlayer = 10.0f;
    }

    public override void UpdateState(GuardStateManager guard)
    {

   
        guard.navMeshAgent.SetDestination(guard.gettargetpos());
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
