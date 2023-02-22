using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatZone : MonoBehaviour
{
    public GameObject entity;

    public bool m_InRange;

    public int zone_number;

    void OnTriggerEnter(Collider other)
    {
        if (entity != null)
        {
            if (other.transform == entity.transform)
            {
                m_InRange = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (entity != null)
        {
            if (other.transform == entity.transform)
            {
                m_InRange = false;
            }
        }
    }


    void Start()
    {

    }

    void Update()
    {
        

    }


    public int return_zonenumber()
    {
        return zone_number;
    }
}
