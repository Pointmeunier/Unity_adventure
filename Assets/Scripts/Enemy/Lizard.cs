using System.Collections;
using UnityEngine;

public class Lizard : Enemy
{
    Character character;

    private void Start()
    {
        // ��� Character ����
        character = GetComponent<Character>();
    }


    public override void Move()
    {
        base.Move();
        ani.SetBool("Walk", true);
    }
}
