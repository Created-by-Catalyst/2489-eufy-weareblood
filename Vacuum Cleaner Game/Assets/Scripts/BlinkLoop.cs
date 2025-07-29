using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
public class BlinkLoop : MonoBehaviour
{
    float fadeDuration = 0.2f; // Time for each fade in/out

    private Image image;

    private bool blinking = false;

    Color startColor;

    public Color endColor;


    private void Start()
    {
        image = GetComponent<Image>();
        startColor = image.color;
    }

    public void BlinkCycle()
    {
        StopAllCoroutines();


        StartCoroutine(FadeLoopCoroutine());
    }


    IEnumerator FadeLoopCoroutine()
    {
        blinking = true;
        StartCoroutine(StopBlinking());

        while (blinking)
        {
            // Fade In
            yield return StartCoroutine(Fade(startColor, endColor));
            // Fade Out
            yield return StartCoroutine(Fade(endColor, startColor));
        }
    }

    IEnumerator StopBlinking()
    {
        yield return new WaitForSeconds(2f);

        blinking = false;
    }


        IEnumerator Fade(Color startColor, Color endColor)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            Color currentColor = Color.Lerp(startColor, endColor, elapsed / fadeDuration);
            image.color = currentColor;
            yield return null;
        }

       // Ensure final value
    }
}
