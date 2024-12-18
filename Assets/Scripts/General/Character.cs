using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("基本屬性")]
    public float MaxHP;

    public float CurrentHP;

    [Header("受傷無敵參數")]
    public float InVulnerableDuration;
    private float InVulnerableCounter;
    public bool InVulnerable;

    public UnityEvent<Character> OnHealthChange;

    public UnityEvent<Transform> OnTakeDamage;
    public UnityEvent OnDie;
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


    public void TakeDamage(Attack attacker)
    {
        //無敵狀態部受傷 直接回傳
        if (InVulnerable)
            return;

        //當前血量-敵人傷害>0 受到傷害並進入無敵幀
        if (CurrentHP - attacker.damage > 0)
        {
            CurrentHP -= attacker.damage;
            TriggerInvulneraber();

            OnTakeDamage?.Invoke(attacker.transform);
        }
        //當前血量-敵人傷害<=0 死亡
        else
        {
            CurrentHP = 0;
            OnDie?.Invoke();
        }

        OnHealthChange?.Invoke(this);
    }
    
    //判斷受傷無敵
    public void TriggerInvulneraber()
    {
        if (!InVulnerable)
        {
            InVulnerable = true;
            InVulnerableCounter = InVulnerableDuration;
        }
    } 



}

