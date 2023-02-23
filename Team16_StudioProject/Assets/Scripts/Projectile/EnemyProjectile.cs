using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{

    float time;
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
        players = GameObject.Find("PlayerArmature");

        // GameObjects that the projectile can collide with.
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
                    Debug.Log("HEALTH " + players.GetComponent<PlayerStats>().health);
                    renderHurtImage.GetComponent<PlayerHurtScript>().SetEnabled(true);
                    renderHurtImage.GetComponent<PlayerHurtScript>().time = 2;
                    renderHurtImage.GetComponent<PlayerHurtScript>().hurtPos = pos;

                    break;
                }
            }

        }
        

    }


}
