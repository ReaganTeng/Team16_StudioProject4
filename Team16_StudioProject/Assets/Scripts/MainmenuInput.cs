using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Diagnostics;

public class MainmenuInput : MonoBehaviour
{

    private GameObject storyDialog;
    private GameObject mainMenu;

    // For story dialog 'animation'
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

        // For story dialog
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
}
