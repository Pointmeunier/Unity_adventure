using System.Collections;
using UnityEngine;

public class LizardDiff : Enemy
{
    public override void Move()
    {
        base.Move();
        ani.SetBool("Walk", true);
    }
}
