
using UnityEngine;

public class GuardPatrolState : GuardStateBase
{
    float timer;

    public override void EnterState(GuardStateManager guard)
    {
        Debug.Log("PATROL STATE");
        timer = 0;
    }

    public override void UpdateState(GuardStateManager guard)
    {
        timer += 1.0f * Time.deltaTime;

        if(timer > 3.0f)
        {
            //guard.SwitchState(guard.StationState);
        }
    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
