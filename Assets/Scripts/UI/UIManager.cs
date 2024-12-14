using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatusBar playerStatusBar;
    [Header("��ť��q���")]
    public CharecterEventSo HealthEvent;

    //�q�\�ƥ�
    private void OnEnable()
    {
        HealthEvent.OnEventRaised += OnHealthEvent;
    }

  

    //�����q�\
    private void OnDisable()
    {
        HealthEvent.OnEventRaised -= OnHealthEvent;
    }

    private void OnHealthEvent(Character character)
    {
        var percentage = character.CurrentHP / character.MaxHP;
        playerStatusBar.OnHealthChange(percentage);
    }
}
