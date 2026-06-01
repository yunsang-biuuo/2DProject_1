using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }

    public enum GameState { Init, Loading, Lobby, Playing }
    public GameState CurrentState { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CurrentState = GameState.Init;

        UIManager.Instance.ShowStartupUIOnGameStart();
    }

    public void OnLoginSuccess()
    {
        CurrentState = GameState.Loading;
        UIManager.Instance.CloseStartLoginUI();

        var uiBase = UIManager.Instance.OpenLoadingUI();
        if (uiBase is LoadingUI loadingUI)
        {
            loadingUI.StartLoading(() => StartCoroutine(CoTransitionToLobby()));
        }
    }

    private IEnumerator CoTransitionToLobby()
    {
        CurrentState = GameState.Lobby;
        UIManager.Instance.CloseStartLoginUI();
        UIManager.Instance.OpenMainUI(UIType.RobbyUI);

        yield return new WaitForEndOfFrame();

        UIManager.Instance.CloseLoadingUI();
    }

    public void EnterChapterMap(string chapterId)
    {
        Debug.Log($"[GameManager] {chapterId} 맵으로 진입을 시작합니다.");

        // 1. 인게임에 필요한 UI 처리 (로비 끄기, 챕터창 끄기 등)
        UIManager.Instance.CloseMainUI(UIType.RobbyUI);
        UIManager.Instance.CloseContentUI(UIType.ChapterScUI);

        // 2. 맵 좌표 이동 시스템 구현 구역
        // 아래 공간에 대화방에 올려주실 '좌표 이동 코드'를 합치면 됩니다.
        switch (chapterId)
        {
            case "SlotBox_Chap1_1":
                // 플레이어.transform.position = 새로운 좌표;
                Debug.Log("1-1 스테이지 좌표로 이동 완료!");
                break;

            case "SlotBox_Chap1_2":
                Debug.Log("1-2 스테이지 좌표로 이동 완료!");
                break;

                // ... 나중에 추가될 챕터들 분기 처리
        }
    }

    public void SaveAndEndGame()
    {
        Application.Quit();
    }
}
