
using UnityEngine;

public class GuardSearchState : GuardStateBase
{
    public override void EnterState(GuardStateManager guard)
    {
        Debug.Log("SEARCHING STATE");

    }

    public override void UpdateState(GuardStateManager guard)
    {

    }

    public override void OnCollisionEnter(GuardStateManager guard, Collision collision)
    {

    }
}
