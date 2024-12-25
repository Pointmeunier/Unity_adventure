using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{

    public string bossSceneName = "BossScene"; // Boss 關卡的場景名稱
    public GameObject bgmObject; // 需要控制的子物件
    private bool hasReactivated = false; // 用於追蹤是否已重新啟用子物件
    private AudioManager audioManager;


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogWarning("找不到 AudioManager，請確保場景中有正確的 AudioManager 物件！");
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (bgmObject != null)
        {
            bgmObject.SetActive(false); // 遊戲開始時禁用子物件
        }
        else
        {
            Debug.LogWarning("未分配 bgmObject，請在 Inspector 中設置！");
        }
    }

    void Update()
    {
        // 如果尚未重新啟用子物件，則執行
        if (!hasReactivated && bgmObject != null)
        {
            bgmObject.SetActive(true); // 在 Update 中重新啟用子物件
            hasReactivated = true; // 確保只執行一次
            Debug.Log("BGM 子物件已重新啟用");
        }
    }
    private void OnDestroy()
    {
        // 取消訂閱場景加載事件以防止記憶體洩漏
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == bossSceneName)
        {
            if (audioManager != null && audioManager.BGM != null)
            {
                audioManager.BGM.volume = 0; // 將 BGM 的音量設為 0
                Debug.Log("已進入 BossScene，普通 BGM 音量設為 0");
            }
        }
        else
        {
            if (audioManager != null && audioManager.BGM != null)
            {
                audioManager.BGM.volume = 0.205f; // 恢復普通場景的 BGM 音量
                Debug.Log("離開 BossScene，恢復普通 BGM 音量");
            }
        }
    }

}