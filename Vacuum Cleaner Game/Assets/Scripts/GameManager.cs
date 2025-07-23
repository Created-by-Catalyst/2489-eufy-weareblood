using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;

    int currentSection = 0;

    [SerializeField]
    HUDManager hudManager;

    [SerializeField]
    GameObject[] playerVehicles;
    [SerializeField]
    GameObject[] playerCameras;
    [SerializeField]
    Animator[] messcots;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartTimer(startTime));
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        hudManager.UpdateScoreText(score);
    }

    float startTime = 45f; // seconds
    float remainingTime;

    IEnumerator StartTimer(float time)
    {
        remainingTime = time;

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            hudManager.UpdateTimeText(remainingTime);
            yield return null;
        }
        //Debug.Log("Countdown complete!");

        NextSection();

    }

    void NextSection()
    {
        if (currentSection == 2) return;

        playerVehicles[currentSection].SetActive(false);
        playerCameras[currentSection].SetActive(false);
        currentSection++;
        playerCameras[currentSection].SetActive(true);
        playerVehicles[currentSection].SetActive(true);
        StartCoroutine(StartTimer(startTime));

        messcots[currentSection].SetInteger("Path", currentSection);
    }


}

