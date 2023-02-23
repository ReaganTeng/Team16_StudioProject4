using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHurtScript : MonoBehaviour
{
    // Start is called before the first frame update

    private GameObject renderImage;
    private Vector3 startPos;


    private GameObject player;
        public Vector3 hurtPos;

    public float time;

    public RawImage image;



    

    // Start is called before the first frame update
    void Start()
    {
        // playerStats = GameObject.Find("Player Character");
        // healthBarObject = GameObject.Find("Health Bar");
        // healthBar = GetComponent<Slider>();
        // healthBar.maxValue = 100;
        player = GameObject.Find("PlayerArmature");
        renderImage = GameObject.Find("HurtDirection");
        image = renderImage.GetComponent<RawImage>();
        startPos = renderImage.transform.position;
        hurtPos = transform.position;
        time = 2.0f;
        image.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

        float distance = 80;

        GameObject enemies = GameObject.Find("Enemy Manager");

        foreach (Transform enemyChild in enemies.transform)
        {

            if (enemyChild.gameObject.GetComponent<GuardStateManager>().hitPlayer)
            {
                hurtPos = enemyChild.gameObject.transform.position;
            }

        }



        Vector3 directionToTarget = player.transform.position - hurtPos;
        float angle = Vector3.Angle(Camera.main.transform.forward, directionToTarget);

        directionToTarget.x += -distance * Mathf.Cos(angle * 2 * Mathf.Deg2Rad);
        directionToTarget.y += -distance * Mathf.Sin(angle * 2 *  Mathf.Deg2Rad);


        transform.rotation = Quaternion.Euler(0, 0, angle * 2);
        transform.position = startPos + directionToTarget;

        time -= Time.deltaTime;
        if (time <= 0)
        {
            foreach (Transform enemyChild in enemies.transform)
            {

                if (enemyChild.gameObject.GetComponent<GuardStateManager>().hitPlayer)
                {
                    enemyChild.gameObject.GetComponent<GuardStateManager>().hitPlayer = false;
                }

            }
            image.enabled = false;
        }
    }

    public void SetEnabled(bool set)
    {
        image.enabled = set;
    }
}
