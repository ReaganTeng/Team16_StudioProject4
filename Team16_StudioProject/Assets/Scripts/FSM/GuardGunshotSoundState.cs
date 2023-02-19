using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardGunshotSoundState : GuardStateBase
{
    private GameObject coin;
    private float time;
    private Transform[] playerPos;

    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
       // Debug.Log("WHAT'S THAT SOUND");
        time = 5.0f;


        guard.navMeshAgent.speed = 5.0f;
        //playerPos = new Transform[1];
        guard.navMeshAgent.SetDestination(guard.getplayerPos().position);
    }

    public override void UpdateState(GuardStateManager guard)
    {
        time -= Time.deltaTime;

        if (time <= 0)
        {
            //GameObject.Destroy(coin);
            //playerPos[0] = guard.getplayerPos();
            guard.SwitchState(guard.SearchState);
        }

        //Debug.Log("TIME " + time);
        //CONTANTLY SET DESTINATION AS PLAYER'S CURRENT POSITION

    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
