using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // 設定第一個遊戲場景名稱
    [SerializeField] private string firstSceneName = "GameScene1";

    // 此方法需要綁定到按鈕的 OnClick 事件
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(firstSceneName)) { 
        
            SceneManager.LoadScene(firstSceneName);
        }
        else
        {
            Debug.LogWarning("未設置場景名稱，請在檢視面板中設置第一個場景的名稱！");
        }
    }
}