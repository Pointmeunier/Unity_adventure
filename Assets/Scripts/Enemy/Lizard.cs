using System.Collections;
using UnityEngine;

public class Lizard : Enemy
{
    private BoxCollider2D boxCollider; // 用來存取 BoxCollider2D
    private float offsetTimer = 0f; // 用於計算時間

    public override void Move()
    {
        // 在移動之前，先強制檢查 FaceDir.x 是否接近 0
        if (Mathf.Abs(FaceDir.x) < 0.1f)
        {
            // 若接近 0，強制設定為 1 或 -1，這樣保持移動方向
            FaceDir.x = Mathf.Sign(FaceDir.x);
        }

        // 調用基礎 Enemy 類別的 Move 方法
        base.Move();

        // 播放行走動畫
        ani.SetBool("Walk", true);

        // 更新 BoxCollider2D 的 offset.x
        UpdateBoxColliderOffset();
    }

    private void Start()
    {
        // 初始化 BoxCollider2D
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
            // 使用 Mathf.PingPong 讓 offset.x 在 -4.8 和 -4.9 之間來回浮動
            offsetTimer += Time.deltaTime;
            float offsetX = Mathf.Lerp(-4.9f, -4.8f, Mathf.PingPong(offsetTimer, 1f));
            boxCollider.offset = new Vector2(offsetX, boxCollider.offset.y);
        }
    }
}
