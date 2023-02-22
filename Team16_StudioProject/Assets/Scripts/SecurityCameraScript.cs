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



    bool detected_player;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        detected_player = false;
        timer = 3;

        enemies = GameObject.Find("Enemy Manager");
        player = GameObject.Find("PlayerArmature");
        collider = GetComponent<Collider>();
        pos = transform.position;

        if (gameObject.GetComponent<Light>() == null)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        }


        if (waypoints.Length > 0) 
            navMeshAgent.SetDestination(waypoints[0].position);

        //collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (detected_player == true)
        {

            //timer -= 1 * Time.deltaTime;

            if (gameObject.GetComponent<Light>() == null)
            {
                //navMeshAgent.speed = 0;
                navMeshAgent.SetDestination(player.transform.position);
                gameObject.GetComponentInChildren<Light>().color = Color.red;
            }
            else
            {
                gameObject.GetComponent<Light>().color = Color.red;
            }

            if (Vector3.Distance(player.transform.position, gameObject.transform.position) > 3)
            {

                timer -= 1 * Time.deltaTime;
                if (timer <= 0)
                {
                    detected_player = false;
                }
            }
            else
            {
                timer = 3;
            }
        }
        else
        {
            if (gameObject.GetComponent<Light>() == null)
            {
                gameObject.GetComponentInChildren<Light>().color = Color.green;
                //navMeshAgent.speed = 10;


                if (gameObject.GetComponent<Light>() == null)
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

            }
            else
            {
                gameObject.GetComponent<Light>().color = Color.green;
            }

            timer = 3;
        }


        navMeshAgent.speed = 10;

    }


    void OnTriggerEnter(Collider other)
     {

        if (other.tag == "Player")
        {
            foreach (Transform child in enemies.transform)
            {
                child.GetComponent<GuardStateManager>().targetPosition = player.transform.position;
                child.GetComponent<GuardStateManager>().SwitchState(child.GetComponent<GuardStateManager>().SecurityState);


                detected_player = true;

            }
        }

     }
}
