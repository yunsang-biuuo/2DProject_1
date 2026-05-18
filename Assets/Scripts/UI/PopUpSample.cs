using UnityEngine;
using UnityEngine.UI;

public class PopUpSample : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject popupPanel;
    [Header("Buttons")]
    [SerializeField] private Button closeButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button restartButton;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(OnClickClose);
        quitButton.onClick.AddListener(OnClickQuit);
        restartButton.onClick.AddListener(OnClickRestart);
    }

    private void OnDisable()
    {
        closeButton.onClick.RemoveListener(OnClickClose);
        quitButton.onClick.RemoveListener(OnClickQuit);
        restartButton.onClick.RemoveListener(OnClickRestart);
    }

    private void HidePopup()
    {
        UIManager.Instance.ClosePopupSampleUI();
        Time.timeScale = 1f;
    }

    private void OnClickClose()
    {
        HidePopup();
    }

    private void OnClickQuit()
    {
        HidePopup();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    private void OnClickRestart()
    {
        HidePopup();
    }

}
