using UnityEngine;

public class Forg : Enemy
{
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
    }
}
