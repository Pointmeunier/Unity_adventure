using System.Collections;
using UnityEngine;

public class FrogBoss : Enemy
{
    [Header("追擊範圍設定")]
    public float chaseRange; // 追擊範圍
    public float boostedSpeed; // 追擊時的加速速度
    private Transform player; // 玩家目標
    private bool isChasing; // 是否正在追擊
    private Coroutine idleCoroutine; // 控制閒置狀態的 Coroutine

    private BoxCollider2D boxCollider;
    private float offsetTimer = 0f;

    [Header("攻擊設定")]
    public float attackRange; // 攻擊範圍
    public float attackCooldown = 1.5f; // 攻擊冷卻時間
    private bool canAttack = true; // 是否能攻擊

    private void Start()
    {
        // 找到玩家
        player = GameObject.FindGameObjectWithTag("Player").transform;

        boxCollider = GetComponent<BoxCollider2D>();

        // 初始化狀態
        isChasing = false;
        CurrentSpeed = NormalSpeed; // 設定初始速度為正常速度
        ani.SetBool("Walk", true);  // 設定初始動畫為走路動畫
    }

    private void Update()
    {
        base.Update();

        if (player == null)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // 玩家進入追擊範圍
        if (distanceToPlayer <= chaseRange && !isDead)
        {
            if (!isChasing)
            {
                isChasing = true;
                CurrentSpeed = boostedSpeed; // 增加速度
                ani.SetBool("Walk", true); // 確保切換為走路動畫
                if (idleCoroutine != null)
                {
                    StopCoroutine(idleCoroutine);
                    idleCoroutine = null;
                }
            }

            // 檢查攻擊條件
            if (distanceToPlayer <= attackRange && CanAttackPlayer())
            {
                TriggerAttack(); // 撥放攻擊動畫
            }
        }
        else if (isChasing && !isDead) // 玩家離開追擊範圍
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
        CurrentSpeed = 0; // 停止移動
        ani.SetBool("Walk", false); // 切換為閒置動畫
        yield return new WaitForSeconds(1f); // 閒置1秒
        CurrentSpeed = NormalSpeed; // 恢復正常速度
        ani.SetBool("Walk", true); // 切換回走路動畫
    }

    public override void Move()
    {
        if (CurrentSpeed > 0 && !isDead) // 確保只有在移動時進行操作
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
        // 檢查玩家是否在 Boss 面前
        float directionToPlayer = player.position.x - transform.position.x;
        bool isPlayerInFront = (directionToPlayer > 0 && transform.localScale.x > 0) ||
                               (directionToPlayer < 0 && transform.localScale.x < 0);

        return isPlayerInFront && canAttack;
    }

    private void TriggerAttack()
    {
        ani.SetTrigger("Attack"); // 撥放攻擊動畫
        StartCoroutine(AttackCooldown());
    }

    private IEnumerator AttackCooldown()
    {
        canAttack = false; // 暫時不能攻擊
        yield return new WaitForSeconds(attackCooldown); // 等待冷卻時間
        canAttack = true; // 恢復攻擊能力
    }
}
