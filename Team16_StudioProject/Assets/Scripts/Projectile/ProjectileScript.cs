using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ProjectileScript : MonoBehaviour
{

    float time;
    private GameObject[] enemies;
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
        //enemies = EnemyManager.enemyManager.GetNumberOfEnemies();
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
            int i = 0;


            if (collision.gameObject.tag != "Enemy")
            {
                Destroy(gameObject);
                break;
            }

            foreach (GameObject child in EnemyManager.enemyManager.GetNumberOfEnemies())
            {
                //Debug.Log("SHOT");

                float distance = Vector3.Distance(child.transform.position, pos);
                if (distance < 3.0f)
                {
                    child.GetComponent<GuardStateManager>().damage(30);
                    if (child.GetComponent<GuardStateManager>().health <= 0)
                    {

                        //INSTANTIATE COLLECTIBLE
                        child.GetComponent<GuardStateManager>().IntantiateObject_random();

                        Destroy(child);
                        child.SetActive(false);


                        List<GameObject> tmp = new List<GameObject>(EnemyManager.enemyManager.GetNumberOfEnemies());
                        //ArrayList enemyArr = new ArrayList(EnemyManager.enemyManager.GetNumberOfEnemies());
                        // GameObject enemyToRemove = child;
                        // int numIndex = enemyArr.IndexOf(enemyArr, i);

                        tmp.RemoveAt(i);
                        // enemyArr.RemoveAt(i);
                        EnemyManager.enemyManager.SetNumberOfEnemies(tmp.ToArray());
                        EventManager.Event.CheckForEnemies();
                    }


                    break;
                }
                i++;
                
            }
            
        }
        

    }


}
