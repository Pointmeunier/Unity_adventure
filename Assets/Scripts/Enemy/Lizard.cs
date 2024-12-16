using System.Collections;
using UnityEngine;

public class Lizard : Enemy
{
    private bool isWalking = true;

    private void Start()
    {
        // �T�O�C���}�l�ɳB��樫���A
        isWalking = true;
        ani.SetBool("Walk", true);

        // �Ұʪ��A������{
        StartCoroutine(SwitchWalkState());
    }

    public override void Move()
    {
        if (isWalking)
        {
            base.Move();
            ani.SetBool("Walk", true);
        }
        else
        {
            ani.SetBool("Walk", false);
            rb.velocity = Vector2.zero; // �����
        }
    }

    private IEnumerator SwitchWalkState()
    {
        while (true) // ����������A
        {
            // �樫�@�q�H���ɶ���i�J���m���A
            yield return new WaitForSeconds(Random.Range(5f, 8f));
            isWalking = false;
            ani.SetBool("Walk", false);

            // ���m�@�q�H���ɶ����_�樫���A
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            isWalking = true;
            ani.SetBool("Walk", true);
        }
    }
}
