using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    // �]�w�Ĥ@�ӹC�������W��
    [SerializeField] private string firstSceneName = "GameScene1";

    // ����k�ݭn�j�w����s�� OnClick �ƥ�
    public void StartGame()
    {
        if (!string.IsNullOrEmpty(firstSceneName)) { 
        
            SceneManager.LoadScene(firstSceneName);
        }
        else
        {
            Debug.LogWarning("���]�m�����W�١A�Цb�˵����O���]�m�Ĥ@�ӳ������W�١I");
        }
    }
}