using System.Collections;
using System.Collections.Generic;
using harish.Player;
using Input;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform playerT;
    private CharacterController playerController;
   
    [SerializeField] InputManagerScriptable inputManager;
    [SerializeField] private float moveSpeed = 10;
    private Vector2 moveInp = Vector2.zero;
    
    void Start()
    {
        playerT = GetComponent<Transform>();
        playerController = GetComponent<CharacterController>();

        inputManager.OnMoveEv += GetMoveInput;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        playerController.Move((playerT.forward * moveInp.y + playerT.right * moveInp.x) * moveSpeed);
    }

    void GetMoveInput(Vector2 move)
    {
        moveInp = move;
    }
}
