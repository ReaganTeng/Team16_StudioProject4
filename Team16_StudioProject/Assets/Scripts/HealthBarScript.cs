using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{

    private GameObject playerStats;
    private Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("Player Character");
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = 100;

    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = playerStats.GetComponent<PlayerStats>().health;
    }
}
