using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject enemies;

    private float timer;

    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        //enemies = GameObject.Find("Enemies");
        timer = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // rb.velocity.x--;  
        //Debug.Log(transform.position);
        enemies = GameObject.Find("Enemy Manager");

        player = GameObject.Find("PlayerArmature");


        timer -= Time.deltaTime;

        if(timer <= 0)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (enemies != null)
        {
            foreach (Transform child in enemies.transform)
            {
                float distance = Vector3.Distance(child.position, transform.position);
                if (distance < 50
                    && (child.GetComponent<GuardStateManager>().returnzoneNumber() == player.GetComponent<PlayerStats>().returnzoneNumber())
                    )
                {
                    //Debug.Log("COIN SEEN");
                    if (child.GetComponent<GuardStateManager>().returnState() == child.GetComponent<GuardStateManager>().PatrolState
                    || child.GetComponent<GuardStateManager>().returnState() == child.GetComponent<GuardStateManager>().SearchState
                    || child.GetComponent<GuardStateManager>().returnState() == child.GetComponent<GuardStateManager>().StationState)
                    {
                        child.GetComponent<GuardStateManager>().SS(child.GetComponent<GuardStateManager>().CoinState);
                    }

                }
                //else/* if (child.GetComponent<GuardStateManager>().returnzoneNumber() != player.GetComponent<PlayerStats>().returnzoneNumber())*/
                //{
                //    Debug.Log("COIN NOT SEEN");
                //}
            }
        }

    }
}
