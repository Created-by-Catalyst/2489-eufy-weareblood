using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class FadeLoop : MonoBehaviour
{
    public float fadeDuration = 1f; // Time for each fade in/out

    private TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();

        StartCoroutine(FadeLoopCoroutine());
    }


    private void OnEnable()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(FadeLoopCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    IEnumerator FadeLoopCoroutine()
    {
        while (true)
        {
            // Fade In
            yield return StartCoroutine(Fade(0f, 1f));
            // Fade Out
            yield return StartCoroutine(Fade(1f, 0f));
        }
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = text.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            text.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        text.color = new Color(color.r, color.g, color.b, endAlpha); // Ensure final value
    }
}
