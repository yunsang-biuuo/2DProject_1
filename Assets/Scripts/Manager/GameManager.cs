using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; set; }
    public GameState CurrentState { get; private set; }
    public enum GameState
    {
        Init,       // 초기화 및 로그인 단계
        Loading,    // 로딩 중
        Lobby,      // 메인 로비 화면
        Playing     // 실제 인게임 플레이
    }

    private PlayerAtkCountModel _playerModel = new PlayerAtkCountModel();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        LoadSaveData();
        StartCoroutine(GameFlowSequence());
    }

    private IEnumerator GameFlowSequence()
    {
        // 1. [초기화 및 로그인 단계]
        CurrentState = GameState.Init;

        // 데이터 세팅 (필요 시 유지)
        LoadSaveData();

        // 첫 시작 UI(StartLoginUI) 오픈 (확장 메서드 활용)
        UIManager.Instance.ShowStartupUIOnGameStart();

        // 유저가 로그인 버튼을 누르거나 화면을 탭할 때까지 대기하는 가상 조건
        // 실제 로그인 로직이 완성되면 조건문이나 이벤트를 꽂아주시면 됩니다.
        yield return new WaitForSeconds(2.0f); // 테스트용 2초 대기

        // 2. [로딩 단계]
        CurrentState = GameState.Loading;

        // 로그인 창을 닫고 로딩 스크린을 엽니다.
        UIManager.Instance.CloseStartLoginUI();
        UIManager.Instance.OpenLoadingUI();

        // 리소스 로드나 데이터 통신이 처리되는 가상의 로딩 시간
        // 만약 씬을 전환한다면 여기서 AsyncOperation을 기다려줍니다.
        yield return new WaitForSeconds(1.5f); // 테스트용 1.5초 대기

        // 3. [로비 진입 단계]
        CurrentState = GameState.Lobby; // 

        // 💡 핵심: 로딩 UI를 닫기 '전에' 로비 UI를 먼저 띄웁니다.
        UIManager.Instance.OpenMainUI(UIType.RobbyUI); // 

        // 💡 팁: UI가 켜지고 첫 프레임이 그려질 때까지 아주 잠깐 대기해 주면 더욱 안전합니다.
        yield return new WaitForEndOfFrame(); // 혹은 yield return null; (1프레임 대기)

        // 로비 UI가 뒤에 확실히 준비되었으므로, 가리고 있던 로딩 화면을 안전하게 닫습니다.
        UIManager.Instance.CloseLoadingUI(); // 

        Debug.Log("메인 로비 진입 완료 (깜빡임 없이 전환 성공)"); //
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