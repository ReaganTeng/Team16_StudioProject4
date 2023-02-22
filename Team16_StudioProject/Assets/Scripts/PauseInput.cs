using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading;

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
        GameObject gameState = GameObject.Find("Gamestate Manager");
        pauseScreen = GameObject.Find("Pause Screen");
        gameState.GetComponent<GamestateManager>().currentState = GamestateManager.Gamestate.GAMEPLAY;
        Time.timeScale = 1;
        pauseScreen.SetActive(false);

    }

    public void QuitButton()
    {
        Time.timeScale = 1;
        GameObject gameState = GameObject.Find("Gamestate Manager");
        gameState.GetComponent<GamestateManager>().currentState = GamestateManager.Gamestate.GAMEPLAY;
        SceneManager.LoadScene("MainMenu");

    }
}

