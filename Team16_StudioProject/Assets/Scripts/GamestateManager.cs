using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GamestateManager : MonoBehaviour
{

    public enum Gamestate
    {
        PAUSE,
        GAMEPLAY,
        GAMEOVER,
        MAINMENU,
        // add more here if possible
        NUMSTATES

    }
    
    public Gamestate currentState;
    private GameObject gameOverDialog;
    private GameObject pauseScreen;



    // Start is called before the first frame update
    void Start()
    {
        currentState = Gamestate.GAMEPLAY;

        gameOverDialog = GameObject.Find("Gameover Dialog");
        gameOverDialog.SetActive(false);
        pauseScreen = GameObject.Find("Pause Screen");
        pauseScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        switch(currentState)
        {
            case Gamestate.MAINMENU:
                break;
            case Gamestate.PAUSE:


                // This stops activity on the gameplay side
                Time.timeScale = 0;
                pauseScreen.SetActive(true);

                //  Return to Gameplay
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    currentState = Gamestate.GAMEPLAY;
                    pauseScreen.SetActive(false);

                    Time.timeScale = 1;

                }

                break;

            case Gamestate.GAMEOVER:



                // Display the Gameover dialog
                Time.timeScale = 1;

                gameOverDialog.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    // Restarts the scene.
                    Scene scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.name);
                }
                break;

            case Gamestate.GAMEPLAY:


                break;

            default:
                break;
        }

    }

   
}
