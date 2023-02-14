using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyProjectile : MonoBehaviour
{

    float time;
    private GameObject players;
    private Vector3 pos;


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
                if (distance < 1.3f)
                {
                    // Destroy the enemy
                    //Destroy(child.gameObject);
                    Destroy(gameObject);
                    Debug.Log("YOURE SHOT");

                    break;
                }
            }

        }
        

    }


}
