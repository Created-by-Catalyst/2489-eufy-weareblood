using TMPro;
using UnityEngine;


public class HUDManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    TMP_Text timerText;


    public void UpdateScoreText(int newScore)
    {
        scoreText.text = newScore.ToString();
    }

    public void UpdateTimeText(float newTime)
    {
        timerText.text = ((int)newTime).ToString();
    }

}

