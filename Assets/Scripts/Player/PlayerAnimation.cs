using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator ani;
    private Rigidbody2D rb;
    private PhysicsCheck physicsCheck;
    
    private PlayerController playerController;

    public void Awake()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerController = GetComponent<PlayerController>();
    }

    public void Update()
    {
           SetAmination();
    }

    public void SetAmination()
    {
        //偵測玩家速度播放跑步動畫
        ani.SetFloat("SpeedX",Mathf.Abs(rb.velocity.x));
        //偵測玩家Y值速度播放跳躍動畫
        ani.SetFloat("SpeedY", rb.velocity.y);
        ani.SetBool("isGround", physicsCheck.isGround);
        //死亡
        ani.SetBool("isDead", playerController.isDead);
    }
    
    public void PlayHurt()
    {
        ani.SetTrigger("hurt");
        
    }


}
    
    
