using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator ani;

    PhysicsCheck physicsCheck;

    [Header("速度參數")]
    public float NormalSpeed;
    public float ChaseSpeed;
    public float CurrentSpeed;

    public Vector3 FaceDir;
    [Header("經驗值")]
    public float Exp;

    [Header("圖片初始面向")]
    public bool IsFacingRight; // true 表示初始面向右

    // 翻轉冷卻時間
    private float flipCooldown = 1f;
    private float lastFlipTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        CurrentSpeed = NormalSpeed;
    }

    private void Update()
    {
        // 根據是否初始面向右來計算 FaceDir
        FaceDir = IsFacingRight
            ? new Vector3(transform.localScale.x, 0, 0)
            : new Vector3(-transform.localScale.x, 0, 0);

        // 怪物撞牆後翻轉，但加入冷卻限制
        if ((physicsCheck.touchLeftWall || physicsCheck.touchRightWall) && Time.time - lastFlipTime > flipCooldown)
        {
            Flip();
            lastFlipTime = Time.time;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        rb.velocity = new Vector2(CurrentSpeed * FaceDir.x * Time.deltaTime, rb.velocity.y);
    }

    protected void Flip()
    {
        // 翻轉怪物方向
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
