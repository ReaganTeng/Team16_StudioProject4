using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject enemies;

    private float timer;

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
        enemies = GameObject.Find("Enemies");

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
                if (distance < 20)
                {
                    if (child.GetComponent<GuardStateManager>().returnState() == child.GetComponent<GuardStateManager>().PatrolState
                    || child.GetComponent<GuardStateManager>().returnState() == child.GetComponent<GuardStateManager>().SearchState
                    || child.GetComponent<GuardStateManager>().returnState() == child.GetComponent<GuardStateManager>().StationState)
                    {
                        child.GetComponent<GuardStateManager>().SwitchState(child.GetComponent<GuardStateManager>().CoinState);
                    }

                }
            }
        }

    }
}
