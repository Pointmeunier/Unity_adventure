using UnityEngine;

public class Forg : Enemy
{
    public override void Move()
    {
        // �b���ʤ��e�A���j���ˬd FaceDir.x �O�_���� 0
        if (Mathf.Abs(FaceDir.x) < 0.1f)
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
