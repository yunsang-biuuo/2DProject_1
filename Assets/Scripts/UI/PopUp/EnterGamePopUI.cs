using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class EnterGamePopUI : UIBase
{
    [Header("Texts")]
    [SerializeField] private TMP_Text txt_ChapterName; // 챕터 이름을 보여줄 텍스트

    [Header("Buttons")]
    [SerializeField] private Button enterBtn;
    [SerializeField] private Button noBtn;
    [SerializeField] private Button closeBtn;

    private string _currentChapterId;
    private ChapterType _currentChapterType;

    private void Awake()
    {
        noBtn.onClick.AddListener(ClosePopup);
        closeBtn.onClick.AddListener(ClosePopup);
        enterBtn.onClick.AddListener(OnClickEnter);
    }

    public void SetupPopup(ChapterType chapterType)
    {
        _currentChapterType = chapterType;
        _currentChapterId = chapterType.ToString();

        // 클릭 오브젝트 이름 가져오서 자르기
        if (txt_ChapterName != null)
        {
            string chapterName = _currentChapterId.Replace("SlotBox_", "")
                                                  .Replace("Chap", "")
                                                  .Replace("chap", "")
                                                    .Replace("_", "-");

            txt_ChapterName.text = $"Would you like to enter Chapter {chapterName}?";
        }
    }

    private void OnClickEnter()
    {
        Debug.Log($"[ChapterEnterPopup] {_currentChapterId} 이동 승인! 등록된 이동 좌표 콜백을 실행합니다.");

        GameManager.Instance.EnterChapterMap(_currentChapterType);
        GameManager.Instance.OnClick_StartGame();
        ClosePopup();
    }

    private void ClosePopup()
    {
        UIManager.Instance.CloseEnterGamePopUI();
    }
}
