using UnityEngine;

public class Forg : Enemy
{
    private BoxCollider2D boxCollider;
    private float offsetTimer = 0f;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        if (boxCollider == null)
        {
            Debug.LogError("BoxCollider2D not found on the Forg object.");
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
            float offsetX = Mathf.Lerp(0.35f, -0.4f, Mathf.PingPong(offsetTimer, 1f));
            boxCollider.offset = new Vector2(offsetX, boxCollider.offset.y);
        }
    }
}
