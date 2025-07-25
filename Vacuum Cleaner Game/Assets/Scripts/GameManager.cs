using RGSK;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;

    int currentSection = 0;

    [SerializeField]
    HUDManager hudManager;
    [SerializeField]
    UIController uiController;

    [SerializeField]
    GameObject[] playerVehicles;
    [SerializeField]
    GameObject[] playerCameras;
    [SerializeField]
    Animator[] messcots;


    [SerializeField]
    public InputActionAsset actionMap;


    [SerializeField]
    public InputActions _actions;
    private void Awake()
    {
        instance = this;

        _actions = new InputActions();

        _actions.asset = actionMap;

        _actions.Vehicle.Enable();

        _actions.Vehicle.Throttle.performed += OnThrottle;

    }

    void OnThrottle(InputAction.CallbackContext context)
    {
        //ThrottleInput = context.ReadValue<float>();
        uiController.PedalPressed();
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

