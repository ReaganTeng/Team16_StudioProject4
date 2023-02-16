using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderWeaponModel : MonoBehaviour
{

    private PlayerStats playerStats;
    private GameObject shivModel;
    private GameObject pistolModel;


    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>();
        shivModel = GameObject.Find("Shiv Model");
        pistolModel = GameObject.Find("Pistol Model");

    }

    // Update is called once per frame
    void Update()
    {
        switch (playerStats.equippedWeapon)
        {
            case PlayerStats.EquippedWeapon.Shiv:
                       
                shivModel.SetActive(true);
                pistolModel.SetActive(false);       
                break;
            case PlayerStats.EquippedWeapon.Pistol:
                shivModel.SetActive(false);
                pistolModel.SetActive(true);                          
                break;
        }
    }
}
