using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Newtonsoft.Json;
using TMPro;

public class IllustratedBookButton : MonoBehaviour
{
    public GameObject PlantBook; // 植物圖鑑
    public GameObject EnemyBook; // 怪物圖鑑
    private Transform PlantPanel; // 植物按鈕的父物件 (Panel)
    private Transform EnemyPanel; // 怪物按鈕的父物件 (Panel)
    private Image PlantImage; // 圖鑑的圖片顯示
    private TMP_Text PlantName; // 圖鑑名稱顯示
    private TMP_Text PlantDetail; // 圖鑑細節顯示
    private Image EnemyImage; // 圖鑑的圖片顯示
    private TMP_Text EnemyName; // 圖鑑名稱顯示
    private TMP_Text EnemyDetail; // 圖鑑細節顯示
    public GameObject ButtonPrefab; // 按鈕預製件
    public TMP_FontAsset font;
    private Dictionary<string, object> jsonData;

    void Start()
    {

        string originalFilePath = "./Assets/Resources/Data.json";
        string backupFilePath = "./Assets/Resources/Data_backup.json";

        // 檢查備份檔案是否存在，若不存在則創建
        if (!File.Exists(backupFilePath))
        {
            File.Copy(originalFilePath, backupFilePath);
        }

        // 每次遊戲重啟，使用備份檔案覆蓋原始檔案
        File.Copy(backupFilePath, originalFilePath, true);

        // 動態尋找子物件
        if (PlantBook != null)
        {
            PlantPanel = PlantBook.transform.Find("PlantPanel");
            PlantImage = PlantBook.transform.Find("PlantImage").GetComponent<Image>();
            PlantName = PlantBook.transform.Find("PlantName").GetComponent<TMP_Text>();
            PlantDetail = PlantBook.transform.Find("PlantDetail").GetComponent<TMP_Text>();
        }

        if (EnemyBook != null)
        {
            EnemyPanel = EnemyBook.transform.Find("EnemyPanel");
            EnemyImage = EnemyBook.transform.Find("EnemyImage").GetComponent<Image>();
            EnemyName = EnemyBook.transform.Find("EnemyName").GetComponent<TMP_Text>();
            EnemyDetail = EnemyBook.transform.Find("EnemyDetail").GetComponent<TMP_Text>();
        }

        PlantBook.SetActive(false);
        EnemyBook.SetActive(false);
    }

    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame){
            ClearCurrentBook(PlantBook);
            ClearCurrentBook(EnemyBook);
            PlantBook.SetActive(false);
            EnemyBook.SetActive(false);
        }
    }

    public void BookInitOpen()
    {
        BookOpen(PlantBook);
    }

    public void BookOpen(GameObject targetBook)
    {
        Color color = PlantImage.color;
        color.a = 0f;
        PlantImage.color = color;
        EnemyImage.color = color;
        // 開啟目標圖鑑並加載內容
        if (targetBook == PlantBook)
        {
            PlantBook.SetActive(true);
            PopulatePlantBook();
        }
        else if (targetBook == EnemyBook)
        {
            EnemyBook.SetActive(true);
            PopulateEnemyBook();
        }
    }

    public void ClearCurrentBook(GameObject targetBook)
    {
        if (targetBook == PlantBook)
        {
            foreach (Transform child in PlantPanel)
            {
                Destroy(child.gameObject);
            }
        }

        if (targetBook == EnemyBook)
        {
            foreach (Transform child in EnemyPanel)
            {
                Destroy(child.gameObject);
            }
        }
    }

    private void LoadJsonData()
    {
        string path = Path.Combine(Application.dataPath, "./Resources/Data.json");
        if (File.Exists(path))
        {
            string jsonString = File.ReadAllText(path);
            jsonData = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);
        }
    
    }

    private void PopulatePlantBook()
    {
        LoadJsonData();

        if (jsonData != null && jsonData.ContainsKey("Plant"))
        {
            
            Dictionary<string, Dictionary<string, object>> plants = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData["Plant"].ToString());

            foreach (var plant in plants)
            {
                string plantName = plant.Key;
                var plantData = plant.Value;

                if (plantData.ContainsKey("pick") && (bool)plantData["pick"])
                {
                    GameObject newButton = Instantiate(ButtonPrefab, PlantPanel);
                    TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
                    buttonText.font = font;
                    buttonText.text = plantName;
                    buttonText.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -30);
                    string imagePath = plantData["image"].ToString();
                    Sprite plantSprite = LoadSprite(imagePath);
                    newButton.GetComponent<Image>().sprite = plantSprite;

                    newButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        PlantImage.sprite = plantSprite;
                        Color color = PlantImage.color;
                        color.a = 255f;
                        PlantImage.color = color;
                        PlantName.text = plantName;
                        PlantDetail.text = plantData["detail"].ToString();
                    });
                }
            }
        }
    }

    private void PopulateEnemyBook()
    {
        LoadJsonData();

        if (jsonData != null && jsonData.ContainsKey("Enemy"))
        {
            Dictionary<string, Dictionary<string, object>> enemies = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, object>>>(jsonData["Enemy"].ToString());

            foreach (var enemy in enemies)
            {
                string enemyName = enemy.Key;
                var enemyData = enemy.Value;

                if (enemyData.ContainsKey("alive") && !(bool)enemyData["alive"])
                {
                    GameObject newButton = Instantiate(ButtonPrefab, EnemyPanel);
                    TMP_Text buttonText = newButton.GetComponentInChildren<TMP_Text>();
                    buttonText.font = font;

                    buttonText.text = enemyName;
                    buttonText.GetComponent<RectTransform>().anchoredPosition += new Vector2(0, -30);
                    string imagePath = enemyData["image"].ToString();
                    Sprite enemySprite = LoadSprite(imagePath);
                    newButton.GetComponent<Image>().sprite = enemySprite;

                    newButton.GetComponent<Button>().onClick.AddListener(() =>
                    {
                        EnemyImage.sprite = enemySprite;
                        Color color = EnemyImage.color;
                        color.a = 255f;
                        EnemyImage.color = color;
                        EnemyName.text = enemyName;
                        EnemyDetail.text = enemyData["detail"].ToString();
                    });
                }
            }
        }
    }

    private Sprite LoadSprite(string path)
    {
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(File.ReadAllBytes(path));
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
    }
}
