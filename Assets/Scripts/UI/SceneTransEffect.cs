using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransEffect : MonoBehaviour
{
    public Image fadeImage; // �¦�H�J�H�X�� UI �Ϲ�
    public float fadeDuration = 3f; // �H�J�H�X���ɶ�
    private bool isFading = false; // �O�_���b�H�J�H�X

    private void Start()
    {
        // �ˬd fadeImage �O�_�]�m�A�ö}�l�����ɲH�X�]�v����ܳ����^
        if (fadeImage != null)
        {
            StartCoroutine(FadeOut());
        }

        // ���U�����[���ƥ�A�T�O�C�������[���ɲH�J�H�X�ĪG���Q����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �b�s�����[����}�l�H�J�ĪG
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
        // �H�J�]�v���ܶ¡^
        yield return StartCoroutine(FadeIn());

        // �[���s����
        SceneManager.LoadScene(sceneName);

        // ���ݳ����[��������A�A����H�X
        yield return new WaitForSeconds(fadeDuration);

        // �H�X�]�v����ܷs�����^
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
            color.a = Mathf.Lerp(0f, 1f, timer / fadeDuration); // ���ܳz���ר� 1
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1f; // �����ܶ�
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
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration); // ���ܳz���ר� 0
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0f; // �����z��
        fadeImage.color = color;
        isFading = false;
    }

    private void OnDestroy()
    {
        // �Ѱ����U�����[���ƥ�
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
