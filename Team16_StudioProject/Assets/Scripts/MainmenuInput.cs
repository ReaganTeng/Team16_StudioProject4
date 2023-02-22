using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainmenuInput : MonoBehaviour
{

    private GameObject storyDialog;
    private GameObject mainMenu;
    private GameObject settings;
    private GameObject howToPlay;

    // For story scrolling text
    int storyIndex;
    TextMeshProUGUI storyText;
    string tempText;
    string newText;
    float time;

    // Start is called before the first frame update
    void Awake()    
    {

        storyDialog = GameObject.Find("Story");
        mainMenu = GameObject.Find("Mainmenu");
        settings = GameObject.Find("Settings");
        howToPlay = GameObject.Find("How To Play");

        // For story scrolling text
        storyIndex = 0;
        storyText = GameObject.Find("Story Text").GetComponent<TextMeshProUGUI>();
        tempText = storyText.text;
        newText = "";
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {

        StartCoroutine(MainMenu());

        if (storyDialog.gameObject.activeSelf)
            StartCoroutine(LoadStoryText());

    }

    IEnumerator MainMenu()
    {
        // Render only mainmenu
        if (mainMenu.gameObject.activeSelf)
        {
            storyDialog.SetActive(false);
            settings.SetActive(false);
            howToPlay.SetActive(false);
        }


        if (storyDialog.gameObject.activeSelf)
        {
            // Begin gameplay when pressing ENTER in story dialog
            // Can ENTER no matter if text has finished scrolling or not.
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("LevelScene");
            }
        }

        yield return new WaitForSeconds(Time.deltaTime);
    }

    IEnumerator LoadStoryText()
    {

        time -= Time.deltaTime;
        if (newText != tempText && time <= 0)
        {
            newText += tempText[storyIndex];
            storyText.SetText(newText);
            storyIndex++;
            time = 0.03f;
        }

        yield return new WaitForSeconds(Time.deltaTime);
    }

    public void StartButton()
    {
        storyDialog.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void SettingsButton()
    {
        settings.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void HowToPlayButton()
    {
        howToPlay.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
    }   
}
