using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LizardDiff : Enemy
{
    public override void Move()
    {
        base.Move();
        ani.SetBool("Walk", true);
    }
}
