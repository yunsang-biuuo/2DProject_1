using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    private PlayerAtkCountModel _playerModel = new PlayerAtkCountModel();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadSaveData();
    }

    public void SaveData()
    {
        NetworkManager.Instance.RequstSaveData(_playerModel);
    }

    public void SaveAndEndGame()
    {
        SaveData();
        Application.Quit();
    }

    private void LoadSaveData()
    {
        _playerModel = NetworkManager.Instance.RequstLoadSaveData();
    }

    // 공격 횟수 올리는 메서드
    public void AddAttackCount()
    {
        _playerModel.attackCount++;
        Debug.Log("공격 횟수: " + _playerModel.attackCount);
    }
}