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
    public float hurtForce;

    public Transform attacker;

    [Header("�g���")]
    public float Exp;

    [Header("���A")]
    public bool isHurt;
    public bool isDead;

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
        if (!isHurt & !isDead)
        {
            Move();

        }
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
    public void OnTakeDamage(Transform attackerTrans)
    {
        attacker = attackerTrans;

        if(attackerTrans.position.x - transform.position.x > 0)
        {
            Flip();
        }
        if (attackerTrans.position.x - transform.position.x < 0)
        {
            Flip();
        }
        //���˳Q���h
        isHurt = true;
        ani.SetTrigger("Hurt");
        Vector2 dir = new Vector2(transform.position.x - attackerTrans.position.x, 0).normalized;

        rb.AddForce(dir*hurtForce, ForceMode2D.Impulse);
        
        StartCoroutine(OnHurt(dir));
    }
    private IEnumerator OnHurt(Vector2 dir)
    {
     
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.3f);
        isHurt = false;
    }

    public void Dead()
    {
        gameObject.layer = 2;
        ani.SetBool("Dead",true);
        isDead = true;
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

}
