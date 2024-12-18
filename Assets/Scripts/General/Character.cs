using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("���ݩ�")]
    public float MaxHP;

    public float CurrentHP;

    [Header("���˵L�İѼ�")]
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
        //�L�Ī��A������ �����^��
        if (InVulnerable)
            return;

        //��e��q-�ĤH�ˮ`>0 ����ˮ`�öi�J�L�ĴV
        if (CurrentHP - attacker.damage > 0)
        {
            CurrentHP -= attacker.damage;
            TriggerInvulneraber();

            OnTakeDamage?.Invoke(attacker.transform);
        }
        //��e��q-�ĤH�ˮ`<=0 ���`
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

