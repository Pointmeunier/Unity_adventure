using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImgge;
    public Character character;
    public float delaySpeed = 0.5f; // 控制血量回復的速度

    private void Start()
    {
        // 自動尋找帶有Player標籤的遊戲物件，並取得Character腳本
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            character = player.GetComponent<Character>();
            if (character == null)
            {
                Debug.LogError("Player物件上沒有找到Character腳本！");
            }
        }
        else
        {
            Debug.LogError("找不到帶有Player標籤的遊戲物件！");
        }
    }

    private void Update()
    {
        // 確保Character腳本存在，並更新血量顯示
        if (character != null)
        {
            float healthPercentage = character.CurrentHP / character.MaxHP; // 假設Character腳本有MaxHP參數
            OnHealthChange(healthPercentage);
        }

        // 延遲條邏輯處理
        if (healthDelayImgge.fillAmount > healthImage.fillAmount)
        {
            healthDelayImgge.fillAmount -= Time.deltaTime * delaySpeed;
            if (healthDelayImgge.fillAmount < healthImage.fillAmount)
            {
                healthDelayImgge.fillAmount = healthImage.fillAmount;
            }
        }
        else if (healthDelayImgge.fillAmount < healthImage.fillAmount)
        {
            healthDelayImgge.fillAmount += Time.deltaTime * delaySpeed;
            if (healthDelayImgge.fillAmount > healthImage.fillAmount)
            {
                healthDelayImgge.fillAmount = healthImage.fillAmount;
            }
        }
    }

    public void OnHealthChange(float percentage)
    {
        healthImage.fillAmount = percentage;
    }
}
