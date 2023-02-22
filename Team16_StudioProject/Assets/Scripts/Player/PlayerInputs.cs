using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;



public class PlayerInputs : MonoBehaviour
{
    public enum Gamestate
    {
        PAUSE,
        GAMEPLAY,
        GAMEOVER,
        MAINMENU,
        // add more here if possible
        NUMSTATES

    }

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

    public AudioSource ShootAudioSource;
    public AudioSource ReloadAudioSource;
    public GamestateManager gameState;



    void Awake()
    {
        player = GameObject.Find("Player Character");
        playerModel = GameObject.Find("PlayerArmature");
        enemies = GameObject.Find("Enemy Manager");
        playerStats = GameObject.Find("PlayerArmature").GetComponent<PlayerStats>();
        FirePoint = GameObject.Find("PlayerCameraRoot");


        gameState = GameObject.Find("Gamestate Manager").GetComponent<GamestateManager>();

    }

    



    void Update()
    {
        // Don't update if NOT IN gameplay state
        if (gameState.currentState != GamestateManager.Gamestate.GAMEPLAY)
            return;


        if (Input.GetKeyDown(KeyCode.C))
        {
            playerStats.health -= 10;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameState.currentState = GamestateManager.Gamestate.PAUSE;
        }

        if (playerStats.health <= 0)
        {
            gameState.currentState = GamestateManager.Gamestate.GAMEOVER;
        }

        // Throw a coin
        if (Input.GetKeyDown(KeyCode.LeftControl)
            && playerStats.Numberofcoins > 0)
        {

            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(coin, playerModel.transform.position, playerModel.transform.rotation);
            pos = transform.position;
            clone.position = playerModel.transform.position;
            clone.position += playerModel.GetComponent<TPSController>().direction * 3.0f;
            clone.velocity = new Vector3(0, 0, 0);
            clone.velocity = transform.TransformDirection(playerModel.GetComponent<TPSController>().direction * 20);
            clone.MoveRotation(Quaternion.Euler(0.0f, playerModel.GetComponent<TPSController>().angle, 0.0f));

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
                            //INSTANTIATE COLLECTIBLE
                            child.gameObject.GetComponent<GuardStateManager>().IntantiateObject_random();

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
                    Debug.Log("Shoot");
                    // Instantiate the projectile at the position and rotation of this transform
                    PlayShootSound();
                    Rigidbody clone;
                    clone = Instantiate(projectile, playerModel.transform.position, playerModel.transform.rotation);
                    pos = transform.position;

                    clone.position = FirePoint.transform.position;
                    clone.position -= new Vector3(0.0f, 0.5f, 0.0f);
                    clone.position += playerModel.GetComponent<TPSController>().direction * 2.0f;
                    clone.velocity = transform.TransformDirection(playerModel.GetComponent<TPSController>().direction * 30);
                    clone.MoveRotation(Quaternion.Euler(0.0f, playerModel.GetComponent<TPSController>().angle, 0.0f));

                    if (enemies != null)
                    {

                        foreach (Transform child in enemies.transform)
                        {
                            float distance = Vector3.Distance(child.position, transform.position);
                            if (distance < 40)
                                child.GetComponent<GuardStateManager>().SwitchState(child.GetComponent<GuardStateManager>().GunshotSoundState);
                        }
                    }

                    playerStats.ammoCount--;
                }

                // Reloading pistol
                if (Input.GetKeyDown(KeyCode.R) && playerStats.clipCount > 0)
                {
                    playerStats.ammoCount = playerStats.maxAmmoCount;
                    playerStats.clipCount--;
                    PlayReloadSound();
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

                            child.gameObject.GetComponent<GuardStateManager>().damage(5);

                            Debug.Log("PUNCH");
                            if (child.GetComponent<GuardStateManager>().health <= 0)
                            {
                                //INSTANTIATE COLLECTIBLE
                                child.GetComponent<GuardStateManager>().IntantiateObject_random();

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
    public void PlayShootSound()
    {
        ShootAudioSource.Play();
    }
    public void PlayReloadSound()
    {
        ReloadAudioSource.Play();
    }

}