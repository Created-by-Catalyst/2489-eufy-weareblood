using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    Animator uiAnimations;

    [SerializeField]
    TMP_Text camCountdownText;

    [SerializeField]
    GameObject splashScreen;

    [SerializeField]
    WebcamDisplay webcamDisplay;

    [SerializeField]
    Sprite[] levelDescriptions;

    [SerializeField]
    Sprite[] levelSplashes;


    [SerializeField]
    Image levelDesc;
    [SerializeField]
    Image levelSplash;


    [SerializeField]
    OverviewScreen overviewScreen;



    public void GoToNextLevel()
    {
        levelDesc.sprite = levelDescriptions[GameManager.instance.currentSection];
        levelSplash.sprite = levelSplashes[GameManager.instance.currentSection];
        uiAnimations.SetTrigger("TransitionLevel");
    }

    public void LoadNextLevel()
    {
        GameManager.instance.PauseControl();
        GameManager.instance.LevelLoadTransition();
    }

    public void NextLevelLoaded()
    {
        overviewScreen.ShowOverview();
    }


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

        webcamDisplay.CaptureImage();

        //Debug.Log("Countdown complete!");
        uiAnimations.SetTrigger("PhotoTaken");

    }

    public void ResetPedalTrigger()
    {
        uiAnimations.ResetTrigger("PedalPressed");
    }

    public void PedalPressed()
    {
        print("pedal pressed");
        uiAnimations.SetTrigger("PedalPressed");
        overviewScreen.HideOverview();
        overviewScreen.HideResults();
    }



    public void ShowResults()
    {
        overviewScreen.ShowResults();
    }


}
