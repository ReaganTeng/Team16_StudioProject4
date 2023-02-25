using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsScript : MonoBehaviour
{
    //SettingsStats
    public globalstats settingsGO;

    //Enable_notification

    public RawImage minimap;
    public TextMeshProUGUI notification;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI username;


    // Start is called before the first frame update
    void Start()
    {
        settingsGO = GameObject.Find("SettingsStats").GetComponent<globalstats>();

        
        if (settingsGO.Enable_minimap == false)
        {
            minimap.enabled = false;
        }
        else
        {
            minimap.enabled = true;
        }


        if (settingsGO.Enable_notification == false)
        {
            notification.enabled = false;
        }
        else
        {
            notification.enabled = true;
        }

        if (settingsGO.Enable_Timer == false)
        {
            timer.enabled = false;
        }
        else
        {
            timer.enabled = true;
        }


        if (settingsGO.Enable_Username == false)
        {
            username.enabled = false;
        }
        else
        {
            username.enabled = true;
        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
