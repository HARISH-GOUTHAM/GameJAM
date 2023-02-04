using System.Collections;
using System.Collections.Generic;
using harish.Player;
using Input;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Transform playerT;
    private CharacterController playerController;

    InputManagerScriptable inputManager;
    [SerializeField] private float moveSpeed = 10;
    [SerializeField] private float gravity = -10;
    [SerializeField] private float groundCheckDist = .51f;
    private Vector2 moveInp = Vector2.zero;
    
    
    
    void Start()
    {
        Debug.Log("started");
        inputManager = PlayerData.instance.inputManger;
        playerT = GetComponent<Transform>();
        playerController = GetComponent<CharacterController>();

        inputManager.OnMoveEv += GetMoveInput;

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    
        Gravity();
    }

    private Vector3 velocity;
    
    void Gravity()
    {
        
        if(IsGrounded() && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        
        //add gravity
        velocity.y += gravity * Time.deltaTime;
        playerController.Move(velocity * Time.deltaTime);
        
        
    }

    bool IsGrounded()
    {
        //raycast to see if we are grounded
        if(Physics.Raycast(playerT.position,Vector3.down, groundCheckDist))
        {
            return true;
        }
        else
        {
            return false;
        }
        
    }
    void Movement()
    {
        playerController.Move((playerT.forward * moveInp.y + playerT.right * moveInp.x) * (moveSpeed * 0.01f));
        Debug.Log("moved ig");
    }

    void GetMoveInput(Vector2 move)
    {
        moveInp = move;
    }
}
