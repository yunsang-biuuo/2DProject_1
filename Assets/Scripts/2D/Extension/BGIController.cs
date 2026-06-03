using UnityEngine;
using UnityEngine.UI;

public class BGIController : MonoBehaviour
{
    public static BGIController Instance { get; private set; }

    [Header("UI Component")]
    [SerializeField] private Image bgImage;

    [Header("Background Sprites")]
    [SerializeField] private Sprite sprite_Chap1_1;
    [SerializeField] private Sprite sprite_Chap1_2;
    [SerializeField] private Sprite sprite_Chap2_1;
    [SerializeField] private Sprite sprite_Chap2_2;
    [SerializeField] private Sprite sprite_Chap2_3;
    [SerializeField] private Sprite sprite_Chap3_1;
    [SerializeField] private Sprite sprite_Chap3_2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ChangeBackground(ChapterType chapterType)
    {
        if (bgImage == null)
        {
            Debug.LogError("[BackgroundImageController] 자식 bgImage 컴포넌트가 연결되지 않았습니다!");
            return;
        }

        Sprite targetSprite = null;

        switch (chapterType)
        {
            case ChapterType.SlotBox_Chap1_1: targetSprite = sprite_Chap1_1; break;
            case ChapterType.SlotBox_Chap1_2: targetSprite = sprite_Chap1_2; break;
            case ChapterType.SlotBox_Chap2_1: targetSprite = sprite_Chap2_1; break;
            case ChapterType.SlotBox_Chap2_2: targetSprite = sprite_Chap2_2; break;
            case ChapterType.SlotBox_Chap2_3: targetSprite = sprite_Chap2_3; break;
            case ChapterType.SlotBox_Chap3_1: targetSprite = sprite_Chap3_1; break;
            case ChapterType.SlotBox_Chap3_2: targetSprite = sprite_Chap3_2; break;
        }

        // 매칭된 이미지가 있다면 이미지 컴포넌트 교체 후 켜기
        if (targetSprite != null)
        {
            bgImage.sprite = targetSprite;
            bgImage.gameObject.SetActive(true);
        }
        else
        {
            // 만약 None 이거나 이미지를 안 넣어뒀다면 배경을 임시로 꺼두거나 기본 상태 유지
            bgImage.sprite = null;
            bgImage.gameObject.SetActive(false);
            Debug.LogWarning($"[BackgroundImageController] {chapterType}에 등록된 스프라이트 에셋이 없습니다.");
        }
    }
}