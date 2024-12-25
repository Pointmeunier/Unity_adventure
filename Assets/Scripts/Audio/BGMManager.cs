using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public GameObject bgmObject; // 需要控制的子物件
    private bool hasReactivated = false; // 用於追蹤是否已重新啟用子物件

    void Start()
    {
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
}
