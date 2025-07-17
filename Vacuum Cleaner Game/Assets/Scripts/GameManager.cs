using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int score = 0;

    [SerializeField]
    HUDManager hudManager;


    private void Awake()
    {
        instance = this;
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        hudManager.UpdateScoreText(score);
    }


}

