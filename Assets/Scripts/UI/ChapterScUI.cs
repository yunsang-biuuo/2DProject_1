using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChapterScUI : UIBase
{
    [Header("Top Layout")]
    [SerializeField] private Button closeBtn;

    [Header("Scroll Content")]
    [SerializeField] private Transform content;

    private void Awake()
    {
        InitChapterButtons();
    }

    private void Start()
    {
        closeBtn.onClick.AddListener(() => UIManager.Instance.ClosePopupUI(UIType.EnterGamePopUI));
    }

    private void InitChapterButtons()
    {
        // Content 자식으로 배치된 모든 SlotBox들을 순회하며 버튼 매핑
        for (int i = 0; i < content.childCount; i++)
        {
            Transform slotBox = content.GetChild(i);
            string chapterId = slotBox.name;

            // 자식 오브젝트 중 Btn_Slot 컴포넌트 탐색
            Button chapterBtn = slotBox.GetComponentInChildren<Button>();

            if (chapterBtn != null)
            {
                string currentChapter = chapterId;

                chapterBtn.onClick.AddListener(() => OnClickChapterButton(currentChapter));
            }
        }
    }

    private void OnClickChapterButton(string chapterId)
    {
        var uiBase = UIManager.Instance.OpenPopupUI(UIType.EnterGamePopUI);

        // 팝업에 챕터 ID 데이터만 전달
        if (uiBase is EnterGamePopUI enterPopup)
        {
            enterPopup.SetupPopup(chapterId);
        }
    }
}
