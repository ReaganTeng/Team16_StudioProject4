using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainmenuInput : MonoBehaviour
{

    private GameObject storyDialog;
    private GameObject mainMenu;

    // Start is called before the first frame update
    void Awake()    
    {

        storyDialog = GameObject.Find("Story");
        mainMenu = GameObject.Find("Mainmenu");
    }

    // Update is called once per frame
    void Update()
    {
        // Begin gameplay when pressing ENTER in story dialog
        if (mainMenu.activeSelf)
            storyDialog.SetActive(false);

        if (storyDialog.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("LevelScene");
            }
        }


    }

    public void StartButton()
    {
        storyDialog.SetActive(true);
        mainMenu.SetActive(false);
    }
}
