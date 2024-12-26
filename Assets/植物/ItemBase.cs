using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json.Linq;
using UnityEngine.UI;


public class ItemBase : MonoBehaviour
{
    private Transform EffectPanel;

    void Start(){
        GameObject panelObject = GameObject.Find("EffectPanel");
        EffectPanel = panelObject.transform;
    }

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
    
    public AudioClip boostSound;  // 增益音效
    public AudioClip debuffSound; // 減益音效
    private PlayerController playerController;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // 獲取玩家控制器
    }

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


        GameObject effectPrefab = Resources.Load<GameObject>("EffectImagePrefab");
        if (effectPrefab == null)
            {
                Debug.LogError("無法加載 EffectImagePrefab");
            }
        GameObject newEffect = Instantiate(effectPrefab, EffectPanel);
        Sprite EffectSprite;

        switch (itemType)
        {
            case ItemType.AttackBoost:
                player.IncreaseAttack(5);
                EffectSprite = LoadSprite("Assets/Effect/attack_boost.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 2f);
                // 销毁物品
                player.PlayBoostSound();  // 播放增益音效
                Destroy(gameObject);
                break;
            case ItemType.Attackreduce:
                player.DecreaseAttack(5);
                EffectSprite = LoadSprite("Assets/Effect/attack_down.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 2f);
                // 销毁物品
                player.PlayDebuffSound();  // 播放減益音效
                Destroy(gameObject);
                break;
            case ItemType.HealthRestore:
                player.RestoreHealth(50f);
                EffectSprite = LoadSprite("Assets/Effect/regeneration.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 2f);
                // 销毁物品
                player.PlayBoostSound();  // 播放增益音效
                Destroy(gameObject);
                break;
            //10趴血
            case ItemType.HealthRestore10percent:
                player.RestoreHealthtenpercent(1.1f);

                player.PlayBoostSound();  // 播放增益音效

                EffectSprite = LoadSprite("Assets/Effect/regeneration.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 2f);



                EffectSprite = LoadSprite("Assets/Effect/regeneration.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 2f);

                // 销毁物品
                Destroy(gameObject);
                break;
            //扣一半血
            case ItemType.Healthreducepercent:
               player.Healthreducepercent(0.5f);
              player.PlayDebuffSound();  // 播放減益音效
              EffectSprite = LoadSprite("Assets/Effect/bleeding.png");
              newEffect.GetComponent<Image>().sprite = EffectSprite;
              Destroy(newEffect, 2f);

                EffectSprite = LoadSprite("Assets/Effect/bleeding.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 2f);

                // 销毁物品
                Destroy(gameObject);
                break;
                //扣50血
            case ItemType.Healthreduce:
                player.Healthreduce(50f);

                player.PlayDebuffSound();  // 播放減益音效

                EffectSprite = LoadSprite("Assets/Effect/bleeding.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 2f);



                EffectSprite = LoadSprite("Assets/Effect/bleeding.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;

                // 销毁物品
                Destroy(gameObject);
                break;
            
            case ItemType.SpeedBoost:
                player.PlayBoostSound();  // 播放增益音效
                player.BoostSpeed(1.5f); // 速度每次提升50%3秒
                EffectSprite = LoadSprite("Assets/Effect/swiftness.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 3f);
                
                break;
            //減速
            case ItemType.SpeedReduce:
                player.PlayDebuffSound();  // 播放減益音效
                player.SpeedReduce(0.5f);  
                EffectSprite = LoadSprite("Assets/Effect/slowed.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 3f);
                
                break;
            case ItemType.JumpBoost:
                player.BoostJump(1.3f); // 每次提升30%
                EffectSprite = LoadSprite("Assets/Effect/jump_boost.png");
                newEffect.GetComponent<Image>().sprite = EffectSprite;
                Destroy(newEffect, 5f);

                
                player.PlayBoostSound();  // 播放增益音效
                

                
                break;
        }
    }

    private Sprite LoadSprite(string path)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(path));
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}