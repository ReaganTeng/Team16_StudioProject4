using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreenInput : MonoBehaviour
{
    public Canvas lb_canvas;
    [SerializeField] private TextMeshProUGUI leaderboardInfo;
    // Start is called before the first frame update
    void Start()
    {
        if (lb_canvas != null)
        {
            lb_canvas.enabled = false;
        }
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
        StartCoroutine(GlobalStuffs.GetScoreBoard(leaderboardInfo));
    }

    public void CloseLB()
    {
        lb_canvas.enabled = false;
    }
}
