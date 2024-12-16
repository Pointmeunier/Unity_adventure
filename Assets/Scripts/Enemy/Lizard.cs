using System.Collections;
using UnityEngine;

public class Lizard : Enemy
{
    private bool isWalking = true;

    private void Start()
    {
        // 確保遊戲開始時處於行走狀態
        isWalking = true;
        ani.SetBool("Walk", true);

        // 啟動狀態切換協程
        StartCoroutine(SwitchWalkState());
    }

    public override void Move()
    {
        if (isWalking)
        {
            base.Move();
            ani.SetBool("Walk", true);
        }
        else
        {
            ani.SetBool("Walk", false);
            rb.velocity = Vector2.zero; // 停止移動
        }
    }

    private IEnumerator SwitchWalkState()
    {
        while (true) // 持續切換狀態
        {
            // 行走一段隨機時間後進入閒置狀態
            yield return new WaitForSeconds(Random.Range(5f, 8f));
            isWalking = false;
            ani.SetBool("Walk", false);

            // 閒置一段隨機時間後恢復行走狀態
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            isWalking = true;
            ani.SetBool("Walk", true);
        }
    }
}
