using UnityEngine;

public class PersistentObjects : MonoBehaviour
{
    public GameObject player; // ���a����
    public GameObject canvas; // �e��

    private void Start()
    {
        // �]�m���a�M�e�����H���������ӾP��
        DontDestroyOnLoad(player);
        DontDestroyOnLoad(canvas);
    }
}