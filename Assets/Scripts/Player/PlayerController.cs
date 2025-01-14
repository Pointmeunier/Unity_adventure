using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public PalyerInputControll inputControll;
    public Vector2 inputDirection;

    //植物音效
    public AudioClip boostSound;  // 增益音效
    public AudioClip debuffSound; // 減益音效
    private AudioSource audioSource; // 播放音效的音頻源
    
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

    private bool isMoving = false;
    private float moveDirection = 0f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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



    // 播放增益音效
    public void PlayBoostSound()
    {
        if (audioSource != null && boostSound != null)
        {
            audioSource.PlayOneShot(boostSound); // 播放一次增益音效
        }
    }

    // 播放減益音效
    public void PlayDebuffSound()
    {
        if (audioSource != null && debuffSound != null)
        {
            audioSource.PlayOneShot(debuffSound); // 播放一次減益音效
        }
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
        if (isMoving)
        {
            inputDirection.x = moveDirection;
        }
        else
        {
            inputDirection = inputControll.GamePlayer.Move.ReadValue<Vector2>(); // 停止移動
        }
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

    public void OnMoveLeft(bool isPressed)
    {
        isMoving = isPressed; // 當按下按鈕時設置為移動狀態
        moveDirection = isPressed ? -1 : 0; // 設置方向為左
    }

    public void OnMoveRight(bool isPressed)
    {
        isMoving = isPressed; // 當按下按鈕時設置為移動狀態
        moveDirection = isPressed ? 1 : 0; // 設置方向為右
    }


    public void onJump()
        {
            if (physicscheck.isGround)
            rb.AddForce(transform.up*JumpForce,ForceMode2D.Impulse);
        }

    public void Jump(InputAction.CallbackContext context)
    {
        if (physicscheck.isGround)
        rb.AddForce(transform.up*JumpForce,ForceMode2D.Impulse);
    }

    public void onPlayerAttack()
    {
        if (physicscheck.isGround)
        {
            playerAnimation.PlayAttack();
            isAttack = true;
        }
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
    public void DecreaseAttack(int amount)
    {
        for (int i = 0; i < attackComponents.Length; i++)
            {
                attackComponents[i].damage -= amount; // 修改每个 Attack 的伤害值
                Debug.Log($"目前第{i + 1}段攻擊力：{attackComponents[i].damage}");
            }
    }

    public void RestoreHealth(float amount)
    {
        character.RestoreHealth(amount); // use Character 
    }

    public void RestoreHealthtenpercent(float amount)
    {
        character.RestoreHealthtenpercent(amount); // use Character 
    }
    public void Healthreducepercent(float amount)
    {
        character.Healthreducepercent(amount); // use Character 
    }

    public void Healthreduce(float amount)
    {
        character.Healthreduce(amount); // use Character 
    }




    public void BoostSpeed(float multiplier)
    {
        Speed *= multiplier;
        Debug.Log("速度提升，目前速度: " + Speed);

        // 恢复速度（例如3秒后恢复）
        Invoke(nameof(ResetSpeed), 3f);
    }
    //減速3秒
    public void SpeedReduce(float multiplier)
    {
        Speed *= multiplier;
        Debug.Log("速度減半，目前速度: " + Speed);

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
