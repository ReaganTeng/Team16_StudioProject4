using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VirtualCamScript : MonoBehaviour
{

    private CinemachineVirtualCamera vcam;
    // Start is called before the first frame update
    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {

        // Zoom in/out by right click on the mouse       
        if (Input.GetMouseButton(1))
        {
            if (vcam.m_Lens.FieldOfView > 20)
            {
                vcam.m_Lens.FieldOfView -= 0.3f;
            }
        }
        else
        {
            if (vcam.m_Lens.FieldOfView < 40)
            {
                vcam.m_Lens.FieldOfView += 0.3f;
            }
        }
    }
}
