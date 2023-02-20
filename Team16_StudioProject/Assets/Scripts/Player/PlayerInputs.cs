using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using StarterAssets;



public class PlayerInputs : MonoBehaviour
{
    public Rigidbody projectile;
    public Rigidbody coin;
    private GameObject player;
    private GameObject playerModel;
    private GameObject enemies;

    private GameObject gameOverDialog;
    private Vector3 pos;
    private PlayerStats playerStats;
    public ThirdPersonController thirdPersonController;
    private GameObject FirePoint;

    void Awake()
    {
        player = GameObject.Find("Player Character");
        playerModel = GameObject.Find("PlayerArmature");
        enemies = GameObject.Find("Enemies");
        playerStats = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>();
        gameOverDialog = GameObject.Find("Gameover Dialog");
        gameOverDialog.SetActive(false);
        FirePoint = GameObject.Find("PlayerCameraRoot");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.C))
        {
            playerStats.health-= 10;
        }

        if (playerStats.health <= 0)
        {
            // Disables player controls
            var controller = GameObject.Find("PlayerArmature").GetComponent<TPSController>();
            controller.enabled = false;
            var playerController = GameObject.Find("PlayerArmature").GetComponent<ThirdPersonController>();
            playerController.enabled = false;

            // Display the Gameover dialog
            gameOverDialog.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Restarts the scene.
                Scene scene = SceneManager.GetActiveScene(); 
                SceneManager.LoadScene(scene.name);
            }
            return;
        }

        //foreach (Transform child in enemies.transform)
        //{
        //    float distance = Vector3.Distance(child.position, transform.position);
        //    if (distance < 20)
        //        child.GetComponent<GuardStateManager>().SwitchState(child.GetComponent<GuardStateManager>().GunshotSoundState);
        //}

        // Throw a coin
        if (Input.GetKeyDown(KeyCode.LeftControl) 
            && playerStats.Numberofcoins > 0)
        {

            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(coin, playerModel.transform.position, playerModel.transform.rotation);
            pos = transform.position;

            clone.position = playerModel.transform.position;
            //clone.position += Vector3.up * 1.0f;
            //clone.position += Camera.main.transform.forward * 3.0f;
            //clone.velocity = transform.TransformDirection(Camera.main.transform.forward * 40);
            clone.position += playerModel.GetComponent<TPSController>().direction  * 3.0f;
            clone.velocity = transform.TransformDirection(playerModel.GetComponent<TPSController>().direction * 20);
            clone.MoveRotation(Quaternion.Euler(0.0f, playerModel.GetComponent<TPSController>().angle, 0.0f));
            //clone.MoveRotation(Camera.main.transform.rotation);

            playerStats.Numberofcoins -= 1;
        }

   
    
        // Left click 

        switch (playerStats.equippedWeapon)
        {
            case PlayerStats.EquippedWeapon.Shiv:
                       

                pos = playerModel.transform.position;

                if (Input.GetMouseButtonDown(0) && playerStats.shivDurability > 0)
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
                            //EventManager.Event.CheckForEnemies();
                            child.gameObject.SetActive(false);

                            playerStats.shivDurability--;
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
                    clone = Instantiate(projectile, playerModel.transform.position, playerModel.transform.rotation);
                    pos = transform.position;   

                    clone.position = FirePoint.transform.position;
                    clone.position -= new Vector3(0.0f, 0.5f, 0.0f);
                    clone.position += playerModel.GetComponent<TPSController>().direction * 2.0f;
                    clone.velocity = transform.TransformDirection(playerModel.GetComponent<TPSController>().direction * 30);
                    clone.MoveRotation(Quaternion.Euler(0.0f, playerModel.GetComponent<TPSController>().angle, 0.0f));

                    foreach (Transform child in enemies.transform)
                    {
                        float distance = Vector3.Distance(child.position, transform.position);
                        if (distance < 20)
                            child.GetComponent<GuardStateManager>().SwitchState(child.GetComponent<GuardStateManager>().GunshotSoundState);
                    }

                    playerStats.ammoCount--;
                }

                // Reloading pistol
                if (Input.GetKeyDown(KeyCode.R) && playerStats.clipCount > 0)
                {
                    playerStats.ammoCount = playerStats.maxAmmoCount;
                    playerStats.clipCount--;
                }

                break;



            //PUNCH
            case PlayerStats.EquippedWeapon.fists:

                pos = playerModel.transform.position;
                if (Input.GetMouseButtonDown(0))
                {
                    int i = 0;
                    foreach (Transform child in enemies.transform)
                    {
                        float distance = Vector3.Distance(child.position, pos);
                        if (distance < 1.5f)
                        {

                            child.GetComponent<GuardStateManager>().damage(5);

                            Debug.Log("PUNCH");
                            if (child.GetComponent<GuardStateManager>().health <= 0)
                            {
                                Destroy(child.gameObject);
                                List<GameObject> tmp = new List<GameObject>(EnemyManager.enemyManager.GetNumberOfEnemies());

                                tmp.RemoveAt(i);
                                EnemyManager.enemyManager.SetNumberOfEnemies(tmp.ToArray());
                                //EventManager.Event.CheckForEnemies();
                                child.gameObject.SetActive(false);
                            }
                            break;
                        }
                        i++;
                    }
                }
                break;
            //

            default:
                break;
        }

    }

}