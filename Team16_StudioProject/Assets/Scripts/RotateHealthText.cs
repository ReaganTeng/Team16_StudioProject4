using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RotateHealthText : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI healthtext;
    private int parent;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        player = GameObject.Find("PlayerArmature");
        parent = GetComponentInParent<GuardStateManager>().health;

        healthtext.SetText(parent.ToString());


        var lookpos = (player.transform.position - transform.position);
        lookpos.y = 0;
        var rotation = Quaternion.LookRotation(lookpos);


        transform.rotation = Quaternion.Slerp(
            transform.rotation , rotation, Time.deltaTime * 10
            ) /*- GetComponentInParent<GuardStateManager>().getgenemyPos().rotation*/;
    }
}
