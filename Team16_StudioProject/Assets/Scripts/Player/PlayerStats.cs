using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public enum EquippedWeapon
    {
        Fists,
        Shiv,
        Pistol,

        NUM_TYPES
    }

    [SerializeField] public int health = 100;
    [SerializeField] public int ammoCount = 10;
    [SerializeField] public int clipCount = 1;
    [SerializeField] public int shivDurability = 0;
    public int maxAmmoCount;
    [SerializeField] private bool shootPistol = true;
    [SerializeField] public int Numberofkeys = 2;
    [SerializeField] public int Numberofcoins = 1;

    private GameObject[] zone;


    public EquippedWeapon equippedWeapon;
    private GameObject healthBar;

    public InventoryObject inventory;

    public CollectibleObject firstaid;
    public CollectibleObject ammoclip;
    public CollectibleObject coin;
    public CollectibleObject key;
    public bool gunequipped;
    public EquipmentObject shiv;
    public EquipmentObject pistol;

    private TextMeshProUGUI obtainText;
    private float obtainTimer;

    private int maxhealth = 100;

    public bool[] weapon = new bool[3];

    // For time taken in game ending
    private TextMeshProUGUI timerText;
    public int timeCounterSecond;
    public int timeCounterMinute;
    private float counterTimer;
    private TimeCounter timerObject;



    private int zoneno;

    void Start()
    {
        for(int i = 0; i< 3; i++)
        {
            weapon[i] = false;
        }
        weapon[0] = true;


       // gunequipped = false;
        healthBar = GameObject.Find("Health Bar");
        obtainText = GameObject.Find("Obtained Text").GetComponent<TextMeshProUGUI>();
        obtainText.SetText("");
        timerText = GameObject.Find("Timer").GetComponent<TextMeshProUGUI>();
        timerObject = GameObject.Find("TimerObject").GetComponent<TimeCounter>();
        obtainTimer = 0;
        maxAmmoCount = 12;
        equippedWeapon = EquippedWeapon.Fists;

        timeCounterSecond = 0;
        timeCounterMinute = 0;
        counterTimer = 1;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            obtainTimer = 2;

            if(item.item == firstaid
                && health < maxhealth)
            {
                obtainText.SetText("Restored 20 health.");
                health += 20;

                Destroy(other.gameObject);

                if (health > maxhealth)
                {
                    health = maxhealth;
                }
            }
            else if (item.item == ammoclip)
            {
                obtainText.SetText("Pistol Clip Obtained.");
                clipCount += 1;
                Destroy(other.gameObject);

            }
            else if (item.item == shiv
                && shivDurability <= 0)
            {
                obtainText.SetText("Shiv Obtained.");
                shivDurability = 2;
                //equippedWeapon = EquippedWeapon.Shiv;


                Destroy(other.gameObject);
            }
            else if (item.item == coin)
            {
                obtainText.SetText("Coin Obtained.");
                Numberofcoins += 1;
                Destroy(other.gameObject);

            }
            else if (item.item == key)
            {
                obtainText.SetText("Key Obtained.");
                Numberofkeys += 1;
                Destroy(other.gameObject);

            }
            else if (item.item == pistol)
            {
                obtainText.SetText("Pistol Obtained.");
                gunequipped = true;

                weapon[2] = true;

                Destroy(other.gameObject);

            }
        }
    }

    void Update()
    {

        // Time counter
        counterTimer -= Time.deltaTime;
        if (counterTimer <= 0)
        {
            counterTimer += 1;
            timeCounterSecond++;
            if (timeCounterSecond == 60)
            {
                timeCounterMinute++;
                timeCounterSecond = 0;
            }
        }
        if (timeCounterSecond > 9)
        {
            timerText.SetText(timeCounterMinute.ToString() + ":" + timeCounterSecond);
        }
        else
        {
            timerText.SetText(timeCounterMinute.ToString() + ":0" + timeCounterSecond);
        }
        timerObject.timeSecond = timeCounterSecond;
        timerObject.timeMinute = timeCounterMinute;     



        if (obtainTimer > 0)
        {
            obtainTimer -= Time.deltaTime;

            if (obtainTimer <= 0)
            {
                obtainText.SetText("");
            }
        }

        if(Input.GetAxis("Mouse ScrollWheel") > 0 && equippedWeapon < EquippedWeapon.NUM_TYPES - 1)
        {
            Debug.Log("inc");
            EquippedWeapon tempweapon = equippedWeapon;
            equippedWeapon++;

            while (weapon[(int)equippedWeapon] == false)
            {
                equippedWeapon++;

                if ((int)equippedWeapon > 2)
                {
                    equippedWeapon = tempweapon;
                    break;
                }
            }

        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && equippedWeapon > 0)
        {
            Debug.Log("dec");
            EquippedWeapon tempweapon = equippedWeapon;
            equippedWeapon--;

            while (weapon[(int)equippedWeapon] == false)
            {
                equippedWeapon--;

                if((int)equippedWeapon < 0)
                {
                    equippedWeapon = tempweapon;
                    break;
                }
            }
        }


        if(shivDurability > 0)
        {
            weapon[1] = true;
        }
        else
        {
            weapon[1] = false;
        }


        if (gunequipped == true)
        {
            weapon[2] = true;
        }
        else
        {
            weapon[2] = false;
        }

        //switch (equippedWeapon)
        //{
        //    case EquippedWeapon.Shiv:
        //        {

        //            if (shivDurability <= 0)
        //            {
        //                equippedWeapon = EquippedWeapon.Fists;
        //                break;
        //            }
        //            break;
        //        }
        //    case EquippedWeapon.Pistol:
        //        {
        //            if (gunequipped == false)
        //            {
        //                equippedWeapon = EquippedWeapon.Fists;
        //                break;
        //            }
        //            break;
        //        }
        //    default:
        //        {
        //            break;
        //        }
        //}

        Debug.Log("EQUPPED WEAPON " + equippedWeapon);


      


        zone = GameObject.FindGameObjectsWithTag("Z");

        for (int i = 0; i < zone.Length; i++)
        {
            if (/*zone[i].GetComponent<WhatZone>().m_InRange == true
             && */gameObject.transform.position.x < zone[i].GetComponent<Transform>().position.x + (zone[i].GetComponent<Transform>().localScale.x/2)
             && gameObject.transform.position.x > zone[i].GetComponent<Transform>().position.x - (zone[i].GetComponent<Transform>().localScale.x / 2)
             && gameObject.transform.position.z > zone[i].GetComponent<Transform>().position.z - (zone[i].GetComponent<Transform>().localScale.z / 2)
            && gameObject.transform.position.z < zone[i].GetComponent<Transform>().position.z + (zone[i].GetComponent<Transform>().localScale.z / 2)

             )
            //if (zone[i].GetComponent<Collider>() == gameObject.GetComponent<Transform>())
            {
                zoneno = zone[i].GetComponent<WhatZone>().zone_number;
                //Debug.Log("PLAYER ZONE " + zoneno);

            }
        }


    }


    public int returnzoneNumber()
    {
        return zoneno;
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
    public int GetHealth()
    {
        return health;
    }
}
