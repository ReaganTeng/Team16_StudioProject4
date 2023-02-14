using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectileScript : MonoBehaviour
{

    float time;
    public GameObject enemies;
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
        enemies = GameObject.Find("Enemies");


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
            foreach (Transform child in enemies.transform)
            {
                float distance = Vector3.Distance(child.position, pos);
                if (distance < 1.3f)
                {
                    // Destroy the enemy
                    Destroy(child.gameObject);
                    Destroy(gameObject);

                    break;
                }
            }



        }
        

    }


}
