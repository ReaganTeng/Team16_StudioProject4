using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        float rotateDir = 0f;
       // if (Input.GetKey(KeyCode.Q)) rotateDir -= 1f;
        //if (Input.GetKey(KeyCode.E)) rotateDir += 1f;

        int edgeScrollSize = 20;

        if (Input.mousePosition.x < edgeScrollSize)
        {
            rotateDir -= 1f;
        }
        if (Input.mousePosition.x > Screen.width - edgeScrollSize)
        {
            rotateDir += 1f;
        }


        float rotateSpeed = 35f;
        transform.eulerAngles += new Vector3(0, rotateDir * rotateSpeed * Time.deltaTime, 0);
    }
}
