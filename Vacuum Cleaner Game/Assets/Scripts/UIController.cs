using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static UIController instance;

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
    Leaderboard leaderboard;
    [SerializeField]
    Leaderboard miniLeaderboard;

    [SerializeField]
    KeyboardManager keyboardManager;

    [SerializeField]
    public OverviewScreen overviewScreen;

    [SerializeField]
    TMP_Text[] finalResultsScores;


    private void Awake()
    {
        instance = this;
    }


    public void PlayAnimation(string anim)
    {
        uiAnimations.Play(anim);
    }


    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }


    public void GoToFinalResultsScreen()
    {
        PlayAnimation("ResultsAnim");

        for (int i = 0; i < 3; i++)
        {
            finalResultsScores[i].text = GameManager.instance.roomScores[i].ToString();
        }
        finalResultsScores[3].text = GameManager.instance.overallScore.ToString();
    }

    public void GoToLeaderboard(string name, int score)
    {
        leaderboard.AddEntry(name, score);

        PlayAnimation("ResultsTransition");

        StartCoroutine(MusicHandler.instance.FadeOut(MusicHandler.instance.musicSources[3], 5));

        Invoke("EndGame", 6f);
    }
    void EndGame()
    {
        leaderboard.SaveToFile();
        PlayAnimation("ResultsOut");
    }


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
