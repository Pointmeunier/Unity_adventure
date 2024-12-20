using System.Collections;
using UnityEngine;

public class Lizard : Enemy
{
    public override void Move()
    {
        base.Move();
        ani.SetBool("Walk", true);
    }
}
