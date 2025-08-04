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
    public OverviewScreen overviewScreen;

    public void PlaySound(int id)
    {
        AudioHandler.instance.PlaySound(id);
    }

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


    public void ChangeMusic()
    {
        MusicHandler.instance.PlaySong(GameManager.instance.currentSection + 1);
    }

    public void NextLevelLoaded()
    {
        GameManager.instance.IntroAnimations();
    }

    public void StainCollected(int[] score, int stainTier)
    {
        overviewScreen.scores[stainTier].text = score[stainTier].ToString() + "pts";
        overviewScreen.scores[3].text = score[3].ToString() + "pts";
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
        //print("pedal pressed");
        uiAnimations.SetTrigger("PedalPressed");
        //overviewScreen.HideOverview();
        //overviewScreen.HideResults();
    }


    public void Confirm()
    {
        AudioHandler.instance.PlaySound(2);
    }


    public void ShowResults()
    {
        overviewScreen.ShowResults();
    }


}
