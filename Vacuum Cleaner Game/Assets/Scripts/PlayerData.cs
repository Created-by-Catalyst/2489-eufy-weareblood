using UnityEngine;
using System.IO;
using System.Collections.Generic;


#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif

public struct HighscoreEntry : System.IComparable<HighscoreEntry>
{
    public string name;
    public int finalScore;

    public int CompareTo(HighscoreEntry other)
    {
        // We want to sort from highest to lowest, so inverse the comparison.
        return other.finalScore.CompareTo(finalScore);
    }
}

/// <summary>
/// Save data for the game. This is stored locally in this case, but a "better" way to do it would be to store it on a server
/// somewhere to avoid player tampering with it. Here potentially a player could modify the binary file to add premium currency.
/// </summary>
public class PlayerData
{
    static protected PlayerData m_Instance;
    static public PlayerData instance { get { return m_Instance; } }

    protected string saveFile = "";



    public List<string> characters = new List<string>();    // Inventory of characters owned.
    public int usedCharacter;                               // Currently equipped character.
    public int usedAccessory = -1;
    public List<string> characterAccessories = new List<string>();  // List of owned accessories, in the form "charName:accessoryName".
    public List<string> themes = new List<string>();                // Owned themes.
    public int usedTheme;                                           // Currently used theme.
    public List<HighscoreEntry> highscores = new List<HighscoreEntry>();

    public string previousName = "Black Woman";

    public bool licenceAccepted;
    public bool tutorialDone;

    public float masterVolume = float.MinValue, musicVolume = float.MinValue, masterSFXVolume = float.MinValue;

    //ftue = First Time User Expeerience. This var is used to track thing a player do for the first time. It increment everytime the user do one of the step
    //e.g. it will increment to 1 when they click Start, to 2 when doing the first run, 3 when running at least 300m etc.
    public int ftueLevel = 0;
    //Player win a rank ever 300m (e.g. a player having reached 1200m at least once will be rank 4)
    public int rank = 0;

    // This will allow us to add data even after production, and so keep all existing save STILL valid. See loading & saving for how it work.
    // Note in a real production it would probably reset that to 1 before release (as all dev save don't have to be compatible w/ final product)
    // Then would increment again with every subsequent patches. We kept it to its dev value here for teaching purpose. 
    static int s_Version = 12;




    public int GetScorePlace(int finalScore)
    {
        HighscoreEntry entry = new HighscoreEntry();
        entry.finalScore = finalScore;
        entry.name = "";

        int index = highscores.BinarySearch(entry);

        return index < 0 ? (~index) : index;
    }

    public void InsertScore(int finalScore, string name)
    {
        HighscoreEntry entry = new HighscoreEntry();
        entry.finalScore = finalScore;
        entry.name = name;

        highscores.Insert(GetScorePlace(finalScore), entry);

        // Keep only the 10 best scores.
        while (highscores.Count > 10)
            highscores.RemoveAt(highscores.Count - 1);
    }

    // File management

    static public void Create()
    {
        if (m_Instance == null)
        {
            m_Instance = new PlayerData();
        }

        m_Instance.saveFile = Application.persistentDataPath + "/save.bin";

        if (File.Exists(m_Instance.saveFile))
        {
            // If we have a save, we read it.
            m_Instance.Read();
        }
        else
        {
            // If not we create one with default data.
            NewSave();
        }
    }

    static public void NewSave()
    {
        m_Instance.Save();
    }

    public void Read()
    {
        BinaryReader r = new BinaryReader(new FileStream(saveFile, FileMode.Open));

        int ver = r.ReadInt32();

        if (ver < 6)
        {
            r.Close();

            NewSave();
            r = new BinaryReader(new FileStream(saveFile, FileMode.Open));
            ver = r.ReadInt32();
        }


        // Added highscores.
        if (ver >= 3)
        {
            highscores.Clear();
            int count = r.ReadInt32();
            for (int i = 0; i < count; ++i)
            {
                Debug.Log("Save highscore: count-" + count);

                HighscoreEntry entry = new HighscoreEntry();
                entry.name = r.ReadString();
                entry.finalScore = r.ReadInt32();

                highscores.Add(entry);
            }
        }

        r.Close();
    }

    public void Save()
    {
        BinaryWriter w = new BinaryWriter(new FileStream(saveFile, FileMode.OpenOrCreate));


        // Write highscores.
        w.Write(highscores.Count);
        for (int i = 0; i < highscores.Count; ++i)
        {
            Debug.Log("Save highscore: name-" + highscores[i].name + " score-" + highscores[i].finalScore);

            w.Write(highscores[i].name);
            w.Write(highscores[i].finalScore);
        }

        w.Close();
    }


}

// Helper class to cheat in the editor for test purpose
#if UNITY_EDITOR
public class PlayerDataEditor : Editor
{
    [MenuItem("Trash Dash Debug/Clear Save")]
    static public void ClearSave()
    {
        File.Delete(Application.persistentDataPath + "/save.bin");
    }
}
#endif