using TMPro;
using UnityEngine;


public class HUDManager : MonoBehaviour
{

    [SerializeField]
    TMP_Text popupMessages;

    [SerializeField]
    TMP_Text scoreText;

    [SerializeField]
    TMP_Text timerText;
    [SerializeField]
    TMP_Text countdownText;

    [SerializeField]
    TMP_Text[] pointValues;

    [SerializeField]
    BlinkLoop[] messes;


    public void StainCollected(int newScore, int stainTier, string description)
    {
        scoreText.text = "Score: " + newScore.ToString();
        messes[stainTier].BlinkCycle();
        popupMessages.text = description;
    }

    public void UpdateTimeText(float newTime)
    {

        timerText.text = string.Format("0:{0:00}", (int)newTime);

    }

    public void UpdateCountdownText(int newTime)
    {
        if(newTime > 0)
        {
            countdownText.fontSize = 128;
            countdownText.text = newTime.ToString() + "..";
        }
        else
        {
            countdownText.fontSize = 64;
            countdownText.text = "Clean up the room!";
        }


        if(newTime == -1)
        {
            countdownText.text = "";
        }

    }

}

