using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBarScript : MonoBehaviour
{
    private GameObject playerStats;
    private GameObject healthBarObject;
    private Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerArmature");
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Don't render at full health
        if (GetComponentInParent<GuardStateManager>().health == 100)
        {
            healthBar.value = 0;
        }
        else
        {
            healthBar.value = GetComponentInParent<GuardStateManager>().health;
        }

        this.transform.rotation = Quaternion.Euler(0.0f, 45.0f, 0.0f);

    }
}
