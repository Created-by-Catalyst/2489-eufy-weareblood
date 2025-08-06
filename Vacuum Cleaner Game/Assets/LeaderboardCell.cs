using TMPro;
using UnityEngine;

public class LeaderboardCell : MonoBehaviour
{
    public TMP_Text cellRank;
    public TMP_Text cellName;
    public TMP_Text cellScore;

    public void PopulateCell(string name, int score)
    {
        cellName.text = name;
        cellScore.text = score.ToString() + "pts";
    }

    public void PopulateCell(int rank, string name, int score)
    {
        cellRank.text = rank.ToString();
        PopulateCell(name, score);
    }
}
