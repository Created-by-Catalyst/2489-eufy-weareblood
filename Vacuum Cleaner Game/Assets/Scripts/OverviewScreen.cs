using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverviewScreen : MonoBehaviour
{

    [SerializeField]
    GameObject pressPedalHint;

    [SerializeField]
    Sprite[] overviewSprites;

    [SerializeField]
    Animator overviewAnim;

    [SerializeField]
    Animator resultsAnim;

    [SerializeField]
    Sprite[] resultsSprites;

    [SerializeField]
    public TMP_Text[] scores;


    public void ShowOverview()
    {
        int level = GameManager.instance.currentSection;

        overviewAnim.GetComponent<Image>().sprite = overviewSprites[level];

        overviewAnim.SetBool("OverviewShowing", true);
        //pressPedalHint.SetActive(true);

        Invoke("HideOverview", 6.5f);
    }

    public void HideOverview()
    {
        int level = GameManager.instance.currentSection;

        if (overviewAnim.GetBool("OverviewShowing") == true)
        {
            overviewAnim.SetBool("OverviewShowing", false);
            //pressPedalHint.SetActive(false);

            GameManager.instance.StartGame();
        }

    }

    bool resultsRecentlyActive = false;

    public void ShowResults()
    {
        int level = GameManager.instance.currentSection;

        resultsAnim.GetComponent<Image>().sprite = resultsSprites[level];

        resultsAnim.SetBool("OverviewShowing", true);

        MusicHandler.instance.PlaySong(GameManager.instance.currentSection + 2);

        Invoke("HideResults", 5f);
    }

    void ResetRecentlyActiveBool()
    {
        resultsRecentlyActive = false;
    }

    public void HideResults()
    {
        int level = GameManager.instance.currentSection;


        resultsAnim.SetBool("OverviewShowing", false);
        GameManager.instance.GoToNextLevel();


    }



}
