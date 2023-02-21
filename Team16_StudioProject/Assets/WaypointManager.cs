using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointManager : MonoBehaviour
{
    public static WaypointManager waypointManager;
    // Start is called before the first frame update
    void Awake()
    {
        if (waypointManager == null)
        {
            waypointManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

}
