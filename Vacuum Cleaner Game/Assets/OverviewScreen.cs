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
    Animator[] results;


    public void ShowOverview()
    {
        int level = GameManager.instance.currentSection;

        overviewAnim.GetComponent<Image>().sprite = overviewSprites[level];

        overviewAnim.SetBool("OverviewShowing", true);
        pressPedalHint.SetActive(true);
    }

    public void HideOverview()
    {
        int level = GameManager.instance.currentSection;

        if(overviewAnim.GetBool("OverviewShowing") == true)
        {
            overviewAnim.SetBool("OverviewShowing", false);
            pressPedalHint.SetActive(false);

            GameManager.instance.StartGame();
        }

    }


}
