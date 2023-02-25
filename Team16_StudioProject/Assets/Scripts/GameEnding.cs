
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnding : MonoBehaviour
{
   
    public GameObject player;
       
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            GlobalStuffs.isCompleted = true;
        }
    }

   


}
