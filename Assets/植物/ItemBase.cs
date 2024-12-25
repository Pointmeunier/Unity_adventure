using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;

public class ItemBase : MonoBehaviour
{
    public enum ItemType
    {
        AttackBoost,  // 增加攻击力
        
        Attackreduce,  //減少攻擊力
        HealthRestore, // 補血

        Healthreduce, //減少血量50

        Healthreducepercent, //減少一半
        HealthRestore10percent,     // 增加10趴血

        SpeedReduce, //減速

        SpeedBoost, //速度提升50%
        JumpBoost //增加跳躍力
    }

    public ItemType itemType; // 物品类型

    public void ApplyEffect(PlayerController player)
    {

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            string spriteName = spriteRenderer.sprite.name;
            Debug.Log($"物品的 Sprite 名稱是: {spriteName}");
            string filePath = "./Assets/Resources/Data.json";
            string jsonData = File.ReadAllText(filePath);
            JObject jsonObject = JObject.Parse(jsonData);

            // 嘗試找到匹配的怪物
            if (jsonObject["Plant"]?[spriteName] != null)
            {
                jsonObject["Plant"][spriteName]["pick"] = true;
            }
            File.WriteAllText(filePath, jsonObject.ToString());
        }

        switch (itemType)
        {
            case ItemType.AttackBoost:
                player.IncreaseAttack(5);
                // 销毁物品
                Destroy(gameObject);
                break;
            case ItemType.Attackreduce:
                player.DecreaseAttack(5);
                // 销毁物品
                Destroy(gameObject);
                break;
            case ItemType.HealthRestore:
                player.RestoreHealth(50f);
                // 销毁物品
                Destroy(gameObject);
                break;
            //10趴血
            case ItemType.HealthRestore10percent:
                player.RestoreHealthtenpercent(1.1f);
                // 销毁物品
                Destroy(gameObject);
                break;
            //扣一半血
            case ItemType.Healthreducepercent:
                player.Healthreducepercent(0.5f);
                // 销毁物品
                Destroy(gameObject);
                break;
                //扣50血
            case ItemType.Healthreduce:
                player.Healthreduce(50f);
                // 销毁物品
                Destroy(gameObject);
                break;
            
            case ItemType.SpeedBoost:
                player.BoostSpeed(1.5f); // 速度每次提升50%3秒
                
                break;
            //減速
            case ItemType.SpeedReduce:
                player.SpeedReduce(0.5f);  
                
                break;
            case ItemType.JumpBoost:
                player.BoostJump(1.3f); // 每次提升30%
                
                break;
        }
    }
}