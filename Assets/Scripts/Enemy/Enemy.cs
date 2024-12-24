using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class Enemy : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator ani;

    PhysicsCheck physicsCheck;

    [Header("�t�װѼ�")]
    public float NormalSpeed;
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
    public bool IsFacingRight; // true ���ܪ�l���V�k

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

    public void Update()
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

        // �p������̻P�Ǫ��������۹��m�A�îھڦ�m½��Ǫ�
        if (attackerTrans.position.x - transform.position.x > 0)
        {
            Flip(); // �p�G�����̦b�Ǫ��k��A½��Ǫ�
        }
        if (attackerTrans.position.x - transform.position.x < 0)
        {
            Flip(); // �p�G�����̦b�Ǫ�����A½��Ǫ�
        }

        // ��ܨ��˰ʵe
        isHurt = true;
        ani.SetTrigger("Hurt");


        Vector2 dir = new Vector2(transform.position.x - attackerTrans.position.x, 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

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
        ani.SetBool("Dead", true);
        isDead = true;
        string filePath = "./Assets/Resources/Data.json";
        string jsonData = File.ReadAllText(filePath);
        JObject jsonObject = JObject.Parse(jsonData);
        string monsterName = ani.name;

        // 嘗試找到匹配的怪物
        if (jsonObject["Enemy"]?[monsterName] != null)
        {
            jsonObject["Enemy"][monsterName]["alive"] = false;
        }
        File.WriteAllText(filePath, jsonObject.ToString());

        Debug.Log(monsterName + "修改完成");
    }

    public void DestroyEnemy()
    {
        Destroy(this.gameObject);
    }

}