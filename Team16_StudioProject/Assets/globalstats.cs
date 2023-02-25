using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class globalstats : MonoBehaviour
{
    public Toggle toggle;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

        if (toggle.isOn)
        {
            Debug.Log("MINIMAP ENABLED");
        }
        else
        {
            Debug.Log("MINIMAP DISABLED");
        }
    }
}
