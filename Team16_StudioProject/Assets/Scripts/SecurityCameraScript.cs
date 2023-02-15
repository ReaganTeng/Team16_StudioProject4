using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCameraScript : MonoBehaviour
{

    private GameObject enemies;
    private GameObject player;
    private Collider collider;
    private Vector3 pos;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.Find("Enemies");
        player = GameObject.Find("PlayerArmature");
        collider = GetComponent<Collider>();
        pos = transform.position;

        //collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


     void OnTriggerEnter(Collider other)
     {

        if (other.tag == "Player")
        {
            foreach (Transform child in enemies.transform)
            {
                  //  Debug.Log(pos);
                    child.GetComponent<GuardStateManager>().targetPosition = player.transform.position;
                  //  child.GetComponent<GuardStateManager>().SetTargetPosition(pos);
                    child.GetComponent<GuardStateManager>().SwitchState(child.GetComponent<GuardStateManager>().SecurityState);


            }
        }

     }
}
