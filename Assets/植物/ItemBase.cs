using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase : MonoBehaviour
{
    public enum ItemType
    {
        AttackBoost,  // 增加攻击力
        HealthRestore, // 补血
        SpeedBoost,     // 增加速度
        JumpBoost //增加跳躍力
    }

    public ItemType itemType; // 物品类型

    public void ApplyEffect(PlayerController player)
    {
        

        switch (itemType)
        {
            case ItemType.AttackBoost:
                player.IncreaseAttack(5);
                // 销毁物品
                Destroy(gameObject);
                break;
            case ItemType.HealthRestore:
                player.RestoreHealth(50f);
                // 销毁物品
                Destroy(gameObject);
                break;
            case ItemType.SpeedBoost:
                player.BoostSpeed(1.5f); // 速度每次提升50%
                // 销毁物品
                Destroy(gameObject);
                break;
            case ItemType.JumpBoost:
                player.BoostJump(1.3f); // 速度每次提升30%
                
                break;
        }

        
    }
}
