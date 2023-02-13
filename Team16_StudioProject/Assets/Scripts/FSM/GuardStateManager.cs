using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

using UnityEngine;

public class GuardStateManager : MonoBehaviour
{
    GuardStateBase currentState;
    public GuardPatrolState PatrolState = new GuardPatrolState();
    public GuardChaseState ChaseState = new GuardChaseState();
    public GuardStationaryState StationState = new GuardStationaryState();
    public GuardSearchState SearchState = new GuardSearchState();


    //REINITIALISE VALUE
    //FOR GUARDPATROLSTATE
    public Transform[] waypoints;
    public NavMeshAgent navMeshAgent;
    //

    public GameObject player;
    Transform enemyPos;

    void Awake()
    {
        //FOR GUARDPATROLSTATE
        //waypoints[0].Transform.position = GetComponent<Transform>().position + new Vector3(-20, 0, 0);
        //Instantiate(waypoints[0], go.transform.position + new Vector3(-20, 0, 0), new Quaternion(0, 0, 0, 0));
        //waypoints[1].Transform.position = GetComponent<Transform>().position + new Vector3(20, 0, 0);
        //Instantiate(waypoints[1], go.transform.position + new Vector3(20, 0, 0), new Quaternion(0, 0, 0, 0));
        //
    }


    //FIRS UPDATE LOOP, SET TO PATROL

    // Start is called before the first frame update
    void Start()
    {
        //WHEN GUARD IS FIRST INSTANTIATED, MAKE IT PATROL
        currentState = PatrolState;
        //

        //Debug.Log("WAYPOINTS ARE: " + waypoints[0].position);
        currentState.EnterState(this, waypoints);
    }


    void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");


        enemyPos = GetComponent<Transform>();
        currentState.UpdateState(this);
    }

    public void SwitchState(GuardStateBase state)
    {
        //FOR GUARDPATROLSTATE
        //Destroy(waypoints[0]);
        //Instantiate(waypoints[0], go.transform.position + new Vector3(-20, 0, 0), new Quaternion(0, 0, 0, 0));
        //Destroy(waypoints[1]);
        //Instantiate(waypoints[1], go.transform.position + new Vector3(20, 0, 0), new Quaternion(0, 0, 0, 0));
        //

        currentState = state;
        state.EnterState(this, waypoints);
    }


    public Transform getgenemyPos()
    {
        return enemyPos;
    }
}
