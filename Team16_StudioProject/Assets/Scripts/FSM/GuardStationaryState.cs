using UnityEngine;

public class GuardStationaryState : GuardStateBase
{
    public override void EnterState(GuardStateManager guard, Transform[] wp)
    {
        Debug.Log("STATIONARY STATE");

    }

    public override void UpdateState(GuardStateManager guard)
    {

    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
