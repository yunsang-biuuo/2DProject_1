using UnityEngine;
using UnityEngine.UI;

public class RobbyUI : UIBase
{
    [Header("Lobby Buttons")]
    [SerializeField] private Button chapterButton;
    [SerializeField] private Button settingButton;
    [SerializeField] private Button storyboardButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button noFunctionButton;

    private void Awake()
    {
        chapterButton.onClick.AddListener(OnClickChapterButton);
        settingButton.onClick.AddListener(OnClickSettingButton);
        storyboardButton.onClick.AddListener(OnClickStoryboardButton);
        exitButton.onClick.AddListener(OnClickExitButton);

        if (noFunctionButton != null)
        {
            noFunctionButton.onClick.AddListener(OnClickNoFunctionButton);
        }
    }

    private void OnClickChapterButton()
    {
        UIManager.Instance.OpenMainUI(UIType.ChapterScUI);
        UIManager.Instance.CloseMainUI(UIType.RobbyUI);
    }

    private void OnClickSettingButton()
    {
        UIManager.Instance.OpenPopupUI(UIType.SettingUI);
    }

    private void OnClickStoryboardButton()
    {
        UIManager.Instance.OpenMainUI(UIType.StoryScUI);
    }

    private void OnClickExitButton()
    {
        UIManager.Instance.OpenPopupUI(UIType.QuitPopUI);
    }

    private void OnClickNoFunctionButton()
    {
        Debug.Log("아직 기능이 구현되지 않은 버튼입니다.");
    }
}
