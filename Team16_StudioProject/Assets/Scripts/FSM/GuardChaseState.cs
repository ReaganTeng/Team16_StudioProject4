
using UnityEngine;

public class GuardChaseState : GuardStateBase
{
    public override void EnterState(GuardStateManager guard)
    {
        Debug.Log("CHASE STATE");

    }

    public override void UpdateState(GuardStateManager guard)
    {

    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
