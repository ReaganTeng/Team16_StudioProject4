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
        time = 1.0f;
        enemies = GameObject.Find("Wall");

    }


    void Update()
    {
        time -= Time.deltaTime;

        pos = transform.position;


        foreach (Transform child in enemies.transform)
        {
            float distance = Vector3.Distance(child.position, pos);
            if (distance < 3)
            {
                Destroy(child.gameObject);
                //Destroy(enemies.transform);
            }
        }

        if(time <= 0.0f)
        {
            Destroy(gameObject);
        }
    }


}
