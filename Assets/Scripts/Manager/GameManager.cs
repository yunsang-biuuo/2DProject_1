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

    private GameObject _currentActiveMap; // 현재 화면에 켜진 맵 오브젝트 보관용 변수

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentState = GameState.Init;

        UIManager.Instance.ShowStartupUIOnGameStart();
    }

    public void OnLoginProcess()
    {
        CurrentState = GameState.Loading;
        UIManager.Instance.CloseStartLoginUI();

        var uiBase = UIManager.Instance.OpenLoadingUI();
        if (uiBase is LoadingUI loadingUI)
        {
            loadingUI.StartLoading(() => StartCoroutine(ProcessLobby()));
        }
    }

    // 기본 로직 시작->로비
    private IEnumerator ProcessLobby()
    {
        CurrentState = GameState.Lobby;
        UIManager.Instance.CloseStartLoginUI();
        UIManager.Instance.OpenMainUI(UIType.RobbyUI);

        yield return new WaitForEndOfFrame();

        UIManager.Instance.CloseLoadingUI();
    }

    public void EnterChapterMap(ChapterType chapterType)
    {
        CurrentState = GameState.Playing;

        UIManager.Instance.CloseMainUI(UIType.RobbyUI);
        UIManager.Instance.CloseContentUI(UIType.ChapterScUI);

        if (BGIController.Instance != null)
        {
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

        // 꺼져있는 맵 오브젝트까지 완벽하게 찾아내기 위한 방어용 코드
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
            targetMapObj.SetActive(true);

            SpawnSpot[] spawnSpots = targetMapObj.GetComponentsInChildren<SpawnSpot>(true);

            bool spawnFound = false;

            foreach (SpawnSpot spot in spawnSpots)
            {
                // 타입이 Player인 스팟 찾기
                if (spot.SpawnSpotType == SpawnSpotType.Player)
                {
                    // SpawnSpot 내부에 구현해 둔 캐릭터 좌표 이동 함수
                    spot.ForceSpawnFromServer();

                    spawnFound = true;
                    break;
                }
            }
            
        }
        else
        {
            Debug.LogError($"[GameManager] 씬(Hierarchy)에서 '{targetMapName}' 이름을 가진 맵 오브젝트를 찾을 수 없습니다!");
        }
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
