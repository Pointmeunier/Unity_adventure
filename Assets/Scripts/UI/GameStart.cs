using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // 切換到遊戲的第一個場景
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene1"); // 替換 "GameScene" 為你的第一個場景名稱
        Debug.Log("click");
    }
}