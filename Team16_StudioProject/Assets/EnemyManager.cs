using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager enemyManager;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }
    void Awake()
    {
        if (enemyManager == null)
        {
            enemyManager = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetNumberOfEnemies(GameObject[] enemy)
    {
         enemies = enemy;
    }
    public GameObject[] GetNumberOfEnemies()
    {
        //enemies = GameObject.FindGameObjectsWithTag("Enemy");
        return enemies;
    }
}
