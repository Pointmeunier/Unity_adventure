using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBGMController : MonoBehaviour
{
    private AudioSource bossBGM; // 當前物件的音源

    void Start()
    {
        // 獲取當前物件的音源
        bossBGM = GetComponent<AudioSource>();

        if (bossBGM == null)
        {
            Debug.LogWarning("BossBGM 的 AudioSource 未設置！");
            return;
        }

        // 撥放 Boss 音樂
        bossBGM.Play();
        Debug.Log("Boss 音樂開始播放");
    }
}