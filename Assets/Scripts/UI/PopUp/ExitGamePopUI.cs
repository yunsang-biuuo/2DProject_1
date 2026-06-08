using UnityEngine;
using UnityEngine.UI;

public class ExitGamePopUI : UIBase
{
    [Header("Buttons")]
    [SerializeField] private Button restartBtn;
    [SerializeField] private Button returnlobbyBtn;
    [SerializeField] private Button closeBtn;


    private void Awake()
    {
        restartBtn.onClick.AddListener(ClosePopup);
        returnlobbyBtn.onClick.AddListener(OnClickReturnLobby);
        closeBtn.onClick.AddListener(ClosePopup);

    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    private void ClosePopup()
    {
        Time.timeScale = 1f;

        if (UIManager.Instance != null)
        {
            UIManager.Instance.CloseExitGamePopUI();
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnClickReturnLobby()
    {
        GameManager.Instance.ShowLoadingProcess(() =>
        {
            GameManager.Instance.ReturnToLobby();
        });

        UIManager.Instance.CloseExitGamePopUI();
    }
}