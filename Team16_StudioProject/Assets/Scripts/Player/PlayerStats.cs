using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public enum EquippedWeapon
    {
        Shiv,
        Pistol,

        fists,

        NUM_TYPES
    }

    [SerializeField] public int health = 100;
    [SerializeField] public int ammoCount = 10;
    [SerializeField] public int clipCount = 1;
    [SerializeField] public int shivDurability = 2;
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


    private int zoneno;

    void Start()
    {

        gunequipped = true;
        healthBar = GameObject.Find("Health Bar");
        maxAmmoCount = 12;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);

            if(item.item == firstaid)
            {
                health += 20;
                Destroy(other.gameObject);


            }
            else if (item.item == ammoclip)
            {
                clipCount += 1;
                Destroy(other.gameObject);

            }
            else if (item.item == shiv
                && shivDurability <= 0)
            {
                shivDurability = 2;
                Destroy(other.gameObject);

            }
            else if (item.item == coin)
            {
                Numberofcoins += 1;
                Destroy(other.gameObject);

            }
            else if (item.item == key)
            {
                Numberofkeys += 1;
                Destroy(other.gameObject);

            }
            else if (item.item == pistol)
            {
                gunequipped = true;
                Destroy(other.gameObject);

            }


        }
    }

    void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && equippedWeapon < EquippedWeapon.NUM_TYPES - 1)
        {
            equippedWeapon++;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && equippedWeapon > 0)
        {
            equippedWeapon--;
        }


        zone = GameObject.FindGameObjectsWithTag("Z");


        for (int i = 0; i < zone.Length; i++)
        {
            zone[i].GetComponent<WhatZone>().entity = gameObject;
            Debug.Log("SCALE " +  i + " " + zone[i].GetComponent<Transform>().localScale.x )/*= 3*/;

            if (/*zone[i].GetComponent<WhatZone>().m_InRange == true
             && */gameObject.transform.position.x < zone[i].GetComponent<Transform>().position.x + (zone[i].GetComponent<Transform>().localScale.x/2)
             && gameObject.transform.position.x > zone[i].GetComponent<Transform>().position.x - (zone[i].GetComponent<Transform>().localScale.x / 2)
             && gameObject.transform.position.z > zone[i].GetComponent<Transform>().position.z - (zone[i].GetComponent<Transform>().localScale.z / 2)
            && gameObject.transform.position.z < zone[i].GetComponent<Transform>().position.z + (zone[i].GetComponent<Transform>().localScale.z / 2)

             )
            //if (zone[i].GetComponent<Collider>() == gameObject.GetComponent<Transform>())
            {
                zoneno = zone[i].GetComponent<WhatZone>().zone_number;
                Debug.Log("PLAYER ZONE " + zoneno);

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
