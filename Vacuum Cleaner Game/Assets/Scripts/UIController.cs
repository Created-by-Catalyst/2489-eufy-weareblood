using System.Collections;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Animator uiAnimations;

    [SerializeField]
    TMP_Text camCountdownText;

    [SerializeField]
    GameObject splashScreen;

    public void CameraSceneLoaded()
    {
        splashScreen.SetActive(false);
        StartCoroutine(StartTimer());
    }

    float remainingTime;
    IEnumerator StartTimer()
    {
        float totalTime = 5;

        remainingTime = totalTime;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            camCountdownText.text = ((int)remainingTime).ToString();
            yield return null;
        }

        //Debug.Log("Countdown complete!");
        uiAnimations.SetTrigger("PhotoTaken");

    }

    public void PedalPressed()
    {
        uiAnimations.SetTrigger("PedalPressed");
    }

}
