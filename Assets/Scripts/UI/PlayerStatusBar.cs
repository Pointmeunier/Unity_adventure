using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusBar : MonoBehaviour
{
    public Image healthImage;
    public Image healthDelayImgge;
    public Character character;
    public float delaySpeed = 0.5f; // �����q�^�_���t��

    private void Start()
    {
        // �۰ʴM��a��Player���Ҫ��C������A�è��oCharacter�}��
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            character = player.GetComponent<Character>();
            if (character == null)
            {
                Debug.LogError("Player����W�S�����Character�}���I");
            }
        }
        else
        {
            Debug.LogError("�䤣��a��Player���Ҫ��C������I");
        }
    }

    private void Update()
    {
        // �T�OCharacter�}���s�b�A�ç�s��q���
        if (character != null)
        {
            float healthPercentage = character.CurrentHP / character.MaxHP; // ���]Character�}����MaxHP�Ѽ�
            OnHealthChange(healthPercentage);
        }

        // ������޿�B�z
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
