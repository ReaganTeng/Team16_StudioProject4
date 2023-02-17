using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmFlickering : MonoBehaviour
{
    public static AlarmFlickering LightSource;
    public Light flickeringLight;
    void Awake()
    {
        if (LightSource == null)
        {
            LightSource = this;
        }
        else
        {
            Destroy(gameObject);
        }


        //DontDestroyOnLoad(gameObject);
    }
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
