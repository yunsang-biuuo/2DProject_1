using UnityEngine;
using UnityEngine.UI;

public class QuitPopupUI : UIBase
{
    [Header("Buttons")]
    [SerializeField] private Button QuitBtn;
    [SerializeField] private Button continueBtn;
    [SerializeField] private Button closeBtn;

    private void OnEnable()
    {
        QuitBtn.onClick.AddListener(OnClickQuit);
        continueBtn.onClick.AddListener(OnClickClose);
        closeBtn.onClick.AddListener(OnClickClose);
    }

    private void OnDisable()
    {
        QuitBtn.onClick.RemoveListener(OnClickQuit);
        continueBtn.onClick.RemoveListener(OnClickClose);
        closeBtn.onClick.RemoveListener(OnClickClose);
    }

    private void OnClickQuit()
    {
        UIManager.Instance.ClosePopupUI(UIType.QuitPopupUI);
        GameManager.Instance.SaveAndEndGame();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터 플레이 종료
#else
        Application.Quit(); // 실제 빌드 게임 종료
#endif
    }

    private void OnClickClose()
    {
        UIManager.Instance.ClosePopupUI(UIType.QuitPopupUI);
    }
}
