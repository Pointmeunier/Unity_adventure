using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{

    public string bossSceneName = "BossScene"; // Boss ���d�������W��
    public GameObject bgmObject; // �ݭn����l����
    private bool hasReactivated = false; // �Ω�l�ܬO�_�w���s�ҥΤl����
    private AudioManager audioManager;


    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();

        if (audioManager == null)
        {
            Debug.LogWarning("�䤣�� AudioManager�A�нT�O�����������T�� AudioManager ����I");
        }

        SceneManager.sceneLoaded += OnSceneLoaded;

        if (bgmObject != null)
        {
            bgmObject.SetActive(false); // �C���}�l�ɸT�Τl����
        }
        else
        {
            Debug.LogWarning("�����t bgmObject�A�Цb Inspector ���]�m�I");
        }
    }

    void Update()
    {
        // �p�G�|�����s�ҥΤl����A�h����
        if (!hasReactivated && bgmObject != null)
        {
            bgmObject.SetActive(true); // �b Update �����s�ҥΤl����
            hasReactivated = true; // �T�O�u����@��
            Debug.Log("BGM �l����w���s�ҥ�");
        }
    }
    private void OnDestroy()
    {
        // �����q�\�����[���ƥ�H����O���鬪�|
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == bossSceneName)
        {
            if (audioManager != null && audioManager.BGM != null)
            {
                audioManager.BGM.volume = 0; // �N BGM �����q�]�� 0
                Debug.Log("�w�i�J BossScene�A���q BGM ���q�]�� 0");
            }
        }
        else
        {
            if (audioManager != null && audioManager.BGM != null)
            {
                audioManager.BGM.volume = 0.205f; // ��_���q������ BGM ���q
                Debug.Log("���} BossScene�A��_���q BGM ���q");
            }
        }
    }

}