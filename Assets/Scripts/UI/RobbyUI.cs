using UnityEngine;
using UnityEngine.UI;

public class RobbyUI : UIBase
{
    [Header("Lobby Buttons")]
    [SerializeField] private Button chapterButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button storyboardButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button noFunctionButton; // 아직 기능이 없는 5번째 버튼

    private void OnEnable()
    {
        // 버튼 리스너 연결
        chapterButton.onClick.AddListener(OnClickChapterButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
        storyboardButton.onClick.AddListener(OnClickStoryboardButton);
        exitButton.onClick.AddListener(OnClickExitButton);

        if (noFunctionButton != null)
        {
            noFunctionButton.onClick.AddListener(OnClickNoFunctionButton);
        }
    }

    private void OnDisable()
    {
        // 버튼 리스너 해제 (메모리 누수 방지)
        chapterButton.onClick.RemoveListener(OnClickChapterButton);
        settingButton.onClick.RemoveListener(OnClickSettingButton);
        storyboardButton.onClick.RemoveListener(OnClickStoryboardButton);
        exitButton.onClick.RemoveListener(OnClickExitButton);

        if (noFunctionButton != null)
        {
            noFunctionButton.onClick.RemoveListener(OnClickNoFunctionButton);
        }
    }

    /// <summary>
    /// 챕터 버튼 클릭 시 (콘텐츠 창이므로 ContentUI 레이어 오픈 / 비활성화로 관리)
    /// </summary>
    private void OnClickChapterButton()
    {
        // 로비 UI를 닫고 (MainUI 레이어에서 제거 및 비활성화)
        UIManager.Instance.CloseMainUI(UIType.RobbyUI);

        // GameManager를 통해 인게임 상태로 전환하여 게임 화면이 보이도록 처리
    }

    /// <summary>
    /// 세팅 버튼 클릭 시 (설정 창이므로 PopupUI 레이어 오픈 / 비활성화로 관리)
    /// </summary>
    private void OnClickSettingButton()
    {
        UIManager.Instance.OpenPopupUI(UIType.SettingUI);
    }

    /// <summary>
    /// 스토리보드 버튼 클릭 시 (대사/시나리오 관련이므로 ContentUI 레이어 오픈 / 비활성화로 관리)
    /// </summary>
    private void OnClickStoryboardButton()
    {
        // 💡 대사창을 열거나 관련 스토리를 띄우는 처리를 합니다.
        UIManager.Instance.OpenContentUI(UIType.DialogueUI);
    }

    /// <summary>
    /// 나가기 버튼 클릭 시 (생성 및 완전히 지울 팝업이므로 PopupUI 레이어 오픈)
    /// </summary>
    private void OnClickExitButton()
    {
        // 💡 ExitPopup은 UIManager 내부 CloseUI 로직에 의해 닫힐 때 Destroy 처리됩니다.
        UIManager.Instance.OpenPopupUI(UIType.QuitPopupUI);
    }

    /// <summary>
    /// 아직 기능이 없는 5번째 버튼 클릭 시
    /// </summary>
    private void OnClickNoFunctionButton()
    {
        Debug.Log("아직 기능이 구현되지 않은 버튼입니다.");
    }
}
