using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    public GameObject player; // 玩家物件
    public GameObject canvas; // 畫布
    public GameObject DontDestroyObj;

    private void Start()
    {
        // 設置玩家和畫布不隨場景切換而銷毀
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(DontDestroyObj);

    }
}