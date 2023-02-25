using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter: MonoBehaviour
{

    public int timeSecond;
    public int timeMinute;
    // Start is called before the first frame update
    void Awake()
    {
        timeSecond = 0;
        timeMinute = 0;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
