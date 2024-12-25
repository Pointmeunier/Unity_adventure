using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBGMController : MonoBehaviour
{
    private AudioSource bossBGM; // ��e���󪺭���

    void Start()
    {
        // �����e���󪺭���
        bossBGM = GetComponent<AudioSource>();

        if (bossBGM == null)
        {
            Debug.LogWarning("BossBGM �� AudioSource ���]�m�I");
            return;
        }

        // ���� Boss ����
        bossBGM.Play();
        Debug.Log("Boss ���ֶ}�l����");
    }
}