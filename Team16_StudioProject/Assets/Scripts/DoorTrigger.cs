using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    private GameObject player;
    private GameObject playerInventory;

    bool m_IsPlayerInRange;

    bool m_IsDetected = false;

    void OnTriggerEnter (Collider other)
    {
        if (player != null)
        {
            if (other.transform == player.transform)
            {
                m_IsPlayerInRange = true;
               
            }
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (player != null)
        {
            if (other.transform == player.transform)
            {
                m_IsPlayerInRange = false;
            }
        }



    }

    void Update ()
    {
        playerInventory = GameObject.Find("Player Character");
        player = GameObject.Find("PlayerArmature");



        if (m_IsPlayerInRange
             && m_IsDetected == false)
        {
            Vector3 direction = player.transform.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;


            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player.transform
                    && player.GetComponent<PlayerStats>().Numberofkeys > 0)
                {
                    player.GetComponent<PlayerStats>().Numberofkeys -= 1;
                    m_IsDetected = true;
                    //Debug.Log("DOOR OPENED");
                }
                else
                {
                    m_IsDetected = false;
                }
            }
        }
        else
        {
            //m_IsDetected = false;
           // Debug.Log("DOOR CLOSED");

        }
    }


    public bool getdetected()
    {
        return m_IsDetected;
    }
}
