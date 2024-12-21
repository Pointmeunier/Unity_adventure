using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LizardDiff : Enemy
{
    private BoxCollider2D boxCollider;
    private float offsetTimer = 0f;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D not found on the LizardDiff object.");
        }
    }

    public override void Move()
    {
        if (Mathf.Abs(FaceDir.x) < 0.1f)
        {
            FaceDir.x = Mathf.Sign(FaceDir.x);
        }

        base.Move();
        ani.SetBool("Walk", true);

        UpdateBoxColliderOffset();
    }

    private void UpdateBoxColliderOffset()
    {
        if (boxCollider != null)
        {
            offsetTimer += Time.deltaTime;
            float offsetX = Mathf.Lerp(-3.9f, -3.95f, Mathf.PingPong(offsetTimer, 1f));
            boxCollider.offset = new Vector2(offsetX, boxCollider.offset.y);
        }
    }
}
