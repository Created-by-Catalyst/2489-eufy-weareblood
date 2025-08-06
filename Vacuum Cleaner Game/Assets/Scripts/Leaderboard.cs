using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class Leaderboard : MonoBehaviour
{
    public List<LeaderboardEntry> entries = new List<LeaderboardEntry>();

    public LeaderboardCell[] cells;

    public bool generateRanks = true;

    private void Start()
    {
        LoadFromFile();
        SortLeaderboard();
        PopulateCells();
    }

    void PopulateCells()
    {
        if (generateRanks)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (i < entries.Count)
                {
                    cells[i].PopulateCell(i + 1, entries[i].Name, entries[i].Score);
                    cells[i].gameObject.SetActive(true);
                }
                else
                {
                    cells[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < cells.Length; i++)
            {
                if (i < entries.Count)
                {
                    cells[i].PopulateCell(entries[i].Name, entries[i].Score);
                    cells[i].gameObject.SetActive(true);
                }
                else
                {
                    cells[i].gameObject.SetActive(false);
                }
            }
        }
    }

    // Add a new score
    public void AddEntry(string name, int score)
    {
        entries.Add(new LeaderboardEntry(name, score));
        SortLeaderboard();
        PopulateCells();
    }



    // Sort entries from highest to lowest score
    private void SortLeaderboard()
    {
        entries = entries.OrderByDescending(e => e.Score).ToList();
    }




    //SAVING TO LOCAL STORAGE=================================================================================================

    public static string FileName => Application.persistentDataPath + "/leaderboard.json";

    public void LoadFromFile()
    {
        if (File.Exists(FileName))
        {
            string json = File.ReadAllText(FileName);
            entries = JsonUtility.FromJson<LeaderboardListWrapper>(json)?.wrapperEntries ?? new List<LeaderboardEntry>();
        }
        else
        {
            entries = new List<LeaderboardEntry>();
        }
    }

    public void SaveToFile()
    {
        string json = JsonUtility.ToJson(new LeaderboardListWrapper { wrapperEntries = entries }, true);
        File.WriteAllText(FileName, json);
    }


    [ContextMenu("Clear Save")]
    public void ClearSaveFile()
    {
        if (File.Exists(FileName))
        {
            File.Delete(FileName);
            Debug.Log("Leaderboard save file deleted.");
        }
        else
        {
            Debug.Log("No leaderboard file to delete.");
        }

        entries.Clear(); // Clear in-memory list too
    }



    [System.Serializable]
    private class LeaderboardListWrapper
    {
        public List<LeaderboardEntry> wrapperEntries;
    }


}

[Serializable]
public class LeaderboardEntry
{
    public string Name;
    public int Score;

    public LeaderboardEntry(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
