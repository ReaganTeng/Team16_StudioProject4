using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeRotate : MonoBehaviour
{

    private DoorTrigger childscript;
    bool opened;



    // Start is called before the first frame update
    void Start()
    {
        childscript = gameObject.GetComponentInChildren<DoorTrigger>();
        opened = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(childscript.getdetected() == true
            && opened ==false )
        {
            //Quaternion.AngleAxis(-45, Vector3.up)/* * vector*/;

            transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
            opened = true;
            //Debug.Log("OPENED");
        }
    }
}
