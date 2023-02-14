using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");
    
    }

    // Update is called once per frame
    void Update()
    {
       // rb.velocity.x--;  
       Debug.Log(transform.position);
    }

    void OnCollisionEnter(Collision collision)
    {
            foreach (Transform child in enemies.transform)
            {
                float distance = Vector3.Distance(child.position, transform.position);
                if (distance < 100)
                {

                    child.GetComponent<GuardStateManager>().SwitchState(child.GetComponent<GuardStateManager>().CoinState);

                }
            }

    }
}
