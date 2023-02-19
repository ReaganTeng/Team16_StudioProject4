using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using StarterAssets;
using UnityEngine.InputSystem;

public class TPSController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimVirtualCamera;
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private LayerMask aimColliderLayerMask = new LayerMask();
    [SerializeField] private LayerMask groundMask;

    [SerializeField] private Transform debugTransform;
    private Camera mainCamera;

    private GameObject[] LadderObjects;

    public Vector3 pos;

    private ThirdPersonController thirdPersonController;
    private StarterAssetsInputs starterAssetsInput;
    // Start is called before the first frame update
    private void Awake()
    {
        mainCamera = Camera.main;
        thirdPersonController = GetComponent<ThirdPersonController>();
        starterAssetsInput = GetComponent<StarterAssetsInputs>();
        LadderObjects = GameObject.FindGameObjectsWithTag("Ladder");
    }

    // Update is called once per frame
    private void Update()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Aim();

        ////Debug.Log(transform.position.x + "," + transform.position.y + "," + transform.position.z);
        //Vector3 mouseWorldPosition = Vector3.zero;
        //Vector2 screenCenterPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
        //Ray ray = Camera.main.ScreenPointToRay(screenCenterPoint);

        //pos = thirdPersonController.transform.position;
        //// DO NOT UNCOMMENT THIS CODE
        //if (Physics.Raycast(ray, out RaycastHit raycastHit, 999f, aimColliderLayerMask))
        //{
        //    //debugTransform.position = raycastHit.point;
        //    mouseWorldPosition = raycastHit.point;
        //}
        //////////////////////////////////////////////////////////////////////////////////////
        //if (starterAssetsInput.aim)
        //{
        //    aimVirtualCamera.gameObject.SetActive(true);
        //    thirdPersonController.SetSensitivity(aimSensitivity);
        //    thirdPersonController.SetRotateOnMove(false);

        //    Vector3 worldAimTarget = mouseWorldPosition;
        //    worldAimTarget.y = transform.position.y;
        //    Vector3 aimDirection = (worldAimTarget - transform.position).normalized;

        //    transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 20f);
        //}
        //else
        //{
        //    aimVirtualCamera.gameObject.SetActive(false);
        //    thirdPersonController.SetSensitivity(normalSensitivity);
        //    thirdPersonController.SetRotateOnMove(true);

        //    // Debug.Log("CLOWN");
        //}
        CheckForCollision();
       
    }
    void CheckForCollision()
    {
        foreach (GameObject ladder in LadderObjects)
        {
            float distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(ladder.transform.position.x, 0, ladder.transform.position.z));
            if (distance < 1 && transform.position.y < ladder.GetComponent<BoxCollider>().size.y + ladder.transform.position.y + ladder.transform.localScale.y)
            {
                //Debug.Log("Player Pos:" + transform.position.y);
                //Debug.Log("Ladder Height:" + (ladder.GetComponent<BoxCollider>().size.y + ladder.transform.position.y));
                thirdPersonController.SetClimbing(true);
            }
            else
            {
                thirdPersonController.SetClimbing(false);
            }

        }
    }
    private void Aim()
    {
        var (success, position) = GetMousePosition();
        if (success)
        {
            // Calculate the direction
            var direction = position - transform.position;

            // You might want to delete this line.
            // Ignore the height difference.
            direction.y = 0;

            // Make the transform look in the direction.
            transform.forward = direction;
        }
    }
    private (bool success, Vector3 position) GetMousePosition()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, groundMask))
        {
            // The Raycast hit something, return with the position.
            debugTransform.position = hitInfo.point;
            return (success: true, position: hitInfo.point);
        }
        else
        {
            // The Raycast did not hit anything.
            return (success: false, position: Vector3.zero);
        }
    }
}
