using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator ani;

    PhysicsCheck physicsCheck;

    [Header("�t�װѼ�")]
    public float NormalSpeed;
    public float ChaseSpeed;
    public float CurrentSpeed;

    public Vector3 FaceDir;
    [Header("�g���")]
    public float Exp;

    [Header("�Ϥ���l���V")]
    public bool IsFacingRight; // true ��ܪ�l���V�k

    // ½��N�o�ɶ�
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
        // �ھڬO�_��l���V�k�ӭp�� FaceDir
        FaceDir = IsFacingRight
            ? new Vector3(transform.localScale.x, 0, 0)
            : new Vector3(-transform.localScale.x, 0, 0);

        // �Ǫ������½��A���[�J�N�o����
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
        // ½��Ǫ���V
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
}
