using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

using UnityEngine;

public class GuardStateManager : MonoBehaviour
{
    public GuardStateBase currentState;
    public GuardPatrolState PatrolState = new GuardPatrolState();
    public GuardChaseState ChaseState = new GuardChaseState();
    public GuardCoinState CoinState = new GuardCoinState();
    public GuardStationaryState StationState = new GuardStationaryState();
    public GuardSearchState SearchState = new GuardSearchState();
    public GuardSecurityState SecurityState = new GuardSecurityState();
    public GuardAlarmedState AlarmedState = new GuardAlarmedState();
    public GuardGunshotSoundState GunshotSoundState = new GuardGunshotSoundState();


    //REINITIALISE VALUE
    //FOR GUARDPATROLSTATE
    public Transform[] waypoints;
    public Vector3 targetPosition;
    public NavMeshAgent navMeshAgent;
    //
    private GameObject pov;
    private GameObject pov2;

    private GameObject player;
    Transform enemyPos;
    public Rigidbody projectile;

    public string curstate;

    private Observer[] childscript;

    public int health;


    public GameObject pistolClip;
    public GameObject shiv;
    public GameObject coin;


    void Awake()
    {
        //FOR GUARDPATROLSTATE
        //waypoints[0].Transform.position = GetComponent<Transform>().position + new Vector3(-20, 0, 0);
        //Instantiate(waypoints[0], go.transform.position + new Vector3(-20, 0, 0), new Quaternion(0, 0, 0, 0));
        //waypoints[1].Transform.position = GetComponent<Transform>().position + new Vector3(20, 0, 0);
        //Instantiate(waypoints[1], go.transform.position + new Vector3(20, 0, 0), new Quaternion(0, 0, 0, 0));
        //
        player = GameObject.Find("PlayerArmature");
    }


    //FIRS UPDATE LOOP, SET TO PATROL

    // Start is called before the first frame update
    void Start()
    {
        //WHEN GUARD IS FIRST INSTANTIATED, MAKE IT PATROL
        currentState = PatrolState;
        if (curstate == "Station")
        {
            currentState = StationState;
        }
        else/* if (curstate == "Station")*/
        {
            currentState = PatrolState;
        }
        //

        health = 100;

        //Debug.Log("WAYPOINTS ARE: " + waypoints[0].position);
        currentState.EnterState(this, waypoints);


        childscript = gameObject.GetComponentsInChildren<Observer>();

    }



    public bool returnObserver(int i)
    {
        return childscript[i].getdetected();
    }

    public void IntantiateObject()
    {
        Instantiate(shiv, enemyPos.position, Quaternion.identity);
    }



    // Update is called once per frame
    void Update()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        enemyPos = GetComponent<Transform>();


        //if(health <= 0)
        //{

        //    IntantiateObject();
        //    Debug.Log("INSTANTIATE");
        //}

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
        if (waypoints.Length == 0)
            waypoints = EventManager.Event.SortWaypoints();

        currentState = state;
        state.EnterState(this, waypoints);
    }
    public void SwitchState(GuardStateBase state, Transform[] newWP)
    {
        currentState = state;
        state.EnterState(this, newWP);
    }



    public Vector3 gettargetpos()
    {
        return targetPosition;
    }

    public Transform getgenemyPos()
    {
        return enemyPos;
    }

    public Transform getplayerPos()
    {
        return player.transform;
    }

    public GameObject getplayer()
    {
        return player;
    }

    public void damage(int dmg)
    {
        health -= dmg;
    }

    public GameObject getpov1()
    {
        return pov;
    }


    public GameObject getpov2()
    {
        return pov2;
    }

    public GuardStateBase returnState()
    {
        return currentState;
    }


    

}
