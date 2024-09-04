using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int bestScore = 0;
    public TextMeshProUGUI BestScore;
    
    [System.Serializable]
    class SaveData
    {
        public int bestScore;
    }
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
        LoadScore();
        if(bestScore == 0)
        {
            BestScore.text = $"   Best Score ";
        }
        else BestScore.text = $"Best Score: {bestScore}";
    }

    public void SaveScore()
    {

        SaveData data = new SaveData();
        data.bestScore = bestScore;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/test.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/test.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
        }
    }

    private void OnApplicationQuit()
    {
        SaveScore();
    }
}
