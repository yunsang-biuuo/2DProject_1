using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public enum GameState { Init, Loading, Lobby, Playing }
    public GameState CurrentState { get; private set; }

    [Header("Player Tracking")]
    [SerializeField] private GameObject _playerCharacter;
    public GameObject PlayerCharacter => _playerCharacter;

    private GameObject _currentActiveMap;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentState = GameState.Init;
        UIManager.Instance.ShowStartupUIOnGameStart();
    }

    // 맨 처음 시작 화면
    public void OnLoginProcess()
    {
        CurrentState = GameState.Loading;
        UIManager.Instance.CloseStartLoginUI();
        ShowLoadingProcess(() => StartCoroutine(ProcessLobby()));
    }

    // 로딩 UI 모듈화
    public void ShowLoadingProcess(Action onLoadingComplete)
    {
        var uiBase = UIManager.Instance.OpenLoadingUI();

        if (uiBase is LoadingUI loadingUI)
        {
            loadingUI.StartLoading(() =>
            {
                onLoadingComplete?.Invoke();
                UIManager.Instance.CloseLoadingUI();
            });
        }
    }

    // 기본 로직 시작->로비
    private IEnumerator ProcessLobby()
    {
        CurrentState = GameState.Lobby;
        UIManager.Instance.CloseStartLoginUI();
        UIManager.Instance.OpenRobbyUI();
        yield return new WaitForEndOfFrame();
        UIManager.Instance.CloseLoadingUI();
    }

    public void EnterChapterMap(ChapterType chapterType)
    {
        CurrentState = GameState.Playing;

        UIManager.Instance.CloseRobbyUI();
        UIManager.Instance.CloseChapterScUI();

        if (BGIController.Instance != null)
        {
            _currentActiveMap?.SetActive(false); // 기존에 켜진 맵이 있다면 꺼주는 안전장치 추가
            BGIController.Instance.ChangeBackground(chapterType);
        }

        string targetMapName = "";
        switch (chapterType)
        {
            case ChapterType.SlotBox_Chap1_1: targetMapName = "DownStreet_1"; break;
            case ChapterType.SlotBox_Chap1_2: targetMapName = "DownStreet_2"; break;
            case ChapterType.SlotBox_Chap2_1: targetMapName = "DownStreet_3"; break;
            case ChapterType.SlotBox_Chap2_2: targetMapName = "Factory_1"; break;
            case ChapterType.SlotBox_Chap2_3: targetMapName = "Factory_2"; break;
            case ChapterType.SlotBox_Chap3_1: targetMapName = "City_1"; break;
            case ChapterType.SlotBox_Chap3_2: targetMapName = "City_2"; break;
        }

        GameObject targetMapObj = null;

        if (targetMapObj == null)
        {
            var allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
            foreach (var obj in allObjects)
            {
                if (obj.name == targetMapName && obj.scene.isLoaded)
                {
                    targetMapObj = obj;
                    break;
                }
            }
        }

        if (targetMapObj != null)
        {
            _currentActiveMap = targetMapObj; // 현재 활성화된 맵 추적용
            targetMapObj.SetActive(true);

            SpawnSpot[] spawnSpots = targetMapObj.GetComponentsInChildren<SpawnSpot>(true);

            foreach (SpawnSpot spot in spawnSpots)
            {
                if (spot.SpawnSpotType == SpawnSpotType.Player)
                {
                    spot.ForceSpawnFromServer();
                    break;
                }
            }
        }
        else
        {
            Debug.LogError($"[GameManager] 씬(Hierarchy)에서 '{targetMapName}' 이름을 가진 맵 오브젝트를 찾을 수 없습니다!");
        }
    }

    // 메뉴 UI 켜고, 체력 리셋
    public void OnClick_StartGame()
    {
        Time.timeScale = 1f;

        UIManager.Instance.OpenMenuUI();

        CyborgPlayer player = FindObjectOfType<CyborgPlayer>();
        if (player != null)
        {
            player.ResetHP();
        }
    }

    // 플레이어 사망 관리
    public void OnPlayerDead()
    {
        UIManager.Instance.OpenGameOverPopUI();
    }

    // 로비 돌아오기
    public void ReturnToLobby()
    {
        Time.timeScale = 0f;
        UIManager.Instance.CloseMenuUI();
        UIManager.Instance.OpenRobbyUI();
    }

    // 플레이어 캐릭터 등록
    public void RegisterPlayer(GameObject playerObj)
    {
        _playerCharacter = playerObj;
    }

    public void SaveAndEndGame()
    {
        Application.Quit();
    }
}