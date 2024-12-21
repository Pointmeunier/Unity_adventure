using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransEffect : MonoBehaviour
{
    public Image fadeImage; // 黑色淡入淡出的 UI 圖像
    public float fadeDuration = 3f; // 淡入淡出的時間
    private bool isFading = false; // 是否正在淡入淡出

    private void Start()
    {
        // 檢查 fadeImage 是否設置，並開始場景時淡出（逐漸顯示場景）
        if (fadeImage != null)
        {
            StartCoroutine(FadeOut());
        }

        // 註冊場景加載事件，確保每次場景加載時淡入淡出效果都被執行
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 在新場景加載後開始淡入效果
        if (!isFading && fadeImage != null)
        {
            StartCoroutine(FadeOut());
        }
    }

    public void SwitchScene(string sceneName)
    {
        if (!isFading)
        {
            StartCoroutine(FadeAndSwitchScene(sceneName));
        }
    }

    private IEnumerator FadeAndSwitchScene(string sceneName)
    {
        // 淡入（逐漸變黑）
        yield return StartCoroutine(FadeIn());

        // 加載新場景
        SceneManager.LoadScene(sceneName);

        // 等待場景加載完畢後，再執行淡出
        yield return new WaitForSeconds(fadeDuration);

        // 淡出（逐漸顯示新場景）
        yield return StartCoroutine(FadeOut());
    }

    private IEnumerator FadeIn()
    {
        isFading = true;
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration); // 漸變透明度到 1
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f; // 完全變黑
        fadeImage.color = color;
    }

    private IEnumerator FadeOut()
    {
        isFading = true;
        float timer = 0f;
        Color color = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration); // 漸變透明度到 0
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f; // 完全透明
        fadeImage.color = color;
        isFading = false;
    }

    private void OnDestroy()
    {
        // 解除註冊場景加載事件
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
