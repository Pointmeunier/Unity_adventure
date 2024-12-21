using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement; // �ޤJ�����޲z�R�W�Ŷ�

public class EnemyLeft : MonoBehaviour
{
    public TMP_Text enemyCountText; // TextMeshPro ��r UI ����
    private string[] sceneNames = { "GameScene2", "GameScene3", "GameScene4" }; // �w�]�������C��
    private int currentSceneIndex; // ��e��������
    private GameObject player; // ���a����

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
        // �j�M�Ҧ����Ҭ� "Enemy" ������
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        // ��s��r���e��ܳѾl�ĤH�ƶq
        enemyCountText.text = ":" + enemies.Length;

        // �p�G�ĤH�ƶq�� 0�A������ؼг���
        if (enemies.Length == 0)
        {
            SwitchScene();
        }
    }

    // �ھڷ�e�����ӽT�w��e����������
    private int GetCurrentSceneIndex()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        // �T�{��e�����O�_�b�����C��
        for (int i = 0; i < sceneNames.Length; i++)
        {
            if (currentSceneName == sceneNames[i])
            {
                return i;
            }
        }

        // �Y��e�������b�C���A�h��^ -1
        return -1;
    }

    // �]�m���a��m�� StartPoint
    private IEnumerator SetPlayerToStartPoint()
    {
        yield return null; // ���ݤ@�V�T�O�Ҧ���������w��l��

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


    // �����������޿�
    private void SwitchScene()
    {
        // �T�{�O�_���U�@�ӳ���
        if (currentSceneIndex + 1 < sceneNames.Length)
        {
            string nextScene = sceneNames[currentSceneIndex + 1];

            // �ˬd�ؼг����O�_�s�b
            if (IsSceneExists(nextScene))
            {
                SceneManager.LoadScene(nextScene);
                currentSceneIndex++; // ��s��e��������
            }
            else
            {
                // ��ܿ��~�T���A�������s�b
                Debug.LogError("Scene " + nextScene + " does not exist.");
            }
        }
        else
        {
            // �p�G�w�g�O�̫�@�ӳ����A��ܵ����T��
            Debug.Log("All scenes completed.");
        }
    }

    // �ˬd�����O�_�s�b
    private bool IsSceneExists(string sceneName)
    {
        int sceneIndex = SceneUtility.GetBuildIndexByScenePath("Assets/Scenes/" + sceneName + ".unity");
        return sceneIndex != -1;
    }
}
