using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class WinScreenInput : MonoBehaviour
{
    public GameObject lb_canvas;
    [SerializeField] private TextMeshProUGUI PosInfo;
    [SerializeField] private TextMeshProUGUI nameInfo;
    [SerializeField] private TextMeshProUGUI timeInfo;
    [SerializeField] private TextMeshProUGUI lastPlayedInfo;

    // Start is called before the first frame update
    void Start()
    {
        lb_canvas.SetActive(false);
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
        lb_canvas.SetActive(true);
        StartCoroutine(GlobalStuffs.GetScoreBoard(PosInfo, nameInfo, timeInfo, lastPlayedInfo));
    }

    public void CloseLB()
    {
        CleanLeaderboard();
        lb_canvas.SetActive(false);
    }
    public void CleanLeaderboard()
    {
        PosInfo.text = "";
        nameInfo.text = "";
        timeInfo.text = "";
        lastPlayedInfo.text = "";
    }
}
