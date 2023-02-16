using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WeaponUI : MonoBehaviour
{

    // GUIDE
    // To use this, add this script to the Canvas Gameobject,
    // then bind your desired Text Gameobject in Unity

    private PlayerStats playerStats;
    [SerializeField] TextMeshProUGUI  textMessage;
    [SerializeField] TextMeshProUGUI Numberofclips;
    private GameObject weaponUI;

    private int clips;



    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player Character").GetComponent<PlayerStats>();

    }

    // Update is called once per frame
    void Update()
    {

       clips = GameObject.Find("Player Character").GetComponent<PlayerStats>().ammoCount;

        Numberofclips.SetText(clips.ToString());

        switch (playerStats.equippedWeapon)
        {
            case PlayerStats.EquippedWeapon.Shiv:
                       
                textMessage.text = "Shiv Equipped";
                break;
            case PlayerStats.EquippedWeapon.Pistol:
                        

                textMessage.text = "Pistol Equipped";
                break;
        }

    }

}
