using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{

    float time;
    public Vector3 startPos;
    private GameObject players;
    private Vector3 pos;
    private GameObject playerStats;
    private GameObject renderHurtImage; 


    // Start is called before the first frame update

    void Awake()
    {

    }

    void Start()
    {
        // Set a life time for the projectile
        time = 1.0f;

        // GameObjects that the projectile can collide with.
        players = GameObject.Find("PlayerArmature");
        renderHurtImage = GameObject.Find("HurtDirection");
        //playerStats = GameObject.Find("Player Character");
    }


    void Update()
    {
        time -= Time.deltaTime;

        pos = transform.position;

        // Projectile despawns when reaches zero
        if (time <= 0.0f)
        {
            Destroy(gameObject);
        }


    }

    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {


            foreach (Transform child in players.transform)
            {
                float distance = Vector3.Distance(child.position, pos);
                if (distance < 1.7f)
                {
                    Destroy(gameObject);
                    players.GetComponent<PlayerStats>().health -= 10;

                    GameObject enemies = GameObject.Find("Enemy Manager");

                    foreach (Transform enemyChild in enemies.transform)
                    {
                        enemyChild.gameObject.GetComponent<GuardStateManager>().hitPlayer = false;
                        if (startPos == enemyChild.gameObject.GetComponent<GuardStateManager>().shootStartPos)
                        {
                            enemyChild.gameObject.GetComponent<GuardStateManager>().hitPlayer = true;
                        }
                    }


                    renderHurtImage.GetComponent<PlayerHurtScript>().SetEnabled(true);
                    renderHurtImage.GetComponent<PlayerHurtScript>().time = 2;
                    renderHurtImage.GetComponent<PlayerHurtScript>().hurtPos = pos;

                    break;
                }
            }

        }
        

    }


}
