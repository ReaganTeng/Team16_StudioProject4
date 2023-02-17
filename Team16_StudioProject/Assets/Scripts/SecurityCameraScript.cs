using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SecurityCameraScript : MonoBehaviour
{

    private GameObject enemies;
    private GameObject player;
    private Collider collider;
    private Vector3 pos;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;
    public NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");
        player = GameObject.Find("PlayerArmature");
        collider = GetComponent<Collider>();
        pos = transform.position;
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;

        if (waypoints.Length > 0) 
            navMeshAgent.SetDestination(waypoints[0].position);

        //collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length > 0)
        {
            if (navMeshAgent.remainingDistance < 2)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
                navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
        }



    }


     void OnTriggerEnter(Collider other)
     {

        if (other.tag == "Player")
        {
            foreach (Transform child in enemies.transform)
            {
                        Debug.Log(transform.position);  
                  //  Debug.Log(pos);
                    child.GetComponent<GuardStateManager>().targetPosition = player.transform.position;
                  //  child.GetComponent<GuardStateManager>().SetTargetPosition(pos);
                    child.GetComponent<GuardStateManager>().SwitchState(child.GetComponent<GuardStateManager>().SecurityState);


            }
        }

     }
}
