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
        coin = GameObject.Find("Coin(Clone)");
        time = 5.0f;

        guard.navMeshAgent.speed = 5.0f;
        distfromPlayer = 10.0f;
        stoppingdistance = 3.0f;
    }

    public override void UpdateState(GuardStateManager guard)
    {
        if(coin == null)
        {
            return;
        }



        time -= Time.deltaTime;

        if (time <= 0)
        {
            //GameObject.Destroy(coin);
            guard.SwitchState(guard.SearchState);
        }

        //Debug.Log("TIME " + time);
        //CONTANTLY SET DESTINATION AS PLAYER'S CURRENT POSITION
        guard.navMeshAgent.SetDestination(coin.transform.position);




        //     //if player and enemy distance is more than distance
        //     if (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) > distfromPlayer)
        //     {
        //         guard.SwitchState(guard.SearchState);
        //     }

        //     //STOPPING DISTANCE
        //    if (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) <= stoppingdistance)
        //     {
        //         guard.navMeshAgent.speed = 0.0f;
        //     }
        //     else if (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) > stoppingdistance)
        //     {
        //         guard.navMeshAgent.speed = 5.0f;
        //     }
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
