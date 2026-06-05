using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ChapterType
{
    None = 0,
    SlotBox_Chap1_1,
    SlotBox_Chap1_2,
    SlotBox_Chap2_1,
    SlotBox_Chap2_2,
    SlotBox_Chap2_3,
    SlotBox_Chap3_1,
    SlotBox_Chap3_2,
}

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
        closeBtn.onClick.AddListener(OnClickClose);
    }

    private void OnClickClose()

    {
        UIManager.Instance.CloseChapterScUI();
    }

    private void InitChapterButtons()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Transform slotBox = content.GetChild(i);

            string slotName = slotBox.name;

            Button chapterBtn = slotBox.GetComponentInChildren<Button>();
            if (chapterBtn != null)
            {
                // 문자열을 Enum 타입으로 안전하게 변환
                if (System.Enum.TryParse(slotName, out ChapterType chapterType))
                {
                    chapterBtn.onClick.AddListener(() => OnClickChapterButton(chapterType));
                }
                else
                {
                    Debug.LogError($"[ChapterScUI] {slotName}은 ChapterType Enum에 없는 이름입니다!");
                }
            }
        }
    }

    private void OnClickChapterButton(ChapterType chapterType)
    {
        var uiBase = UIManager.Instance.OpenPopupUI(UIType.EnterGamePopUI);

        // 팝업에 챕터 ID 데이터만 전달
        if (uiBase is EnterGamePopUI enterPopup)
        {
            enterPopup.SetupPopup(chapterType);
        }
    }
}
