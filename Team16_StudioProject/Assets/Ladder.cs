using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class Ladder : MonoBehaviour
{
    GameObject Armature;
    void Start()
    {
        Armature = GameObject.Find("PlayerArmature");
    }
    void OnTriggerEnter()
    {
        Armature.GetComponent<ThirdPersonController>().SetClimbing(true);
    }
    void OnTriggerExit()
    {
        Armature.GetComponent<ThirdPersonController>().SetClimbing(false);
    }
}
