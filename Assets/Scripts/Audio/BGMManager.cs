using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    public GameObject bgmObject; // �ݭn����l����
    private bool hasReactivated = false; // �Ω�l�ܬO�_�w���s�ҥΤl����

    void Start()
    {
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
}
