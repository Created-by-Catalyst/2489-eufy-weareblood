using RGSK;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;

    public int currentSection = 1;

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

    private void Start()
    {
        PauseControl();


        //DEBUG
        //StartGame();
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
        uiController.PedalPressed();
    }


    public void StartGame()
    {
        StartCoroutine(StartTimer(startTime));
    }

    public void AddScore(int scoreToAdd, int stainTier, string description)
    {
        score += scoreToAdd;
        hudManager.StainCollected(score, stainTier, description);
    }

    float startTime = 45f; // seconds
    float remainingTime;

    IEnumerator StartTimer(float time)
    {

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

        hudManager.ingameHUD.SetActive(true);

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
        PauseControl();
        uiController.ShowResults();
    }

    public void GoToNextLevel()
    {
        if (currentSection == 2) return;

        uiController.GoToNextLevel();
    }


    public void LevelLoadTransition()
    {
        playerVehicles[currentSection].SetActive(false);
        playerCameras[currentSection].SetActive(false);
        currentSection++;
        playerCameras[currentSection].SetActive(true);
        playerVehicles[currentSection].SetActive(true);

        hudManager.ingameHUD.SetActive(false);
        hudManager.UpdateLevelIcons();
    }


}

