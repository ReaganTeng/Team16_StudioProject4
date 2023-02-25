using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PrintTimeTaken : MonoBehaviour
{

    private TimeCounter timeCounter;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = GameObject.Find("TimerObject").GetComponent<TimeCounter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeCounter.timeSecond > 9)
        {
            this.GetComponent<TextMeshProUGUI>().SetText("Time taken " + timeCounter.timeMinute + ":" + timeCounter.timeSecond);
        }
        else
        {
            this.GetComponent<TextMeshProUGUI>().SetText("Time taken " + timeCounter.timeMinute + ":0" + timeCounter.timeSecond);
        }
    }
}
