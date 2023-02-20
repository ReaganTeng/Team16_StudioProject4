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



    public EquippedWeapon equippedWeapon;
    private GameObject healthBar;

    public InventoryObject inventory;

    public CollectibleObject firstaid;
    public CollectibleObject ammoclip;
    public CollectibleObject coin;


    public EquipmentObject shiv;


    void Start()
    {
        healthBar = GameObject.Find("Health Bar");
        maxAmmoCount = ammoCount;
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
                Debug.Log("COIN COLLECTED");
                Numberofcoins += 1;
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
