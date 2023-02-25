using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class globalstats : MonoBehaviour
{
    public static globalstats Instance; 
    
    public bool Enable_minimap;
    public bool Enable_notification;
    public bool Enable_Timer;
    public bool Enable_Username;
    public float volumeslider;

    public GameObject settingsGO;


    // Start is called before the first frame update
    void Start()
    {

        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        settingsGO = GameObject.Find("Settings");


        if (settingsGO != null)
        {
            //MINIMAP
            settingsGO.transform.Find("MinimapGO").GetComponentInChildren<Toggle>().isOn = Enable_minimap;
            settingsGO.transform.Find("MinimapGO").GetComponentInChildren<Toggle>().onValueChanged.AddListener(
                delegate
            {
                Enable_minimap = settingsGO.transform.Find("MinimapGO").GetComponentInChildren<Toggle>().isOn;
            });
            //


            //NOTIFICATIONS
            settingsGO.transform.Find("NotificationsGO").GetComponentInChildren<Toggle>().isOn
                = Enable_notification;
            settingsGO.transform.Find("NotificationsGO").GetComponentInChildren<Toggle>().onValueChanged.AddListener(
                delegate
            {
                Enable_notification =
                settingsGO.transform.Find("NotificationsGO").GetComponentInChildren<Toggle>().isOn;
            });
            //


            //TIMER
            settingsGO.transform.Find("TimerGO").GetComponentInChildren<Toggle>().isOn
                = Enable_Timer;
            settingsGO.transform.Find("TimerGO").GetComponentInChildren<Toggle>().onValueChanged.AddListener(
                delegate
                {
                    Enable_Timer =
                    settingsGO.transform.Find("TimerGO").GetComponentInChildren<Toggle>().isOn;
                });
            //


            //USERNAME
            settingsGO.transform.Find("UsernameGO").GetComponentInChildren<Toggle>().isOn
                = Enable_Username;
            settingsGO.transform.Find("UsernameGO").GetComponentInChildren<Toggle>().onValueChanged.AddListener(
                delegate
                {
                    Enable_Username =
                    settingsGO.transform.Find("UsernameGO").GetComponentInChildren<Toggle>().isOn;
                });
            //


            //VOLUME
            settingsGO.transform.Find("VolumeGO").GetComponentInChildren<Slider>().value
                = volumeslider;
            settingsGO.transform.Find("VolumeGO").GetComponentInChildren<Slider>().onValueChanged.AddListener(
                delegate
                {
                    volumeslider =
                    settingsGO.transform.Find("VolumeGO").GetComponentInChildren<Slider>().value;
                });
            //
            
        }
    }
}
