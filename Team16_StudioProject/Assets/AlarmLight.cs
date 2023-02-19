using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmLight : MonoBehaviour
{
    private Light flickeringLight;

    // Start is called before the first frame update
    void Start()
    {

        flickeringLight = GetComponent<Light>();
        flickeringLight.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
