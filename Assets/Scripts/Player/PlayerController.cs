using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PalyerInputControll inputControll;
    public Vector2 inputDirection;
    private Rigidbody2D rb;
    private PhysicsCheck physicscheck;
    [Header("速度參數")]
    public float Speed;
    public float JumpForce; 

    private void Awake()
    {
        physicscheck = GetComponent<PhysicsCheck>();
        inputControll = new PalyerInputControll();
        rb = GetComponent<Rigidbody2D>();

        inputControll.GamePlayer.Jump.started += Jump;
    }

    private void OnEnable()
    {
        inputControll.Enable();
    }

    private void OnDisable()
    {
        inputControll.Disable();
    }

    private void Update()
    {
        inputDirection = inputControll.GamePlayer.Move.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        rb.velocity = new Vector2(inputDirection.x *Speed * Time.deltaTime, rb.velocity.y);
        
        int faceDir =(int)transform.localScale.x;
        if(inputDirection.x < 0)
            faceDir = -1;
        if (inputDirection.x > 0)
            faceDir = 1;
        

        transform.localScale = new Vector3(faceDir,1,1);
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (physicscheck.isGround)
        rb.AddForce(transform.up*JumpForce,ForceMode2D.Impulse);
    }

}
