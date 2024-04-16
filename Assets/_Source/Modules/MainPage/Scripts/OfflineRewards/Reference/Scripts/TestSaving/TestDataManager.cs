using MainPage;
using System.IO;
using UnityEngine;

public class TestDataManager : MonoBehaviour
{
    private const string SaveName = "MyGameSave.json";
    public TestPlayerData Data { get; private set; }

    private string _path;

    private void Awake()
    {
        _path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + SaveName;
        LoadDataFromFile();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(Data);
        File.WriteAllText(_path, json);
    }

    public void LoadDataFromFile()
    {
        if (File.Exists(_path))
        {
            string json = File.ReadAllText(_path);
            Debug.Log("Local save exists: " + json);
            Data = JsonUtility.FromJson<TestPlayerData>(json);
        }
        else
        {
            Debug.Log("Save doesn't exists ... creating new game");
            Data = new TestPlayerData();
            Save();
        }
    }
}
