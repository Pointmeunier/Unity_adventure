using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IllustratedBookSwitch : MonoBehaviour
{
    public GameObject CurrentBook;
    public GameObject OtherBook;
    public IllustratedBookButton BookButtonScript; // 引用 `IllustratedBookButton` 腳本

    public void PanelOpen()
    {
        if (CurrentBook != null && OtherBook != null && BookButtonScript != null)
        {
            // 切換圖鑑
            BookButtonScript.ClearCurrentBook(CurrentBook);
            CurrentBook.SetActive(false);
            BookButtonScript.BookOpen(OtherBook);
        }
    }

    public void PanelClose()
    {
    
        BookButtonScript.ClearCurrentBook(CurrentBook);
        CurrentBook.SetActive(false);

    }
}
