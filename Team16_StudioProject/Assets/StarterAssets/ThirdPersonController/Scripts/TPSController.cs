using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;

public class TPSController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;

    private StarterAssetsInputs starterAssetsInputs;
    // Start is called before the first frame update
    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (starterAssetsInputs.aim)
        {
            aimVirtualCamera.gameObject.SetActive(true);
        }
        else
        {
            aimVirtualCamera.gameObject.SetActive(false);
            Debug.Log("CLOWN");
        }
    }
}
