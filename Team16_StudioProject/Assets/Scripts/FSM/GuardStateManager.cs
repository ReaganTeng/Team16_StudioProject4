using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GuardStateManager: MonoBehaviour
{

    GuardStateBase currentState;
    GuardPatrolState PatrolState = new GuardPatrolState();
    GuardChaseState ChaseState = new GuardChaseState();
    GuardStationaryState StationState = new GuardStationaryState();
    GuardSearchState SearchState = new GuardSearchState();


    // Start is called before the first frame update
    void Start()
    {
        //WHEN GUARD IS FIRST INSTANTIATED, MAKE IT PATROL
        currentState = PatrolState;
        currentState.EnterState(this);
    }


    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GuardStateBase state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
