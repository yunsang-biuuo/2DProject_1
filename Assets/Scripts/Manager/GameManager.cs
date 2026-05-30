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

    public void SaveAndEndGame()
    {
        Application.Quit();
    }
}
