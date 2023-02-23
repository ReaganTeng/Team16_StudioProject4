using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardCoinState : GuardStateBase
{
    float distfromPlayer;
    float stoppingdistance;
    private GameObject coin;
    private float time;


    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        

        Debug.Log("DISTRACTED STATE");
        time = 5.0f;
        coin = GameObject.Find("Coin(Clone)");
        guard.navMeshAgent.SetDestination(coin.transform.position);


        guard.navMeshAgent.speed = 5.0f;
        distfromPlayer = 10.0f;
        stoppingdistance = 3.0f;
    }

    public override void UpdateState(GuardStateManager guard)
    { 

        //if (coin == null)
        //{
        //    return;
        //}

        time -= Time.deltaTime;

        if (time <= 0)
        {
            //GameObject.Destroy(coin);
            guard.SwitchState(guard.SearchState);
        }

        
        for (int i = 0; i < 2; i++)
        {
            if (guard.returnObserver(i) == true
                && (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) < 5.0f))
            {
                guard.SwitchState(guard.ChaseState);
            }
        }


       
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
