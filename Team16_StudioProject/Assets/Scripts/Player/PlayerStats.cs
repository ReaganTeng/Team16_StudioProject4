using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health = 100;
    [SerializeField] public int ammoCount = 10;
    [SerializeField] private bool shootPistol = true;
    public InventoryObject inventory;

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

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
