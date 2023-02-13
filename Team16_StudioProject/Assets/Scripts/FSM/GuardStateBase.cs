using UnityEngine;

public abstract class GuardStateBase
{
    public abstract void EnterState(GuardStateManager guard, Transform[] wp);

    public abstract void UpdateState(GuardStateManager guard);

    public abstract void OnCollisionEnter(GuardStateManager guard, Collision collision);
}
