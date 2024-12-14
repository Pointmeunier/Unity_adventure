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
        FaceDir = new Vector3(-transform.localScale.x, 0, 0);

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

    private void Flip()
    {
        // 翻轉怪物方向
        transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
    }
}