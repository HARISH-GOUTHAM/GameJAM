using System.Collections;
using System.Collections.Generic;
using harish.Player;
using Input;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{

    private Transform playerT;
    [SerializeField] private Transform playerCam;
    
    [SerializeField] private float mouseSensitivity = 100f;

    private Vector2 mouseInput;

    private InputManagerScriptable inputManager;
    
    // Start is called before the first frame update
    void Start()
    {
        inputManager = PlayerData.instance.inputManger;
        playerT = GetComponent<Transform>();

        inputManager.OnMouseMoveEv += GetMouseInput;
    }

    // Update is called once per frame
    void Update()
    {
        FirstPersonCameraController();
    }

    private float xRotation = 0;
    void FirstPersonCameraController()
    {
        //rotate the player based on delta mouse input
        playerT.Rotate(Vector3.up * mouseInput.x * mouseSensitivity * Time.deltaTime);
        //rotate the camera based on delta mouse input
        
        
        //clamp rotation of the camera
        xRotation += -mouseInput.y * mouseSensitivity * Time.deltaTime;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        playerCam.localRotation = Quaternion.Euler(xRotation, 0,0);


    }
    
    void GetMouseInput(Vector2 mouseInput)
    {
        Debug.Log(mouseInput);
        this.mouseInput = mouseInput;
    }
    
}
