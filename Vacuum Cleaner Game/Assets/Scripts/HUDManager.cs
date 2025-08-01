using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class HUDManager : MonoBehaviour
{
    [SerializeField]
    public GameObject ingameHUD;

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

    [SerializeField]
    Sprite[] levelIcons;

    public void UpdateLevelIcons()
    {
        if (GameManager.instance.currentSection == 0) return;

        for (int i = 0; i < messes.Length; i++)
        {
            messes[i].GetComponent<Image>().sprite = levelIcons[((GameManager.instance.currentSection - 1) * 3) + i];
        }
        for (int i = 0; i < pointValues.Length; i++)
        {
            pointValues[i].text = (i * 10).ToString();
        }

    }


    public void StainCollected(int newScore, int stainTier, string description)
    {
        scoreText.text = "Score: " + newScore.ToString();
        messes[stainTier].BlinkCycle();
        StopAllCoroutines();

        StartCoroutine(PopUpMessage(description));
    }

    IEnumerator PopUpMessage(string description)
    {
        popupMessages.text = description;

        yield return new WaitForSeconds(2);


        popupMessages.text = "";
    }

    public void UpdateTimeText(float newTime)
    {

        timerText.text = string.Format("0:{0:00}", (int)newTime);

    }

    public void UpdateCountdownText(int newTime)
    {
        if (newTime > 0)
        {
            countdownText.fontSize = 128;
            countdownText.text = newTime.ToString() + "..";
        }
        else
        {
            countdownText.fontSize = 64;
            countdownText.text = "Clean up the room!";
        }


        if (newTime == -1)
        {
            countdownText.text = "";
        }

    }

}

