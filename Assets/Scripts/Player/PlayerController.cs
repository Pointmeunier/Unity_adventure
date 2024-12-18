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
    private CapsuleCollider2D coll;
    private PlayerAnimation playerAnimation;

    [Header("速度參數")]
    public float Speed;
    public float JumpForce;

    [Header("物體材質")]

    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D ground;

    [Header("狀態")]
    public bool isHurt;
    public float HurtForce;

    public bool isDead;
    public bool isAttack;
    private void Awake()
    {
        physicscheck = GetComponent<PhysicsCheck>();
        inputControll = new PalyerInputControll();
        rb = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<PlayerAnimation>();
        coll = GetComponent<CapsuleCollider2D>();
        //跳躍
        inputControll.GamePlayer.Jump.started += Jump;
        //攻擊  
        inputControll.GamePlayer.Attack.started += PlayerAttack;
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
        CheckState();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            Move();
        }


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

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        if (physicscheck.isGround)
        {
            playerAnimation.PlayAttack();
            isAttack = true;
        }
    }

        #region UnityEvent
        public void GetHurt(Transform attacker)
    {
        isHurt = true;
        rb.velocity = Vector2.zero;
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x),0).normalized;

        rb.AddForce(dir * HurtForce,ForceMode2D.Impulse);
      
    }
    public void Dead()
    {
        isDead = true;
        inputControll.GamePlayer.Disable();
    }
    #endregion

    //檢測在地面或牆壁 切換物理材質球
    private void CheckState()
    {
        coll.sharedMaterial = physicscheck.isGround ? ground : wall;
    }
}
