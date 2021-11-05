using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : ScriptableObject
{
    public List<PlayData> playDatas;

    public GameData()
    {
        playDatas = new List<PlayData>();
    }

    public void AddNewData(PlayData playData)
    {
        playDatas.Add(playData);
    }
}

[System.Serializable]
public class PlayData
{
    public string Name;
    public string Score;
    public string Time;
}