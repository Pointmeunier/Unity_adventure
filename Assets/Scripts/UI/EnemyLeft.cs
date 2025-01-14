using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // 引入場景管理命名空間

public class EnemyLeft : MonoBehaviour
{
    public TMP_Text enemyCountText; // TextMeshPro 文字 UI 元件
    private string[] sceneNames = { "GameScene2", "GameScene3","BossScene","ClearScene"}; // 預設的場景列表
    private int currentSceneIndex; // 當前場景索引
    private GameObject player; // 玩家物件

    private void Start()
    {
        currentSceneIndex = GetCurrentSceneIndex();
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Player with tag 'Player' not found.");
        }
        else
        {
            StartCoroutine(SetPlayerToStartPoint());
        }
    }

    private void Update()
    {
        // 搜尋所有標籤為 "Enemy" 的物件
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // 更新文字內容顯示剩餘敵人數量
        enemyCountText.text = ":" + enemies.Length;

        // 如果敵人數量為 0，切換到目標場景
        if (enemies.Length == 0)
        {
            SwitchScene();
        }
    }

    // 根據當前場景來確定當前場景的索引
    private int GetCurrentSceneIndex()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 確認當前場景是否在場景列表中
        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (currentSceneName == sceneNames[i])
            {
                return i;
            }
        }

        // 若當前場景不在列表中，則返回 -1
        return -1;
    }

    // 設置玩家位置到 StartPoint
    private IEnumerator SetPlayerToStartPoint()
    {
        yield return null; // 等待一幀確保所有場景物件已初始化

        GameObject startPoint = GameObject.FindGameObjectWithTag("StartPoint");
        if (startPoint == null)
        {
            Debug.LogError("StartPoint with tag 'StartPoint' not found.");
        }
        else
        {
            player.transform.position = startPoint.transform.position;
        }
    }


    // 切換場景的邏輯
    private void SwitchScene()
    {
        // 確認是否有下一個場景
        if (currentSceneIndex + 1 < sceneNames.Length)
        {
            string nextScene = sceneNames[currentSceneIndex + 1];

            // 檢查目標場景是否存在
            if (IsSceneExists(nextScene))
            {
                SceneManager.LoadScene(nextScene);
                currentSceneIndex++; // 更新當前場景索引
            }
            else
            {
                // 顯示錯誤訊息，場景不存在
                Debug.LogError("Scene " + nextScene + " does not exist.");
            }
        }
        else
        {
            // 如果已經是最後一個場景，顯示結束訊息
            Debug.Log("All scenes completed.");
        }
    }

    // 檢查場景是否存在
    private bool IsSceneExists(string sceneName)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + sceneName + ".unity");
        return sceneIndex != -1;
    }
}
