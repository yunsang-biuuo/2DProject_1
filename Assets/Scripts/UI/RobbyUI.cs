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
        UIManager.Instance.OpenChapterScUI();
    }

    private void OnClickSettingButton()
    {
        UIManager.Instance.OpenSettingUI();
    }

    private void OnClickStoryboardButton()
    {
        UIManager.Instance.OpenStoryScUI();
    }

    private void OnClickExitButton()
    {
        UIManager.Instance.OpenQuitPopUI();
    }

    private void OnClickNoFunctionButton()
    {
        Debug.Log("아직 기능이 구현되지 않은 버튼입니다.");
    }
}
