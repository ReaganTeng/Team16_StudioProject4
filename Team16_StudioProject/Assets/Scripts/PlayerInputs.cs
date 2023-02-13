using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputs : MonoBehaviour
{
    public Rigidbody projectile;
    private GameObject player;

    void Awake()
    {
        player = GameObject.Find("JohnLemon");
    }

    void Update()
    {
        // Left click to shoot projectile
        if (Input.GetMouseButtonDown(0))
        {
            // Instantiate the projectile at the position and rotation of this transform
            Rigidbody clone;
            clone = Instantiate(projectile, transform.position, transform.rotation);
            // Give the cloned object an initial velocity along the current
            // object's Z axis
            clone.position += Vector3.up * 1.0f;
            clone.position += player.transform.forward * 1.0f;
            clone.velocity = transform.TransformDirection(Vector3.forward * 10);
        }
    }

}