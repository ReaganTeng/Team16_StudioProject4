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
    [SerializeField] TextMeshProUGUI numberOfAmmo;


    [SerializeField] TextMeshProUGUI numberOfKeys;

    [SerializeField] TextMeshProUGUI numberOfcoins;


    private GameObject weaponUI;

    private GameObject pistolImage;
    private GameObject shivImage;

    private int ammo;
    private int clipCount;

    private int shivCount;

    private int keycount;
    private int coincount;


    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>();
        shivImage = GameObject.Find("Shiv Image");
        pistolImage =  GameObject.Find("Pistol Image");
        numberOfAmmo = GameObject.Find("Weapon Use Text").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {

      ammo = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>().ammoCount;
      clipCount = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>().clipCount;
      shivCount = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>().shivDurability;
        keycount = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>().Numberofkeys;
        coincount = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>().Numberofcoins;

        numberOfKeys.SetText("KEYS: " + keycount.ToString());
        numberOfcoins.SetText(coincount.ToString());

        switch (playerStats.equippedWeapon)
        {
            case PlayerStats.EquippedWeapon.Shiv:
                shivImage.GetComponent<RawImage>().enabled = true;
                pistolImage.GetComponent<RawImage>().enabled = false;
                numberOfAmmo.SetText("  " + shivCount.ToString());
                break;
            case PlayerStats.EquippedWeapon.Pistol:
                
                pistolImage.GetComponent<RawImage>().enabled = true;
                shivImage.GetComponent<RawImage>().enabled = false;

                numberOfAmmo.SetText(ammo.ToString() + "/" + clipCount.ToString());
                break;


        }

    }

}
