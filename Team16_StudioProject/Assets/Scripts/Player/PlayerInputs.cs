using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;



public class PlayerInputs : MonoBehaviour
{
    public Rigidbody projectile;
    public Rigidbody coin;
    private GameObject player;
    private GameObject playerModel;
    private GameObject enemies;
    private Vector3 pos;
    private PlayerStats playerStats;
    public ThirdPersonController thirdPersonController;

    void Awake()
    {
        player = GameObject.Find("Player Character");
        playerModel = GameObject.Find("PlayerArmature");
        enemies = GameObject.Find("Enemies");
        playerStats = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>();
    }

    void Update()
    {
        // Left click to shoot projectile

        // if (Input.GetButtonDown("Fire1"))
        // {

        //     foreach (Transform child in enemies.transform)
        //     {
        //         float distance = Vector3.Distance(child.position, pos);
        //         if (distance < 1.5f)
        //         {
        //             // Destroy the enemy
        //             Destroy(child.gameObject);
        //             break;
        //         }
        //     }
        // }


        // Throw a coin
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {

            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(coin, transform.position, transform.rotation);
            pos = transform.position;

            clone.position = Camera.main.transform.position;
            //clone.position += Vector3.up * 1.0f;
            clone.position += Camera.main.transform.forward * 3.0f;
            clone.velocity = transform.TransformDirection(Camera.main.transform.forward * 40);
            clone.MoveRotation(Camera.main.transform.rotation);


        }

    
        // Left click 

        switch (playerStats.equippedWeapon)
        {
            case PlayerStats.EquippedWeapon.Shiv:
                       

                pos = playerModel.transform.position;

                if (Input.GetMouseButtonDown(0))
                {
                    int i = 0;
                    foreach (Transform child in enemies.transform)
                    {
                        float distance = Vector3.Distance(child.position, pos);
                        if (distance < 1.5f)
                        {
                            // Destroy the enemy
                            Destroy(child.gameObject);
                            List<GameObject> tmp = new List<GameObject>(EnemyManager.enemyManager.GetNumberOfEnemies());

                            tmp.RemoveAt(i);
                            EnemyManager.enemyManager.SetNumberOfEnemies(tmp.ToArray());
                            EventManager.Event.CheckForEnemies();
                            break;
                        }
                        i++;
                    }
                }


                break;
            case PlayerStats.EquippedWeapon.Pistol:
                        

                if (Input.GetMouseButtonDown(0) && playerStats.ammoCount > 0)
                {
                    // Instantiate the projectile at the position and rotation of this transform
                    Rigidbody clone;
                    clone = Instantiate(projectile, transform.position, transform.rotation);
                    pos = transform.position;   

                    clone.position = Camera.main.transform.position;
                    //clone.position += Vector3.up * 1.0f;
                    clone.position += Camera.main.transform.forward * 3.0f;
                    clone.velocity = transform.TransformDirection(Camera.main.transform.forward * 40);
                    clone.MoveRotation(Camera.main.transform.rotation);

                    playerStats.ammoCount--;
                }

                // Reloading pistol
                if (Input.GetKeyDown(KeyCode.R) && playerStats.clipCount > 0)
                {
                    playerStats.ammoCount = playerStats.maxAmmoCount;
                    playerStats.clipCount--;
                }

                break;
        }

    }

}