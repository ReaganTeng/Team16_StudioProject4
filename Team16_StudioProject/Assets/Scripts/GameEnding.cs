
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEnding : MonoBehaviour
{
   
    public GameObject player;
   

    bool m_IsPlayerAtExit;
    
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    void Update ()
    {
        if (m_IsPlayerAtExit)
        {
            SceneManager.LoadScene("WinScreen");
        }
        
    }

   


}
