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


           Vector3 newPosition = startPos;

        Vector3 directionToTarget = player.transform.position - hurtPos;
        float angle = Vector3.Angle(Camera.main.transform.forward, directionToTarget);

        
         var x = -distance * Mathf.Cos(angle * 2 * Mathf.Deg2Rad);
        var y = -distance * Mathf.Sin(angle * 2 *  Mathf.Deg2Rad);

        newPosition.x += x;
        newPosition.y += y;



        transform.rotation = Quaternion.Euler(0, 0, angle * 2);
        transform.position = newPosition;


        time -= Time.deltaTime;
        if (time <= 0)
        {
            image.enabled = false;
        }
    }

    public void SetEnabled(bool set)
    {
        image.enabled = set;
    }
}
