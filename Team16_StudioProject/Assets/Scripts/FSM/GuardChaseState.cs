using UnityEngine;

public class GuardChaseState : GuardStateBase
{

    float distfromPlayer;
    float stoppingdistance;
    float timer_between_shots;


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


        timer_between_shots += 1.0f * Time.deltaTime;


        if (timer_between_shots >= 3.0f)
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = GameObject.Instantiate(guard.projectile, guard.getgenemyPos().position, guard.getgenemyPos().rotation);
            //pos = guard.getgenemyPos().position;

            //clone.position = Camera.main.transform.position;
            //clone.position += Vector3.up * 1.0f;
            //clone.position += Camera.main.transform.forward * 3.0f;
            clone.velocity = guard.getgenemyPos()/*.TransformDirection(guard.getgenemyPos().transform*/.forward * 40/*)*/;
            clone.MoveRotation(guard.getgenemyPos().rotation);

            timer_between_shots = 0;
        }

        //CONTANTLY SET DESTINATION AS PLAYER'S CURRENT POSITION
        guard.navMeshAgent.SetDestination(guard.getplayerPos().position);
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
