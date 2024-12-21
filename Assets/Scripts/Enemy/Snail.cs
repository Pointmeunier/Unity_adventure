using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    public override void Move()
    {
        // �b���ʤ��e�A���j���ˬd FaceDir.x �O�_���� 0
        if (Mathf.Abs(FaceDir.x) < 0.5f)
        {
            // �Y���� 0�A�j��]�w�� 1 �� -1�A�o�˫O�����ʤ�V
            FaceDir.x = Mathf.Sign(FaceDir.x);
        }

        // �եΰ�¦ Enemy ���O�� Move ��k
        base.Move();

        // ����樫�ʵe
        ani.SetBool("Walk", true);
    }
}