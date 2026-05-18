using System.IO;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "SaveData.json");
    }

    public void RequstSaveData(PlayerAtkCountModel data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(), json);
        Debug.Log($"저장 완료: {GetPath()}");
    }

    public PlayerAtkCountModel RequstLoadSaveData()
    {
        string path = GetPath();
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerAtkCountModel data = JsonUtility.FromJson<PlayerAtkCountModel>(json);
            Debug.Log("데이터를 불러왔습니다.");
            return data;
        }
        else
        {
            Debug.LogWarning("세이브 파일이 없습니다. 새 데이터를 생성합니다.");
            return GetDefaultPlayerData();
        }
    }

    private PlayerAtkCountModel GetDefaultPlayerData()
    {
        var newPlayerData = new PlayerAtkCountModel();
        newPlayerData.attackCount = 0;
        return newPlayerData;
    }

}