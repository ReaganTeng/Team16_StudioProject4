using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
public class HealthBarScript : MonoBehaviour
{
    private GameObject playerStats;
    private GameObject healthBarObject;
    private Slider healthBar;

    // For animation
    private float currentValue;

    private bool healthBarMoveUp;
    private float healthBarTransform;
    private Vector3 healthBarStartPos;

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.Find("PlayerArmature");
        healthBarObject = GameObject.Find("Health Bar");
        healthBar = GetComponent<Slider>();
        healthBar.maxValue = playerStats.GetComponent<PlayerStats>().health;
        currentValue = healthBar.maxValue;

        healthBarMoveUp = true;
        healthBarTransform = 0;
        healthBarStartPos = healthBarObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentValue > playerStats.GetComponent<PlayerStats>().health)
        {
            currentValue -= 50 * Time.deltaTime;
            if (currentValue < playerStats.GetComponent<PlayerStats>().health)
            {
                currentValue = playerStats.GetComponent<PlayerStats>().health;
                healthBarTransform = 0;
            }
            else
            {
                // Handle health bar shaking animation
                if (healthBarMoveUp)
                {
                    healthBarTransform += 200 * Time.deltaTime;

                    if (healthBarTransform >= 5)
                    {
                        healthBarMoveUp = false;
                    }
                }
                else
                {
                    healthBarTransform -= 200 * Time.deltaTime;

                    if (healthBarTransform <= -5)
                    {
                        healthBarMoveUp = true;
                    }
                }
            }


            healthBarObject.transform.position = healthBarStartPos + (new Vector3 (0, healthBarTransform, 0));
        }
        healthBar.value = currentValue;

    }
}
