using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    private GameObject player;

    bool m_IsPlayerInRange;

    bool m_IsDetected = false;

    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player.transform)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player.transform)
        {
            m_IsPlayerInRange = false;
        }
    }

    void Update ()
    {

        player = GameObject.Find("PlayerArmature");

        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.transform.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
            
            if (Physics.Raycast (ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player.transform)
                {
                    m_IsDetected = true;
                }
                else
                {
                    m_IsDetected = false;
                }
            }
        }
        else
        {
            m_IsDetected = false;
        }
    }


    public bool getdetected()
    {
        return m_IsDetected;
    }
}
