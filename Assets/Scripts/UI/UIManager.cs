using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public PlayerStatusBar playerStatusBar;
    [Header("監聽血量減少")]
    public CharecterEventSo HealthEvent;

    //訂閱事件
    private void OnEnable()
    {
        HealthEvent.OnEventRaised += OnHealthEvent;
    }

  

    //取消訂閱
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
