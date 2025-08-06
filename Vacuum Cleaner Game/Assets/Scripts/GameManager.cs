using RGSK;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int[] score = new int[] { 0, 0, 0, 0 };

    public int[] roomScores = new int[] { 0, 0, 0 };

    public int overallScore = 0;

    public int currentSection = 1;


    [SerializeField]
    HUDManager hudManager;
    [SerializeField]
    UIController uiController;

    [SerializeField]
    public GameObject[] playerVehicles;
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



    }

    private void OnEnable()
    {
        _actions = new InputActions();

        _actions.asset = actionMap;

        _actions.Vehicle.Enable();

        _actions.Vehicle.Throttle.performed += OnThrottle;
        _actions.Vehicle.Pause.performed += ResetGame;
    }

    private void OnDisable()
    {
        _actions.Vehicle.Throttle.performed -= OnThrottle;
        _actions.Vehicle.Pause.performed -= ResetGame;
    }

    private void ResetGame(InputAction.CallbackContext context)
    {

        SceneManager.LoadScene(0);

    }

    bool uiControl = false;

    private void Start()
    {
        PauseControl();

        Invoke("EnableUIControl", 1f);

        // Reduce damper force (resistance) to near zero
        //LogitechGSDK.LogiPlayDamperForce(0, 0); // 0 = no resistance

        //DEBUG
        //StartGame();
    }

    private void EnableUIControl()
    {
        uiControl = true;
    }

    public void PauseControl()
    {
        foreach (var vehicle in playerVehicles)
        {
            vehicle.GetComponent<VehicleController>().enabled = false;
            vehicle.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void UnpauseControl()
    {
        foreach (var vehicle in playerVehicles)
        {
            vehicle.GetComponent<VehicleController>().enabled = true;
            vehicle.GetComponent<Rigidbody>().isKinematic = false;
        }
    }



    void OnThrottle(InputAction.CallbackContext context)
    {
        //ThrottleInput = context.ReadValue<float>();
        if (uiControl) uiController.PedalPressed();
    }


    public void IntroAnimations()
    {
        messcots[currentSection].GetComponent<MesscotManager>().MesscotIntroAnims();
    }

    public void IntrosEnded()
    {
        uiController.overviewScreen.ShowOverview();
    }

    public void StartGame()
    {
        StartCoroutine(StartTimer(startTime));
    }

    public void ResetScore()
    {
        overallScore += score[3];
        roomScores[currentSection] = score[3];

        score = new int[] { 0, 0, 0, 0 };

    }

    public void AddScore(int scoreToAdd, int stainTier, string description)
    {
        score[stainTier] += scoreToAdd;
        score[3] += scoreToAdd;

        uiController.StainCollected(score, stainTier);
        hudManager.StainCollected(score, stainTier, description);

    }

    float startTime = 45f; // seconds
    float remainingTime;

    IEnumerator StartTimer(float time)
    {

        AudioHandler.instance.PlaySound(5);

        remainingTime = 4;
        while (remainingTime >= 0)
        {
            remainingTime -= Time.deltaTime;
            hudManager.UpdateCountdownText((int)remainingTime);
            yield return null;
        }



        hudManager.UpdateCountdownText(-1);


        messcots[currentSection].SetInteger("Path", currentSection);



        UnpauseControl();

        remainingTime = time + 1;

        hudManager.HUDAnimator.Play("HUDIn");

        AudioHandler.instance.PlaySound(4);

        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            hudManager.UpdateTimeText(remainingTime);
            yield return null;
        }
        //Debug.Log("Countdown complete!");

        FinishLevel();

    }

    void FinishLevel()
    {
        hudManager.HUDAnimator.Play("HUDOut");
        AudioHandler.instance.PlaySound(4);
        PauseControl();
        uiController.ShowResults();
    }

    public void GoToNextLevel()
    {

        ResetScore();

        if (currentSection == 2)
        {

            uiController.GoToFinalResultsScreen();

            return;
        }

        uiController.GoToNextLevel();
    }


    public void LevelLoadTransition()
    {
        playerVehicles[currentSection].SetActive(false);
        playerCameras[currentSection].SetActive(false);
        currentSection++;
        playerCameras[currentSection].SetActive(true);
        playerVehicles[currentSection].SetActive(true);

        hudManager.scoreText.text = "Score: 0";
        hudManager.UpdateLevelIcons();
    }


}

