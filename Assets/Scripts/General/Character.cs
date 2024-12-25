using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("血量")]
    public float MaxHP;

    public float CurrentHP;

    [Header("受傷無敵參數")]
    public float InVulnerableDuration;
    private float InVulnerableCounter;
    public bool InVulnerable;

    public UnityEvent<Character> OnHealthChange;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;

    public UnityEvent<Character> OnHeal;
    public void Start()
    {
        CurrentHP = MaxHP;

        OnHealthChange?.Invoke(this);
    }
    public void Update()
    {
        InVulnerableCounter -= Time.deltaTime;
        if (InVulnerableCounter <= 0)
        {
            InVulnerable = false;
        }
    }
    public void RestoreHealth(float amount)
    {
        // 增加当前血量
        CurrentHP += amount;

        // 确保当前血量不超过最大血量
        if (CurrentHP > MaxHP)
        {
            CurrentHP = MaxHP;
        }

        Debug.Log("補血:"+amount);
        Debug.Log($"目前血量：{CurrentHP}/{MaxHP}");
    }

    public void RestoreHealthtenpercent(float amount)
    {

        // 增加10趴最高血量 
        //如果ui有問題的話 就要先放參數 先max在current 最後條
        float upw= MaxHP*(amount-1.0f);
        Debug.Log(upw);
        MaxHP *= amount;
        CurrentHP+=upw;

        

        Debug.Log($"目前血量：{CurrentHP}/{MaxHP}");
    }

    //扣50
    public void Healthreduce(float amount)
    {
        // 增加当前血量
        CurrentHP -= amount;

        // 确保当前血量不超过最大血量
        if (CurrentHP < 0)
        {
            CurrentHP = 0;
        }

        Debug.Log($"目前血量：{CurrentHP}/{MaxHP}");
    }
    
    public void Healthreducepercent(float amount)
    {
        // 增加当前血量
        CurrentHP = CurrentHP* amount;

        Debug.Log($"目前血量：{CurrentHP}/{MaxHP}");
    }
    

    

    public void TakeDamage(Attack attacker)
    {
        //�L�Ī��A������ �����^��
        if (InVulnerable)
            return;

        //���e��q-�ĤH�ˮ`>0 ����ˮ`�öi�J�L�ĴV
        if (CurrentHP - attacker.damage > 0)
        {
            CurrentHP -= attacker.damage;
            TriggerInvulneraber();

            OnTakeDamage?.Invoke(attacker.transform);
        }
        //���e��q-�ĤH�ˮ`<=0 ���`
        else
        {
            CurrentHP = 0;
            OnDie?.Invoke();
        }

        OnHealthChange?.Invoke(this);
    }
    
    //�P�_���˵L��
    public void TriggerInvulneraber()
    {
        if (!InVulnerable)
        {
            InVulnerable = true;
            InVulnerableCounter = InVulnerableDuration;
        }
    } 



}

