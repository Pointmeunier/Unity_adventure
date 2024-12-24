using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PalyerInputControll inputControll;
    public Vector2 inputDirection;
    
    public Attack[] attackComponents; // damage 

    private Character character; // MaxHP

    private Rigidbody2D rb;
    private PhysicsCheck physicscheck;
    private CapsuleCollider2D coll;
    private PlayerAnimation playerAnimation;

    [Header("�t�װѼ�")]
    public float Speed;
    public float JumpForce;

    [Header("�������")]

    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D ground;

    [Header("���A")]
    public float defaultSpeed;       // speed copy recover
    public float defaultJump;       // jump copy recover
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
        //���D
        inputControll.GamePlayer.Jump.started += Jump;
        //����  
        inputControll.GamePlayer.Attack.started += PlayerAttack;
        defaultSpeed = Speed;
        defaultJump = JumpForce;
        character = GetComponent<Character>();
    }



    private void OnEnable()
    {
        inputControll.Enable();
    }

    private void OnDisable()
    {
        inputControll.Disable();
    }
    private void Start()
    {
        // 递归获取 Player 所有子对象中的 Attack 组件
        attackComponents = GetComponentsInChildren<Attack>(true); // `true` 表示包括未激活的对象
        
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

      #region player_status_plant

    public void IncreaseAttack(int amount)
    {
        for (int i = 0; i < attackComponents.Length; i++)
            {
                attackComponents[i].damage += amount; // 修改每个 Attack 的伤害值
                Debug.Log($"目前第{i + 1}段攻擊力：{attackComponents[i].damage}");
            }
    }

    public void RestoreHealth(float amount)
    {
        character.RestoreHealth(amount); // use Character 
    }
    public void BoostSpeed(float multiplier)
    {
        Speed *= multiplier;
        Debug.Log("速度提升，目前速度: " + Speed);

        // 恢复速度（例如3秒后恢复）
        Invoke(nameof(ResetSpeed), 3f);
    }

    private void ResetSpeed()
    {
        Speed = defaultSpeed;
        Debug.Log("速度恢復，目前速度: " + Speed);
    }

    public void BoostJump(float amount)
    {
        JumpForce *= amount;
        Debug.Log("跳躍提升5秒，目前跳躍: " + JumpForce);
        // 恢復跳躍（例如5秒后恢复）
        Invoke(nameof(ResetJump), 5f);
    }
    private void ResetJump()
    {
        JumpForce = defaultJump;
        Debug.Log("跳躍力恢復，目前跳躍力: " + JumpForce);
    }
    #endregion


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

    //�˴��b�a������� �������z����y
    private void CheckState()
    {
        coll.sharedMaterial = physicscheck.isGround ? ground : wall;
    }
}
