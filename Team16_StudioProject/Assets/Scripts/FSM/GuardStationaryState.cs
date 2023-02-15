using UnityEngine;

public class GuardStationaryState : GuardStateBase
{
    float detectiondistance;


    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        Debug.Log("STATIONARY STATE");
        detectiondistance = 5.0f;
    }

    public override void UpdateState(GuardStateManager guard)
    {
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
