using System.Collections;
using UnityEngine;

public class Lizard : Enemy
{
    private BoxCollider2D boxCollider; // �ΨӦs�� BoxCollider2D
    private float offsetTimer = 0f; // �Ω�p��ɶ�

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

        // ��s BoxCollider2D �� offset.x
        UpdateBoxColliderOffset();
    }

    private void Start()
    {
        // ��l�� BoxCollider2D
        boxCollider = GetComponent<BoxCollider2D>();

        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D not found on the Lizard object.");
        }
    }

    private void UpdateBoxColliderOffset()
    {
        if (boxCollider != null)
        {
            // �ϥ� Mathf.PingPong �� offset.x �b -4.8 �M -4.9 �����Ӧ^�B��
            offsetTimer += Time.deltaTime;
            float offsetX = Mathf.Lerp(-4.9f, -4.8f, Mathf.PingPong(offsetTimer, 1f));
            boxCollider.offset = new Vector2(offsetX, boxCollider.offset.y);
        }
    }
}
