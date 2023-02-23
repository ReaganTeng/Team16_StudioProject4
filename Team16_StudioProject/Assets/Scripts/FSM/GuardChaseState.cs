using UnityEngine;

public class GuardChaseState : GuardStateBase
{

    float distfromPlayer;
    float stoppingdistance;
    float timer_between_shots;
    private float gracePeriod = 5.0f; // Time before all guards within a specific radius will enter chase state
    PlayerStats pStats;

    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        Debug.Log("CHASE STATE");

        guard.navMeshAgent.speed = 2.0f;
        distfromPlayer = 10.0f;
        stoppingdistance = 3.0f;
        GlobalVolume.globalVolume.SetChase(true);
    }

    public override void UpdateState(GuardStateManager guard)
    {

        var lookpos = guard.getplayerPos().position - guard.getgenemyPos().position;
        lookpos.y = 0;
        var rotation = Quaternion.LookRotation(lookpos);
        guard.getgenemyPos().rotation = Quaternion.Slerp(
            guard.getgenemyPos().rotation, rotation, Time.deltaTime * 10
            );

        GlobalVolume.globalVolume.SetVignetteIntensity();

        if (gracePeriod > 0.0f)
        {
            gracePeriod -= Time.deltaTime;
            //Debug.Log(gracePeriod);
        }
        else
        {
            if (EventManager.Event.GetActiveBool() == false)
            {
                Debug.Log("ALARM");
                // Debug.Log("Event Active");
                EventManager.Event.StartAlarm();
                EventManager.Event.CheckForEnemies();
                //EventManager.Event.AlarmCountDown();

                //if (pStats.GetHealth() > 0)
                //{
                //    Debug.Log("L");
                //    return;
                //}
                // Grace Period is over call the alarm event
                // alarm.Alarmed = true;
                gracePeriod = 5.0f;
            }
        }

        //if player and enemy distance is more than distance
        if (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) > distfromPlayer)
        {
            gracePeriod = 5.0f;
            guard.SwitchState(guard.SearchState);
            GlobalVolume.globalVolume.SetChase(false);
        }

        //STOPPING DISTANCE
       if (Vector3.Distance(guard.getplayerPos().position, guard.getgenemyPos().position) <= stoppingdistance)
        {
            guard.navMeshAgent.speed = 0.0f;
        }
        else
        {
            guard.navMeshAgent.speed = 5.0f;
        }


        timer_between_shots += 1.0f * Time.deltaTime;


        if (timer_between_shots >= 0.5f)
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = GameObject.Instantiate(guard.projectile, guard.getgenemyPos().position, guard.getgenemyPos().rotation);
            clone.velocity = guard.getgenemyPos().forward * 40;
            clone.MoveRotation(guard.getgenemyPos().rotation);

            // For hurt direction
            guard.projectile.GetComponent<EnemyProjectile>().startPos = guard.transform.position;
            guard.shootStartPos = guard.transform.position;

            timer_between_shots = 0;
        }

        //CONTANTLY SET DESTINATION AS PLAYER'S CURRENT POSITION
        guard.navMeshAgent.SetDestination(guard.getplayerPos().position);
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
