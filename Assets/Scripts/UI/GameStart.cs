using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    // ������C�����Ĥ@�ӳ���
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene1"); // ���� "GameScene" ���A���Ĥ@�ӳ����W��
        Debug.Log("click");
    }
}