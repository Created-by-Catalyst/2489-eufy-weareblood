using TMPro;
using UnityEngine;


public class HUDManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text scoreText;



    public void UpdateScoreText(int newScore)
    {
        scoreText.text = newScore.ToString();
    }


}

