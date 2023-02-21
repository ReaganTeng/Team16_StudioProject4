using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseInput : MonoBehaviour
{

    public GameObject player;
    private GameObject pauseScreen;


    // Start is called before the first frame update
    void Start()
    {
        //player = GameObject.Find("PlayerArmature").GetComponent<PlayerInputs>();
        pauseScreen = GameObject.Find("Pause Screen");
        
                            

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ResumeButton()
    {
        player = GameObject.Find("Player Character");
        pauseScreen = GameObject.Find("Pause Screen");
        player.GetComponent<PlayerInputs>().currentState = PlayerInputs.Gamestate.GAMEPLAY;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);

    }
}
