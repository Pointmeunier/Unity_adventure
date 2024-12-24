using System.Collections;
using UnityEngine;

public class FrogBoss : Enemy
{
    [Header("�l���d��]�w")]
    public float chaseRange; // �l���d��
    public float boostedSpeed; // �l���ɪ��[�t�t��
    private Transform player; // ���a�ؼ�
    private bool isChasing; // �O�_���b�l��
    private Coroutine idleCoroutine; // ����m���A�� Coroutine

    private BoxCollider2D boxCollider;
    private float offsetTimer = 0f;

    [Header("�����]�w")]
    public float attackRange; // �����d��
    public float attackCooldown = 1.5f; // �����N�o�ɶ�
    private bool canAttack = true; // �O�_�����

    private void Start()
    {
        // ��쪱�a
        player = GameObject.FindGameObjectWithTag("Player").transform;

        boxCollider = GetComponent<BoxCollider2D>();

        // ��l�ƪ��A
        isChasing = false;
        CurrentSpeed = NormalSpeed; // �]�w��l�t�׬����`�t��
        ani.SetBool("Walk", true);  // �]�w��l�ʵe�������ʵe
    }

    private void Update()
    {
        base.Update();

        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // ���a�i�J�l���d��
        if (distanceToPlayer <= chaseRange && !isDead)
        {
            if (!isChasing)
            {
                isChasing = true;
                CurrentSpeed = boostedSpeed; // �W�[�t��
                ani.SetBool("Walk", true); // �T�O�����������ʵe
                if (idleCoroutine != null)
                {
                    StopCoroutine(idleCoroutine);
                    idleCoroutine = null;
                }
            }

            // �ˬd��������
            if (distanceToPlayer <= attackRange && CanAttackPlayer())
            {
                TriggerAttack(); // ��������ʵe
            }
        }
        else if (isChasing && !isDead) // ���a���}�l���d��
        {
            isChasing = false;
            if (idleCoroutine == null)
            {
                idleCoroutine = StartCoroutine(IdleThenWalk());
            }
        }
    }

    private IEnumerator IdleThenWalk()
    {
        CurrentSpeed = 0; // �����
        ani.SetBool("Walk", false); // ���������m�ʵe
        yield return new WaitForSeconds(1f); // ���m1��
        CurrentSpeed = NormalSpeed; // ��_���`�t��
        ani.SetBool("Walk", true); // �����^�����ʵe
    }

    public override void Move()
    {
        if (CurrentSpeed > 0 && !isDead) // �T�O�u���b���ʮɶi��ާ@
        {
            base.Move();
            UpdateBoxColliderOffset();
        }
    }

    private void UpdateBoxColliderOffset()
    {
        if (boxCollider != null)
        {
            offsetTimer += Time.deltaTime;
            float offsetX = Mathf.Lerp(-0.17f, -0.25f, Mathf.PingPong(offsetTimer, 1f));
            boxCollider.offset = new Vector2(offsetX, boxCollider.offset.y);
        }
    }

    private bool CanAttackPlayer()
    {
        // �ˬd���a�O�_�b Boss ���e
        float directionToPlayer = player.position.x - transform.position.x;
        bool isPlayerInFront = (directionToPlayer > 0 && transform.localScale.x > 0) ||
                               (directionToPlayer < 0 && transform.localScale.x < 0);

        return isPlayerInFront && canAttack;
    }

    private void TriggerAttack()
    {
        ani.SetTrigger("Attack"); // ��������ʵe
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // �Ȯɤ������
        yield return new WaitForSeconds(attackCooldown); // ���ݧN�o�ɶ�
        canAttack = true; // ��_������O
    }
}
