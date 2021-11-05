using System;
using System.IO;
using UnityEngine;

public class JsonManager
{
    public void WriteDataToFile(ScriptableObject data, string dataPath)
    {
        string path = Path.Combine(Application.persistentDataPath, dataPath);
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(path, json);
    }

    public T ReadDataFromFile<T>(string dataPath) where T : ScriptableObject
    {
        string path = Path.Combine(Application.persistentDataPath, dataPath);
        if (File.Exists(path))
            return ReadExistFile<T>(path);
        else
            return CreateNewInstance<T>(path);
    }

    private T ReadExistFile<T>(string dataPath) where T : ScriptableObject
    {
        T data = ScriptableObject.CreateInstance<T>();
        string json = File.ReadAllText(dataPath);
        JsonUtility.FromJsonOverwrite(json, data);
        return data;
    }

    private T CreateNewInstance<T>(string dataPath) where T : ScriptableObject
    {
        T data = ScriptableObject.CreateInstance<T>();
        WriteDataToFile(data, dataPath);
        return data;
    }
}