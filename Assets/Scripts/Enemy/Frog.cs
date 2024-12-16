using System.Collections;
using UnityEngine;

public class Forg : Enemy
{
    public override void Move()
    {
        base.Move();
        ani.SetBool("Walk", true);
    }
}
