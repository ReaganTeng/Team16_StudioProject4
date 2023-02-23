using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HingeRotate : MonoBehaviour
{

    private DoorTrigger childscript;
    bool opened;
    float rotationValue = 0.0f;


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
            //transform.Rotate(0.0f, -90.0f, 0.0f, Space.World);
            opened = true;
            //Debug.Log("OPENED");
        }
        if (opened && rotationValue < 90)
        {
            transform.eulerAngles += new Vector3(0, -20 * Time.deltaTime, 0);
            rotationValue += 20 * Time.deltaTime;
        }
    }
}
