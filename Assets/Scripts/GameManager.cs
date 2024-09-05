using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public TMP_InputField inputName;
    private MainManager mainManager;

    public int bestScore = 0;
    public string nameSave;
    public string temp;
    public TextMeshProUGUI BestScore;

    [System.Serializable]
    class SaveData
    {
        public string name;
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
        if (bestScore == 0)
        {
            BestScore.text = $"   Best Score ";
        }
        else BestScore.text = $"Best Score: {nameSave}: {bestScore}";
    }

    public void SaveScore()
    {

        SaveData data = new SaveData();
        data.bestScore = bestScore;
        data.name = nameSave;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/success.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/success.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            bestScore = data.bestScore;
            nameSave = data.name; 
        }
    }
    

    public void StartGame()
    {
        temp = inputName.text;
        SceneManager.LoadScene(1);
    }

    private void OnApplicationQuit()
    {
        SaveScore();
    }
}