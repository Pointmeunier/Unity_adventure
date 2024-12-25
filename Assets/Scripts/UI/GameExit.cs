using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameExit : MonoBehaviour
{
    // 當玩家按下按鈕時觸發
    public void ExitGame()
    {
        Debug.Log("遊戲已結束"); // 在編輯器中顯示提示
        Application.Quit(); // 結束遊戲（僅在已構建的應用中有效）
    }
}
