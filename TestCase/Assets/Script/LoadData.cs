using System;
using System.IO;
using UnityEngine;
using UnityEngine.Events;

public class LoadData : MonoBehaviour
{
    public Map map = new Map();

    private string path;

    public event UnityAction DataIsLoad;

    private string pathNormal = Application.streamingAssetsPath + "/testing_views_settings_normal_level.json";
    private string pathHard = Application.streamingAssetsPath + "/testing_views_settings_hard_level.json";

    public void LoadDataNormalMap()
    {
        map = JsonUtility.FromJson<Map>(File.ReadAllText(pathNormal));
        DataIsLoad?.Invoke();
    }
    public void LoadDataHardMap()
    {
        map = JsonUtility.FromJson<Map>(File.ReadAllText(pathHard));
        DataIsLoad?.Invoke();
    }

    [Serializable]
    public struct MapStruct
    {
        public string Id;
        public string Type;
        public float X;
        public float Y;
        public float Width;
        public float Height;

    }

    [Serializable]
    public class Map
    {
        public MapStruct[] List;
    }
}
