using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreenInput : MonoBehaviour
{
    public Canvas lb_canvas;

    // Start is called before the first frame update
    void Start()
    {
        lb_canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("LevelScene");
    }


    public void OpenLB()
    {
        lb_canvas.enabled = true;
    }

    public void CloseLB()
    {
        lb_canvas.enabled = false;
    }
}
