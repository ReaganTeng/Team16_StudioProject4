using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public enum EquippedWeapon
    {
        Shiv,
        Pistol,
        NUM_TYPES
    }

    [SerializeField] public int health = 100;
    [SerializeField] public int ammoCount = 10;
    [SerializeField] private bool shootPistol = true;

    [SerializeField] public int Numberofkeys = 1;

    public EquippedWeapon equippedWeapon;
    private GameObject healthBar;

    public InventoryObject inventory;

    void Start()
    {
        healthBar = GameObject.Find("Health Bar");
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided");
        var item = other.GetComponent<Item>();
        if (item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
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
