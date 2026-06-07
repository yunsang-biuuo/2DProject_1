using UnityEngine;
using UnityEngine.UI;

public class GameOverPopUI : UIBase
{
    [Header("Buttons")]
    [SerializeField] private Button acceptBtn;

    private void Awake()
    {
        acceptBtn.onClick.AddListener(OnClickReturnLobby);
    }

    private void OnEnable()
    {
        //Time.timeScale = 0f;
    }

    private void OnClickReturnLobby()
    {
        GameManager.Instance.ShowLoadingProcess(() =>
        {
            GameManager.Instance.ReturnToLobby();
        });

        UIManager.Instance.CloseGameOverPopUI();
    }
}
