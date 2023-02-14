using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;



public class PlayerInputs : MonoBehaviour
{
    public Rigidbody projectile;
    private GameObject player;
    private Vector3 pos;
    private PlayerStats playerStats;
    private ThirdPersonController thirdPersonController;

    void Awake()
    {
        player = GameObject.Find("NestedParentArmature_Unpack");
        playerStats = GetComponent<PlayerStats>();
        thirdPersonController = GetComponent<ThirdPersonController>();

    }

    void Update()
    {
        // Left click to shoot projectile
        if (Input.GetMouseButtonDown(0) && playerStats.ammoCount > 0)
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation);
            pos = transform.position;

            clone.position = Camera.main.transform.position;
            //clone.position += Vector3.up * 1.0f;
            clone.position += Camera.main.transform.forward * 3.0f;
            clone.velocity = transform.TransformDirection(Camera.main.transform.forward * 40);
            clone.MoveRotation(Camera.main.transform.rotation);

            playerStats.ammoCount--;
        }

    }

}