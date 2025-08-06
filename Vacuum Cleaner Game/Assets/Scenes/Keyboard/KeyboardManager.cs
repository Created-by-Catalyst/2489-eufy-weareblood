using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class KeyboardManager : MonoBehaviour
{
    [SerializeField] TMP_Text nameEntry;

    [SerializeField] GameObject startingKey;

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(startingKey);
    }

    public void KeyPress(string key)
    {
        if (key == "Enter")
        {
            //onScreenKeyboard.gameObject.SetActive(false);
            EventSystem.current.SetSelectedGameObject(null);

            UIController.instance.GoToLeaderboard(nameEntry.text, GameManager.instance.overallScore);

        }
        else if (key == "Back")
        {
            if (nameEntry.text.Length > 0)
            {
                nameEntry.text = nameEntry.text.Substring(0, nameEntry.text.Length - 1);
            }
        }
        else
        {
            nameEntry.text += key;
        }

        //playerEntryOnKeyboard.text = playerEntry.text;
    }



    // Update is called once per frame
    void Update()
    {

    }
}
