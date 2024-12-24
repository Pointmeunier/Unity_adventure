using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    [Header("玩家")]
    public GameObject player;
    [Header("UI")]
    public GameObject canvas;
    [Header("不摧毀物件的管理員")]
    public GameObject DontDestroyObj;
    [Header("轉場")]
    public GameObject ScemeLoadEffect;
    [Header("音效相關")]
    public GameObject AudioManager;
    public GameObject BGM;


    private void Start()
    {
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(canvas);
        DontDestroyOnLoad(DontDestroyObj);
        DontDestroyOnLoad(ScemeLoadEffect);
        DontDestroyOnLoad(AudioManager);
        DontDestroyOnLoad(BGM);

    }
}