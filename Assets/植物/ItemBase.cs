using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public enum ItemType
    {
        AttackBoost,   // 增加攻擊力
        HealthRestore, // 補血
        SpeedBoost,    // 增加速度
        JumpBoost      // 增加跳躍力
    }

    public ItemType itemType; // 物品類型
    public AudioClip buffSound; // 增益音效
    public AudioClip debuffSound; // 減益音效
    private AudioSource audioSource; // 播放增益或減益音效的音頻源
    private static AudioSource bgmAudioSource; // 播放背景音樂的靜態音頻源

    void Awake()
    {
        audioSource = GetComponent<AudioSource>(); // 獲取物品的音頻源
        if (bgmAudioSource == null)
        {
            // 從 BGM_Manager 物件中獲取音頻源
            GameObject bgmManager = GameObject.Find("BGM_Manager"); // 查找 BGM_Manager 物件
            if (bgmManager != null)
            {
                bgmAudioSource = bgmManager.GetComponent<AudioSource>(); // 獲取 BGM_Manager 上的 AudioSource
            }
            else
            {
                Debug.LogWarning("BGM_Manager 未找到！"); // 若未找到，輸出警告
            }
        }
    }

    public void ApplyEffect(PlayerController player)
    {
        // 根據物品類型應用效果
        switch (itemType)
        {
            case ItemType.AttackBoost:
                player.IncreaseAttack(5); // 增加攻擊力
                PlaySound(buffSound);     // 播放增益音效
                DestroyItem();            // 應用效果後銷毀物品
                break;

            case ItemType.HealthRestore:
                player.RestoreHealth(50f); // 恢復生命
                PlaySound(buffSound);      // 播放增益音效
                DestroyItem();             // 應用效果後銷毀物品
                break;

            case ItemType.SpeedBoost:
                player.BoostSpeed(1.5f);   // 提升速度
                PlaySound(buffSound);      // 播放增益音效
                DestroyItem();             // 應用效果後銷毀物品
                break;

            case ItemType.JumpBoost:
                player.BoostJump(1.3f);    // 提升跳躍力
                PlaySound(buffSound);      // 播放增益音效
                break;
        }
    }

    // 播放音效
    private void PlaySound(AudioClip sound)
    {
        if (audioSource != null && sound != null)
        {
            audioSource.PlayOneShot(sound); // 播放一次音效
        }
    }

    // 銷毀物品（如果需要）
    private void DestroyItem()
    {
        Destroy(gameObject); // 銷毀物品
    }

    // 控制背景音樂
    public static void SetBGM(AudioClip bgm)
    {
        if (bgmAudioSource != null)
        {
            bgmAudioSource.clip = bgm; // 設定背景音樂
            bgmAudioSource.loop = true; // 確保背景音樂循環播放
            bgmAudioSource.Play(); // 播放背景音樂
        }
    }
}
